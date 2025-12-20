using DotNetEnv;
using StackExchange.Redis;

namespace AuthService.Persistence.Redis {
    public static class RedisInitializer {
        public static WebApplicationBuilder AppConfigureRedis(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();

            string? redisConnection = Environment.GetEnvironmentVariable("RedisConnection");
            var logger = LoggerFactory.Create(builder =>
                builder.AddConsole()
            ).CreateLogger("AuthService.Persistence.Redis.RedisInitializer");

            if (builder.Environment.IsDevelopment()) {
                if (string.IsNullOrWhiteSpace(redisConnection)) {
                    logger.LogCritical("Запуск невозможен, параметр RedisConnection не указан\n" +
                        "\tСмотрите .env.example DEV настройки");
                    Environment.Exit(-1);
                }
            } 
            //else {
            //    string? redisHost = Environment.GetEnvironmentVariable("REDIS_HOST");
            //    if (string.IsNullOrEmpty(redisHost)) {
            //        logger.LogCritical("Запуск невозможен, параметр REDIS_HOST не установлен");
            //        Environment.Exit(-1);
            //    }

            //    string? redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
            //    if (string.IsNullOrEmpty(redisPassword)) {
            //        logger.LogCritical("Запуск невозможен, параметр REDIS_PASSWORD не установлен");
            //        Environment.Exit(-1);
            //    }
            //}

            builder.Services.AddSingleton<IConnectionMultiplexer>(p =>
                ConnectionMultiplexer.Connect(redisConnection!)
            );

            return builder;
        }
    }
}
