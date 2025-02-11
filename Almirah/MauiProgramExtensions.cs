using System.Reflection;
using Almirah.Models;
using Almirah.Services.Interfaces;
using Almirah.Services.Notes;
using Almirah.ViewModels.Notes;
using Almirah.Views.Notes;
using Microsoft.Extensions.Configuration;
using MainPage = Almirah.Views.Notes.MainPage;

namespace Almirah;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Almirah.Resources.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream) // Load the file as a stream
            .Build();

        // Register IConfiguration
        builder.Configuration.AddConfiguration(config);

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
            var dbFileName = config.GetSection("Database")["DbFileName"];
    
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);
    
            var encryptionService = sp.GetRequiredService<IEncryptionService>();
            return new DatabaseService(dbPath, encryptionService);
        });
        
        builder.Services.AddSingleton<IEncryptionService>(sp =>
        {
            var encryptionSection = config.GetSection("Encryption");
    
            var key = encryptionSection["Key"];
            var iv = encryptionSection["IV"];

            return new EncryptionService(key, iv);
        });


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