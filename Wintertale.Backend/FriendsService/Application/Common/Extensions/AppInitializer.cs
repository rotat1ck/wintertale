using FriendsService.Persistence.Data;

namespace FriendsService.Application.Common.Extensions {
    public static class AppInitializer {
        public static WebApplication AppRun(this WebApplication app) {
            app.AppMigrateDatabase();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            //app.UseAuthorization();
            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
