/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazr.Demo.DBNotification.Core
{
    /// <summary>
    /// Provides WeatherForecast data for the UI.  
    /// Nornally runs as a Scoped Service i.e. one per SPA sesson.
    /// </summary>
    public class WeatherForecastViewService
    {
        private IWeatherForecastDataBroker weatherForecastDataBroker;

        public List<WeatherForecast> Records { get; private set; }

        public WeatherForecastViewService(IWeatherForecastDataBroker weatherForecastDataBroker)
            => this.weatherForecastDataBroker = weatherForecastDataBroker;
        
        public async Task GetForecastsAsync()
        {
            // Sets to null records and notifies twice so you can see the reloading event in the SPA
            this.Records = null;
            this.NotifyListChanged(this.Records, EventArgs.Empty);
            this.Records = await weatherForecastDataBroker.GetWeatherForecastsAsync();
            this.NotifyListChanged(this.Records, EventArgs.Empty);
        }

        public async Task AddRecord(WeatherForecast record)
        {
            await weatherForecastDataBroker.AddForecastAsync(record);
            await GetForecastsAsync();
        }

        public async Task DeleteRecord(Guid Id)
        {
            await weatherForecastDataBroker.DeleteForecastAsync(Id);
            await GetForecastsAsync();
        }

        public event EventHandler<EventArgs> ListChanged;

        public void NotifyListChanged(object sender, EventArgs e)
            => ListChanged?.Invoke(sender, e);
    }
}
