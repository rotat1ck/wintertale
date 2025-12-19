using AuthService.Application.Common.Extensions;
using AuthService.Persistence.Data;
using AuthService.Persistence.Jwt;
using AuthService.Persistence.Secrets;

namespace AuthService.WebApi {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.AppRegisterRepositories();
            builder.AppRegisterServices();
            builder.AppRegisterProviders();

            builder.AppConfigureProfiles();
            builder.AppConfigureJWT();
            builder.AppPersistData();
            builder.AppInjectSecrets();

            var app = builder.Build();

            app.AppRun();
        }
    }
}
