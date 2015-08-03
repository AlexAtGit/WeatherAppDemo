using System;

namespace alex.home.WeatherApp.Shared
{
    [Serializable]
    public class WeatherSource
    {
        public bool             IsEnabled       { get; set; }
        public string           Name            { get; set; }
        public string           BaseUrl         { get; set; }
        public TemperatureUnit  TemperatureUnit { get; set; } // Measurement unit
        public WindSpeedUnit    WindSpeedUnit   { get; set; } // Measurement unit
    }
}
