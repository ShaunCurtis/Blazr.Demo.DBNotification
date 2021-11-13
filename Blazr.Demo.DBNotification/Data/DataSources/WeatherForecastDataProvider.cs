/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Blazr.Demo.DBNotification.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazr.Demo.DBNotification.Data
{
    /// <summary>
    /// A dummy data provider using Task.Delay to emulate async behaviour
    /// Normally replaced with an EF or other data base layer
    /// </summary>
    public class WeatherForecastDataProvider
    {
        private int _recordsToGet = 5;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private List<WeatherForecast> _records;

        public WeatherForecastDataProvider()
            =>
            _records = GetForecasts();

        private List<WeatherForecast> GetForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, _recordsToGet).Select(index => new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList();
        }

        public async Task<bool> AddForecastAsync(WeatherForecast record)
        {
            await Task.Delay(500);
            _records.Add(record);
            return true;
        }

        public async Task<bool> DeleteForecastAsync(Guid Id)
        {
            await Task.Delay(500);
            var deleted = false;
            var record = _records.FirstOrDefault(item => item.Id == Id);
            if (record != null)
            {
                _records.Remove(record);
                deleted = true;
            }
            return deleted;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastsAsync()
        {
            await Task.Delay(1000);
            var list = new List<WeatherForecast>();
            _records
                .ForEach(item => list.Add(item with { }));
            return list;
        }
    }
}
