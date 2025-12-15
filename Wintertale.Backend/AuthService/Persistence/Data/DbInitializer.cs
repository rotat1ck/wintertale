using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Data {
    public static class DbInitializer {
        public static WebApplicationBuilder AppPersistData(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();
            string? connectionString = Environment.GetEnvironmentVariable("POSTGRES_SERVER");
            if (string.IsNullOrEmpty(connectionString)) {
                var logger = LoggerFactory.Create(builder =>
                    builder.AddConsole()
                ).CreateLogger("AuthService.Persistence.Data.DbInitializer");

                logger.LogCritical("Запуск невозможен, параметр POSTGRES_SERVER не настроен");
                Environment.Exit(-1);
            }

            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(connectionString)
            );

            return builder;
        }
    }
}
