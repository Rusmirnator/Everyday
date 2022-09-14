﻿using Everyday.Core.Models;
using Everyday.Data.DataProviders;
using Everyday.Data.Interfaces;
using Everyday.Data.Services;
using Everyday.GUI.Base;
using Everyday.GUI.Pages.ViewModels;
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
        ConfigureServiceProvider(builder.Services);

        return builder.Build();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging()
                .AddViewModels()
                .AddDataProviders()
                .AddServices();
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
                       .AddSingleton<ItemEditorViewModel>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddSingleton<IHttpClientService, HttpClientService>()
                        .AddScoped<IAuthorizationService, AuthorizationService>()
                        .AddScoped<ICryptographyService, CryptographyService>()
                        .AddScoped<IItemService, ItemService>()
                        .AddScoped<IManufacturerService, ManufacturerService>()
                        .AddScoped<IConsumableService, ConsumableService>();
    }

    private static IServiceCollection AddDataProviders(this IServiceCollection services)
    {
        return services.AddScoped<IUserDataProvider, UserDataProvider>()
                       .AddScoped<IItemDataProvider, ItemDataProvider>()
                       .AddScoped<IDataProvider<Manufacturer>, ManufacturerDataProvider>()
                       .AddScoped<IDataProvider<Consumable>, ConsumableDataProvider>();
    }
}
