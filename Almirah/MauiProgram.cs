using Almirah.Services.Interfaces;
using Almirah.ViewModels;
using Almirah.ViewModels.Notes;
using Almirah.Views;
using Almirah.Views.Notes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Almirah;

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

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        return builder.Build();
    }
}