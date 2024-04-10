using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using HukutaiaExplorer.MVVM.ViewModels;
using HukutaiaExplorer.MVVM.Views;
using Microsoft.Extensions.Logging;

namespace HukutaiaExplorer
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
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
                    fonts.AddFont("Poppins-Thin.ttf", "PoppinsThin");

                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
