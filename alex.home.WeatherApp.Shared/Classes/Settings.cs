using System.Collections.Generic;

namespace alex.home.WeatherApp.Shared
{
    public class Settings 
    {
        public Settings()
        {
            WeatherSources = new List<WeatherSource>();
        }

        public List<WeatherSource> WeatherSources { get; set; }
    }
}
