using Almirah.Models;
using Almirah.Services.Interfaces;
using Almirah.Services.Notes;
using Almirah.ViewModels.Notes;
using Almirah.Views.Notes;
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
        builder.Services.AddSingleton<IDatabaseService>(sp =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notes.db3");
            var encryptionService = sp.GetRequiredService<IEncryptionService>();
            return new DatabaseService(dbPath, encryptionService);
        });
        
        builder.Services.AddSingleton<IEncryptionService>(sp => 
            new EncryptionService("YourFixedSecretKey12345678901234", "YourIV1234567890"));

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