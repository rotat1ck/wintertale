using FriendsService.Application.Interfaces.Repositories;
using FriendsService.WebApi.Repositories;

namespace FriendsService.Application.Common.Extensions {
    public static class RepositoriesInitializer {
        public static WebApplicationBuilder AppRegisterRepositories(this WebApplicationBuilder builder) {
            builder.Services.AddScoped<IFriendRepository, FriendRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            return builder;
        }
    }
}
