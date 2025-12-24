using CommunityToolkit.Maui;
using Wintertale.Client.Services.BaseApi;

namespace Wintertale.Client.Config.DI {
    internal static class ServicesInitializer {
        public static MauiAppBuilder AppRegisterServices(this MauiAppBuilder builder) {
            builder.Services.AddSingleton<IApiService, ApiService>();

            return builder;
        }
    }
}
