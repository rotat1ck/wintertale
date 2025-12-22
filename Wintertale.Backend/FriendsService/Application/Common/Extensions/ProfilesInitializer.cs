using FriendsService.Application.Common.Profiles;

namespace FriendsService.Application.Common.Extensions {
    public static class ProfilesInitializer {
        public static WebApplicationBuilder AppConfigureProfiles(this WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<FriendsProfile>();
            });

            return builder;
        }
    }
}
