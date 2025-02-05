using Almirah.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace Almirah.Mac;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseSharedMauiApp();
        builder.Services.AddSingleton<IAuthService, AuthService>();

        return builder.Build();
    }
}