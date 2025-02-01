using Almirah.Services;
using Almirah.Services.Interfaces;
using Almirah.ViewModels;
using Almirah.Views;
using Microsoft.Extensions.Logging;

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

		// Register services
		builder.Services.AddSingleton<IStorageService, StorageService>();
		builder.Services.AddSingleton<IFileSystemService, FileSystemService>();

		// Register ViewModels
		builder.Services.AddSingleton<MainPageVM>();
		builder.Services.AddSingleton<FolderPageVM>();

		// Register Views
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<FolderPage>();
        
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder;
	}
}
