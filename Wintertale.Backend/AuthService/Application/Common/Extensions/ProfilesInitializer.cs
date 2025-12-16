using AuthService.Application.Common.Profiles;

namespace AuthService.Application.Common.Extensions {
    public static class ProfilesInitializer {
        public static WebApplicationBuilder AppConfigureProfiles(this WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<AuthProfile>();
            });

            return builder;
        }
    }
}
