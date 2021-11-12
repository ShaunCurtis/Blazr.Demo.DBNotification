using Blazr.Demo.DBNotification.Core;
using Blazr.Demo.DBNotification.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazr.Demo.DBNotification.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<Blazr.Demo.DBNotification.UI.App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<WeatherForecastViewService>();
            builder.Services.AddScoped<IWeatherForecastDataBroker, WeatherForecastAPIDataBroker>();

            await builder.Build().RunAsync();
        }
    }
}
