using System;

namespace alex.home.WeatherApp.Shared
{
    public class Reading
    {
        public string           WeatherSourceName   { get; set; }

        public DateTime         TimeStamp           { get; set; }
        public string           Location            { get; set; }

        public double           TemperatureValue    { get; set; }
        public TemperatureUnit  TemperatureUnit     { get; set; }

        public double           WindSpeedValue      { get; set; }
        public WindSpeedUnit    WindSpeedUnit       { get; set; }

        public override string ToString()
        {
            return WeatherSourceName + ", "
                 + TimeStamp.ToShortDateString() + ", "
                 + Location + ", "
                 + TemperatureValue.ToString() + " "
                 + TemperatureUnit.ToString() + ", "
                 + WindSpeedValue.ToString() + " "
                 + WindSpeedUnit.ToString();
        }
    }
}

