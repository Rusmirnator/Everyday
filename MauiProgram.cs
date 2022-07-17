using Microsoft.Extensions.Configuration;
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
            });

        var config = 
            new ConfigurationBuilder()
                .AddJsonStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Everyday.appsettings.json"))
                    .Build();

        return builder.Build();
    }
}
