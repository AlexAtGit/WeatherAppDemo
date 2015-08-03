using System;
using System.Web.Mvc;
using System.Threading.Tasks;

using alex.home.WeatherApp.BLL;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly Type       _thisClass = typeof(HomeController);

        private IRepository         _repository;
        private IWeatherProvider    _weatherProvider;
        private IUnitConverter      _unitConverter;

        #endregion Fields

        public HomeController(IRepository repository, IWeatherProvider weatherProvider, IUnitConverter unitConverter)
        {
            _repository      = repository;
            _weatherProvider = weatherProvider;
            _unitConverter   = unitConverter;
        }

        public ActionResult Index()
        {
            return RedirectToAction("WeatherForecast", "Home");
        }

        public async Task<ActionResult> WeatherForecast(WeatherForecast weatherForecast)
        {
            if (weatherForecast != null && !string.IsNullOrWhiteSpace(weatherForecast.Location))
            {
                var rootFolder = Server.MapPath("~/App_Data");
                var settings = _repository.LoadSettings(rootFolder);

                try
                {
                    weatherForecast.Readings = await _weatherProvider.GetAllReadings(weatherForecast.Location, settings.WeatherSources);

                    EvaluateAverageReading(weatherForecast);
                }
                catch (Exception ex)
                {
                    LoggerManager.WriteError(_thisClass, ex.Message);
                }
            }
            return View(weatherForecast); 
        }

        public void EvaluateAverageReading(WeatherForecast weatherForecast)
        {
            if (weatherForecast.Readings.Count == 0) return;

            weatherForecast.AverageTemperature = weatherForecast.AverageWindSpeed = 0.0;

            foreach (var reading in weatherForecast.Readings)
            {
                double temperatureValue = _unitConverter.Convert(reading.TemperatureValue, reading.TemperatureUnit, weatherForecast.TemperatureUnit);
                double windSpeedValue   = _unitConverter.Convert(reading.WindSpeedValue,   reading.WindSpeedUnit,   weatherForecast.WindSpeedUnit);

                if (!double.IsNaN(temperatureValue)) weatherForecast.AverageTemperature += temperatureValue;
                if (!double.IsNaN(windSpeedValue))   weatherForecast .AverageWindSpeed  += windSpeedValue;
            }

            weatherForecast.AverageTemperature /= weatherForecast.Readings.Count;
            weatherForecast.AverageWindSpeed   /= weatherForecast.Readings.Count;
        }
    }
}