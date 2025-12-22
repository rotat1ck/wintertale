using FriendsService.Application.Common.Extensions;
using FriendsService.Persistence.Data;
using FriendsService.Persistence.Jwt;

namespace FriendsService.WebApi {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.AppRegisterRepositories();
            builder.AppRegisterServices();

            builder.AppConfigureProfiles();

            builder.AppConfigureJWT();
            builder.AppPersistData();

            var app = builder.Build();

            app.AppRun();
        }
    }
}
