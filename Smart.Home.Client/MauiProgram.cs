using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Smart.Home.Client.Services;
using Smart.Home.Client.Services.Interfaces;
using System.Reflection;

namespace Smart.Home.Client
{
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
                });

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Smart.Home.Client.wwwroot.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Services.AddMauiBlazorWebView();
            builder.Configuration.AddConfiguration(config);
            builder.Services.AddScoped<IMqttService, MqttService>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
