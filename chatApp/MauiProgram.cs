using chatApp.Services;
using chatApp.ViewModel;
using Microsoft.Extensions.Logging;

namespace chatApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MessageService>();
        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<SettingsView>();
        builder.Services.AddSingleton<SettingsViewModel>();

        builder.Services.AddSingleton<MessageTemplateSelector>();

        return builder.Build();
    }
}