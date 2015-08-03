using System;
using System.Collections.Generic;

namespace alex.home.WeatherApp.Shared
{
    public class WeatherForecast
    {
        public List<Reading>    Readings                { get; set; }

        public DateTime         TimeStamp               { get; set; }
        public string           Location                { get; set; }

        public double           AverageTemperature      { get; set; }
        public TemperatureUnit  TemperatureUnit         { get; set; }

        public double           AverageWindSpeed        { get; set; }
        public WindSpeedUnit    WindSpeedUnit           { get; set; }

        public WeatherForecast()
        {
            Readings = new List<Reading>();
        }
    }
}
