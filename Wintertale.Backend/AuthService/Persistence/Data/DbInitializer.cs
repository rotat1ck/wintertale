using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace AuthService.Persistence.Data {
    public static class DbInitializer {
        public static WebApplicationBuilder AppPersistData(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();

            string? connectionString = Environment.GetEnvironmentVariable("POSTGRES_SERVER");
            var logger = LoggerFactory.Create(builder =>
                builder.AddConsole()
            ).CreateLogger("AuthService.Persistence.Data.DbInitializer");

            if (builder.Environment.IsDevelopment()) {
                if (string.IsNullOrEmpty(connectionString)) {
                    logger.LogCritical("Запуск невозможен, параметр POSTGRES_SERVER не настроен\n" +
                                        "\tСмотрите .env.example DEV настройки");
                    Environment.Exit(-1);
                }
            } 

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("Persistence"))
            );

            return builder;
        }
    }
}
