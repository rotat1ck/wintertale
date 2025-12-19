using AuthService.Persistence.Data;
using AuthService.WebApi.Middlewares;

namespace AuthService.Application.Common.Extensions {
    public static class AppInitializer {
        public static WebApplication AppRun(this WebApplication app) {
            app.AppMigrateDatabase();

            app.UseMiddleware<ExceptionsMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            //app.UseAuthorization();
            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
