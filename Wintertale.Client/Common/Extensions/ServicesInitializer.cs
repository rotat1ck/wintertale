using CommunityToolkit.Maui;
using Wintertale.Client.Services.Auth;
using Wintertale.Client.Services.BaseApi;
using Wintertale.Client.Services.Friends;

namespace Wintertale.Client.Common.Extensions {
    internal static class ServicesInitializer {
        public static MauiAppBuilder AppRegisterServices(this MauiAppBuilder builder) {
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<IApiService, ApiService>();
            
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IFriendService, FriendService>();

            return builder;
        }
    }
}
