/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System;

namespace Blazr.Demo.DBNotification.Core
{
    public record WeatherForecast
    {
        public Guid Id { get; init; }

        public DateTime Date { get; init; }

        public int TemperatureC { get; init; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; init; }
    }
}
