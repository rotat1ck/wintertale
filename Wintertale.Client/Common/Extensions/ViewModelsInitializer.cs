using CommunityToolkit.Maui;
using Wintertale.Client.ViewModels.Auth;
using Wintertale.Client.ViewModels.Dashboard;

namespace Wintertale.Client.Common.Extensions {
    internal static class ViewModelsInitializer {
        public static MauiAppBuilder AppRegisterViewModels(this MauiAppBuilder builder) {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<RegistrationViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();

            return builder;
        }
    }
}
