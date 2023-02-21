using Everyday.Application;
using Everyday.GUI.Base;
using Everyday.GUI.Pages.ViewModels;
using Everyday.Infrastructure;
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

        ConfigureServices(builder.Services, builder.Configuration);
        ConfigureServiceProvider(builder.Services);

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddLogging()
                .AddViewModels()
                .AddApplicationServices()
                .AddInfrastructureServices(config);
    }

    private static void ConfigureServiceProvider(IServiceCollection services)
    {
        IServiceProvider provider = services.BuildServiceProvider();
        DependencyInjectionSource.Resolver = (type) =>
        {
            return provider.GetRequiredService(type);
        };
    }

    private static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        return services.AddSingleton<MainPageViewModel>()
                       .AddSingleton<MenuViewModel>()
                       .AddSingleton<ErrorViewModel>()
                       .AddSingleton<PurchasesViewModel>()
                       .AddSingleton<ScannerViewModel>()
                       .AddSingleton<ItemEditorViewModel>()
                       .AddSingleton<ItemListViewModel>();
    }
}
