using Everyday.Services.Interfaces;
using Everyday.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Everyday.GUI;

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
            })
            .Configuration
            .AddJsonFile(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()), "appsettings.json", optional: false, false);

        ConfigureServices(builder.Services);

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging()
                .AddSingleton<MainPage>()
                .AddSingleton<IHttpClientService,HttpClientService>(provider => new("https://localhost:44318/"))
                .AddSingleton<ICryptographyService, CryptographyService>();
    }
}
