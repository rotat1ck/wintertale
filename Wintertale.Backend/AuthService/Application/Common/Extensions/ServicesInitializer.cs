using AuthService.Application.Interfaces.Services;
using AuthService.WebApi.Services;

namespace AuthService.Application.Common.Extensions {
    public static class ServicesInitializer {
        public static WebApplicationBuilder AppRegisterServices(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IAuthService, WebApi.Services.AuthService>();
            builder.Services.AddScoped<IVerifyService, VerifyService>();

            return builder;
        }
    }
}
