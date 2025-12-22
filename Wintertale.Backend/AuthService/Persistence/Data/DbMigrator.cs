using Microsoft.EntityFrameworkCore;
using Persistence;

namespace AuthService.Persistence.Data {
    public static class DbMigrator {
        public static WebApplication AppMigrateDatabase(this WebApplication app) {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try {
                //context.Database.EnsureCreated();
                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations.Any()) {
                    context.Database.Migrate();
                }
            } catch (Exception ex) {
                app.Logger.LogCritical($"Ошибка миграции: {ex.Message}");
                Environment.Exit(-1);
            }

            return app;
        }
    }
}
