using System.Collections.Generic;

using FluentAssertions;
using NUnit.Framework;
using Moq;

using alex.home.WeatherApp.BLL;
using alex.home.WeatherApp.Shared;
using alex.home.WeatherApp.Web.Controllers;

namespace alex.home.WeatherApp.Tests
{
    [TestFixture]
    public class WeatherTests
    {
        [Test]
        public async void GetReadings()
        {
            var location = "London";
            var testReadings = new List<Reading>();
            testReadings.Add(new Reading { Location = location, WeatherSourceName = "BBC Weather", TemperatureValue = 10.0, TemperatureUnit = TemperatureUnit.Celsius, WindSpeedValue = 8.0, WindSpeedUnit = WindSpeedUnit.Kph });
            testReadings.Add(new Reading { Location = location, WeatherSourceName = "Accu Weather", TemperatureValue = 68.0, TemperatureUnit = TemperatureUnit.Fahrenheit, WindSpeedValue = 10.0, WindSpeedUnit = WindSpeedUnit.Mph });
             
            Mock<IWeatherProvider> mock = new Mock<IWeatherProvider>();
            mock.Setup(x => x.GetAllReadings(It.IsAny<string>(), It.IsAny<List<WeatherSource>>())).ReturnsAsync(testReadings);

            IRepository repository = new Repository();
            var settings = repository.LoadSettings(TestHelpers.GetAppDataFolder());
            var readings = await mock.Object.GetAllReadings(location, settings.WeatherSources);

            readings.Count.Should().Be(2);
            readings[0].TemperatureValue.Should().Be(10);
            readings[0].TemperatureUnit.Should().Be(TemperatureUnit.Celsius);
            readings[1].WindSpeedValue.Should().Be(10);
            readings[1].WindSpeedUnit.Should().Be(WindSpeedUnit.Mph);
        }

        [Test]
        public void GetAverageReading()
        {
            var location = "London";
            var testReadings = new List<Reading>();
            testReadings.Add(new Reading { Location = location, WeatherSourceName = "BBC Weather", TemperatureValue = 10.0, TemperatureUnit = TemperatureUnit.Celsius, WindSpeedValue = 8.0, WindSpeedUnit = WindSpeedUnit.Kph });
            testReadings.Add(new Reading { Location = location, WeatherSourceName = "Accu Weather", TemperatureValue = 68.0, TemperatureUnit = TemperatureUnit.Fahrenheit, WindSpeedValue = 10.0, WindSpeedUnit = WindSpeedUnit.Mph });

            Mock<IWeatherProvider> mockWeatherProvider = new Mock<IWeatherProvider>();
            mockWeatherProvider.Setup(x => x.GetAllReadings(It.IsAny<string>(), It.IsAny<List<WeatherSource>>())).ReturnsAsync(testReadings);

            var testSettings = TestHelpers.SetupDefaultSettings();
            Mock<IRepository> mockRepository = new Mock<IRepository>();
            mockRepository.Setup(x => x.LoadSettings(It.IsAny<string>())).Returns(testSettings);

            IUnitConverter unitConverter = new UnitConverter();
            var homeController = new HomeController(mockRepository.Object, mockWeatherProvider.Object, unitConverter);

            //Given temperatures of 10c from bbc and 68f from accuweather when searching then display either 15c or 59f (the average).
            var weatherForecast1 = new WeatherForecast { Readings = testReadings, TemperatureUnit = TemperatureUnit.Celsius, WindSpeedUnit = WindSpeedUnit.Kph };
            homeController.EvaluateAverageReading(weatherForecast1);

            weatherForecast1.AverageTemperature.Should().BeApproximately(15, 0.001);
            weatherForecast1.AverageWindSpeed.Should().BeApproximately(12, 0.001);

            // Given wind speeds of 8kph from bbc and 10mph from accuweather when searching then display either 12kph or 7.5mph (the average).
            var weatherForecast2 = new WeatherForecast { Readings = testReadings, TemperatureUnit = TemperatureUnit.Fahrenheit, WindSpeedUnit = WindSpeedUnit.Mph };
            homeController.EvaluateAverageReading(weatherForecast2);

            weatherForecast2.AverageTemperature.Should().BeApproximately(59, 0.001);
            weatherForecast2.AverageWindSpeed.Should().BeApproximately(7.5, 0.001);
        }

    }
}
