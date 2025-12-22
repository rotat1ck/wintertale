using FriendsService.Application.Interfaces.Services;
using FriendsService.WebApi.Services;

namespace FriendsService.Application.Common.Extensions {
    public static class ServicesInitializer {
        public static WebApplicationBuilder AppRegisterServices(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IFriendService, FriendService>();

            return builder;
        }
    }
}
