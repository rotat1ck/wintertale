using DotNetEnv;
using StackExchange.Redis;

namespace AuthService.Persistence.Redis {
    public static class RedisInitializer {
        public static WebApplicationBuilder AppConfigureRedis(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();

            string? connectionString;
            var password = Environment.GetEnvironmentVariable("RedisPassword");
            if (string.IsNullOrWhiteSpace(password)) {
                var logger = LoggerFactory.Create(builder =>
                    builder.AddConsole()
                ).CreateLogger("AuthService.Persistence.Redis.RedisInitializer");
                logger.LogCritical("Пароль REDIS не указан\n" +
                    "\tПример смотрите в файле .env.example\n");
                Environment.Exit(-1);
            }
                
            var serverOption = builder.Configuration["RedisConnection"];
            connectionString = serverOption?.Replace("{REDIS_PASSWORD}", password);
                
            // base redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(p =>
                ConnectionMultiplexer.Connect(connectionString!)
            );

            return builder;
        }
    }
}
