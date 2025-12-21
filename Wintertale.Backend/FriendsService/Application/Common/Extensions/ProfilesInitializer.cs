namespace FriendsService.Application.Common.Extensions {
    public static class ProfilesInitializer {
        public static WebApplicationBuilder AppConfigureProfiles(this WebApplicationBuilder builder) {
            builder.Services.AddAutoMapper(cfg => {
                
            });

            return builder;
        }
    }
}
