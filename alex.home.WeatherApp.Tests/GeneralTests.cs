using FluentAssertions;
using NUnit.Framework;

using alex.home.WeatherApp.BLL;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.Tests
{
    [TestFixture]
    public class GeneralTests
    {
        #region Fields

        private IRepository _repository;

        #endregion Fields

        #region Setup & TearDown

        [TestFixtureSetUp]
        public void InitFixture()
        {
        }
        [TestFixtureTearDown]
        public void CleanupFixture()
        {
        }

        [SetUp]
        public void Init()
        {
            _repository = new Repository();
        }
        [TearDown]
        public void Cleanup()
        {
        }

        #endregion Setup & TearDown

        [Test]
        public void LoadSettings()
        {
            var settings = _repository.LoadSettings(TestHelpers.GetAppDataFolder());

            settings.WeatherSources.Count.Should().Be(2);

            settings.WeatherSources[0].Name.Should().Be("BBC Weather");
            settings.WeatherSources[0].TemperatureUnit.Should().Be(TemperatureUnit.Celsius);
            settings.WeatherSources[0].WindSpeedUnit.Should().Be(WindSpeedUnit.Kph);

            settings.WeatherSources[1].Name.Should().Be("Accu Weather");
            settings.WeatherSources[1].TemperatureUnit.Should().Be(TemperatureUnit.Fahrenheit);
            settings.WeatherSources[1].WindSpeedUnit.Should().Be(WindSpeedUnit.Mph);
        }
    }
}
