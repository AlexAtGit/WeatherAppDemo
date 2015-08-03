using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

using alex.home.WeatherApp.Shared;
using Newtonsoft.Json.Linq;

namespace alex.home.WeatherApp.BLL
{
    public class WeatherProvider : IWeatherProvider
    {
        private readonly Type _thisClass = typeof(WeatherProvider);

        public async Task<List<Reading>> GetAllReadings(string location, List<WeatherSource> weatherSources)
        {
            var readings = new List<Reading>();
            if (string.IsNullOrWhiteSpace(location) || weatherSources == null) return readings;
            
            foreach (var weatherSource in weatherSources)
            {
                if (!weatherSource.IsEnabled) continue;

                try
                {
                    var reading = await GetReading(location, weatherSource);
                    LoggerManager.WriteInfo(_thisClass, reading.ToString());

                    readings.Add(reading);
                }
                catch (Exception ex)
                {
                    LoggerManager.WriteWarning(_thisClass, weatherSource.Name);
                    LoggerManager.WriteError(_thisClass, ex.Message);
                }
            }            

            return readings;
        }

        private async Task<Reading> GetReading(string location, WeatherSource weatherSource)
        {
            Reading reading = null;

            using (var client = new HttpClient())
            {
                // Send HTTP requests: e.g. http://localhost:17855/api/Weather/Bournemouth
                client.BaseAddress = new Uri(weatherSource.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Get the reading back
                HttpResponseMessage response = await client.GetAsync("api/Weather/" + location);
                response.EnsureSuccessStatusCode();  

                // Extract the actual measurements from the JSON response
                var jsonReading = await response.Content.ReadAsStringAsync();

                dynamic data = JObject.Parse(jsonReading);

                var tempParam = "Temperature" + weatherSource.TemperatureUnit.ToString();
                var wsParam   = "WindSpeed"   + weatherSource.WindSpeedUnit.ToString();

                reading = new Reading
                {
                    WeatherSourceName   = weatherSource.Name,

                    TimeStamp           = DateTime.Now,
                    Location            = location,

                    TemperatureValue    = (double) data[tempParam],
                    TemperatureUnit     = weatherSource.TemperatureUnit,
                    WindSpeedValue      = (double)data[wsParam], // or data.WindSpeedKph,
                    WindSpeedUnit       = weatherSource.WindSpeedUnit
                };
            }

            return reading;
        }
    }
}
