using AuthService.Application.Interfaces.Repositories;
using AuthService.WebApi.Repositories;

namespace AuthService.Application.Common.Extensions {
    public static class RepositoriesInitializer {
        public static WebApplicationBuilder AppRegisterRepositories(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IVerifyRepository, VerifyRepository>();

            return builder;
        }
    }
}
