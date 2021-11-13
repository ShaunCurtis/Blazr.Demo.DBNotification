/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Blazr.Demo.DBNotification.Core;
using Blazr.Demo.DBNotification.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazr.Demo.DBNotification
{
    /// <summary>
    /// This is the server version of the data broker.
    /// It's used in the Server SPA and in the API web server for the WASM SPA 
    /// </summary>
    public class WeatherForecastServerDataBroker : IWeatherForecastDataBroker
    {
        private WeatherForecastDataProvider weatherForecastDataService;  

        public WeatherForecastServerDataBroker(WeatherForecastDataProvider weatherForecastDataService)
            => this.weatherForecastDataService = weatherForecastDataService;

        public async Task<bool> AddForecastAsync(WeatherForecast record)
            => await this.weatherForecastDataService.AddForecastAsync(record);

        public async Task<bool> DeleteForecastAsync(Guid Id)
            => await this.weatherForecastDataService.DeleteForecastAsync(Id);

        public async Task<List<WeatherForecast>> GetWeatherForecastsAsync()
            => await this.weatherForecastDataService.GetWeatherForecastsAsync();

    }
}
