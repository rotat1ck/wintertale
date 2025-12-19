using DotNetEnv;

namespace AuthService.Persistence.Secrets {
    public static class SecretsInitializer {
        public static WebApplicationBuilder AppInjectSecrets(this WebApplicationBuilder builder) {
            builder.AppInjectVerification();

            return builder;
        }

        private static WebApplicationBuilder AppInjectVerification(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();

            var logger = LoggerFactory.Create(builder =>
                builder.AddConsole()
            ).CreateLogger("AuthService.Persistence.Secrets.SecretsInitializer");

            string? smsKey = Environment.GetEnvironmentVariable("SMSRU_APIKEY");
            if (string.IsNullOrWhiteSpace(smsKey)) {
                logger.LogCritical("Отсутствует SMSRU_APIKEY верификация номеров телефона невозможна");
                Environment.Exit(-1);
            }

            builder.Configuration["SMSRU_APIKEY"] = smsKey;

            return builder;
        }
    }
}
