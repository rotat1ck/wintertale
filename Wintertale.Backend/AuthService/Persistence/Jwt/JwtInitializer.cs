using System.Text;
using AuthService.Application.Common.Providers;
using AuthService.Application.Interfaces.Providers;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Persistence.Jwt {
    public static class JwtInitializer {
        public static WebApplicationBuilder AppConfigureJWT(this WebApplicationBuilder builder) {
            Env.TraversePath().Load();
            var logger = LoggerFactory.Create(builder =>
                builder.AddConsole()
            ).CreateLogger("AuthService.Persistence.Jwt.JwtInitializer");

            string? jwtKey = Environment.GetEnvironmentVariable("JWTKey");
            if (string.IsNullOrWhiteSpace(jwtKey)) {
                logger.LogCritical("JWT ключ не указан\n" +
                    "\tПример смотрите в файле .env.example\n" +
                    "\tСоздать ключ(если есть openssl): openssl rand -base64 32");
                Environment.Exit(-1);
            }

            if (!int.TryParse(Environment.GetEnvironmentVariable("JWTLifetimeMinutes"), out int jwtLifetimeMinutes)) {
                logger.LogWarning("Время жизни JWT токена не указана\n" +
                    "\tПример смотрите в файле .env.example\n" +
                    "\tЗначение установлено на 30 минут");
                jwtLifetimeMinutes = 30;
            }

            if (!int.TryParse(Environment.GetEnvironmentVariable("RefreshLifetimeDays"), out int refreshLifetimeDays)) {
                logger.LogWarning("Время жизни Refresh токена не указана\n" +
                    "\tПример смотрите в файле .env.example\n" +
                    "\tЗначение установлено на 30 дней");
                refreshLifetimeDays = 30;
            }

            builder.Configuration["JWT:Key"] = jwtKey;
            builder.Configuration["JWT:LifetimeMinutes"] = jwtLifetimeMinutes.ToString();
            builder.Configuration["RefreshToken:LifetimeDays"] = refreshLifetimeDays.ToString();

            builder.Services.AddSingleton<IJWTProvider, JWTProvider>();
            // Добавляем политики для [Authorize]
            builder.AppConfigurePolicies();

            return builder;
        }

        private static WebApplicationBuilder AppConfigurePolicies(this WebApplicationBuilder builder) {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
