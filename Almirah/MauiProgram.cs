using Almirah;
using Almirah.Services;
using Almirah.Services.Interfaces;
using Almirah.ViewModels;
using Almirah.Views;

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

        // Register services
        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<IFileSystemService, FileSystemService>();
        
        // Register ViewModels
        builder.Services.AddSingleton<MainPageVM>();
        
        // Register Views
        
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}