namespace AuthService.Application.Interfaces.Providers {
    public interface IRedisProvider {
        Task SubscribeAsync(string channel, Func<string, Task> handler);
        Task PublishAsync(string channel, string message);
        Task UnsubscribeAsync(string channel);
    }
}
