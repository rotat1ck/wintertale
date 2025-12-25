using Wintertale.Client.ViewModels.Auth;

namespace Wintertale.Client.Common.Extensions {
    internal static class ViewModelsInitializer {
        public static MauiAppBuilder AppRegisterViewModels(this MauiAppBuilder builder) {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddSingleton<RegistrationViewModel>();

            return builder;
        }
    }
}
