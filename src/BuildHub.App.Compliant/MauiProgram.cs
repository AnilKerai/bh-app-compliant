using Microsoft.Extensions.Logging;
using BuildHub.App.Compliant.Shared.Services;
using BuildHub.App.Compliant.Services;
using BuildHub.App.Compliant.Shared;

namespace BuildHub.App.Compliant;

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
            });

        // Add device-specific services used by the BuildHub.App.Compliant.Shared project
        builder
            .Services
            .AddSingleton<IFormFactor, FormFactor>()
            .AddSharedServices()
            ;

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
