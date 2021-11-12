/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Blazr.Demo.DBNotification.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazr.Demo.DBNotification.Data
{
    /// <summary>
    /// This is the client version of the data broker.
    /// It's used in the Wasm SPA and gets its data from the API 
    /// </summary>
    public class WeatherForecastAPIDataBroker : IWeatherForecastDataBroker
    {
        private HttpClient httpClient;  

        public WeatherForecastAPIDataBroker(HttpClient httpClient)
            => this.httpClient = httpClient;

        public async Task<bool> AddForecastAsync(WeatherForecast record)
        {
            var response = await this.httpClient.PostAsJsonAsync<WeatherForecast>($"/api/weatherforecast/add", record);
            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }

        public async Task<bool> DeleteForecastAsync(Guid Id)
        {
            var response = await this.httpClient.PostAsJsonAsync<Guid>($"/api/weatherforecast/delete", Id);
            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastsAsync()
           => await this.httpClient.GetFromJsonAsync<List<WeatherForecast>>($"/api/weatherforecast/list");
    }
}
