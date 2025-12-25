using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Wintertale.Client.Common.Extensions;

namespace Wintertale.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SplineSans-Regular.ttf", "SplineSansRegular");
                    fonts.AddFont("SplineSans-SemiBold.ttf", "SplineSansSemiBold");
                    fonts.AddFont("SplineSans-Bold.ttf", "SplineSansBold");
                });

            builder.AppRegisterServices();
            builder.AppRegisterViewModels();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
