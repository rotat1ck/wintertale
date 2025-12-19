using AuthService.Application.Interfaces.Providers;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace AuthService.Application.Common.Providers {
    public class RedisProvider : IRedisProvider {
        private readonly IConnectionMultiplexer redis;
        private readonly ConcurrentDictionary<string, ChannelMessageQueue> subscriptions = new();

        public RedisProvider(IConnectionMultiplexer redis) {
            this.redis = redis;
        }

        public async Task SubscribeAsync(string channel, Func<string, Task> handler) {
            if (subscriptions.TryGetValue(channel, out var existing)) {
                return;
            }

            var sub = redis.GetSubscriber();
            var queue = await sub.SubscribeAsync(RedisChannel.Literal(channel));
            subscriptions[channel] = queue;

            queue.OnMessage(async channelMessage => {
                await handler(channelMessage.Message!);
            });
        }

        public async Task PublishAsync(string channel, string message) {
            var sub = redis.GetSubscriber();
            await sub.PublishAsync(RedisChannel.Literal(channel), message);
        }

        public async Task UnsubscribeAsync(string channel) {
            if (subscriptions.TryRemove(channel, out var queue)) {
                await queue.UnsubscribeAsync();
            }
        }
    }
}
