using AuthService.Application.Common.Providers;
using AuthService.Application.Interfaces.Providers;

namespace AuthService.Application.Common.Extensions {
    public static class ProvidersInitializer {
        public static WebApplicationBuilder AppRegisterProviders(this WebApplicationBuilder builder) {
            builder.Services.AddSingleton<IJWTProvider, JWTProvider>();
            builder.Services.AddSingleton<IHashProvider, HashProvider>();
            builder.Services.AddSingleton<IRedisProvider, RedisProvider>();

            return builder;
        }
    }
}
