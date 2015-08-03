using System.Threading.Tasks;
using System.Collections.Generic;

using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public interface IWeatherProvider
    {
        Task<List<Reading>> GetAllReadings(string location, List<WeatherSource> weatherSources);
    }
}
