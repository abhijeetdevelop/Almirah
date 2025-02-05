using System.IO;
using Almirah.Models;
using Almirah.Services.Interfaces;
using Almirah.ViewModels;
using Almirah.ViewModels.Notes;
using Almirah.Views;
using Almirah.Views.Notes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Storage;
using MainPage = Almirah.Views.Notes.MainPage;

namespace Almirah;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<App>();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddTransient<Note>();

        // Register services
        builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");

        builder.Services.AddSingleton<IDatabaseService>(s => new DatabaseService(dbPath));

        // Register ViewModels
        builder.Services.AddSingleton<MainPageVM>();
        builder.Services.AddSingleton<ViewVM>();

        // Register Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ViewPage>();
        builder.Services.AddSingleton<App>();

        return builder;
    }
}