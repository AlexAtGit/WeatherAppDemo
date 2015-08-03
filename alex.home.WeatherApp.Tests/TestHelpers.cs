using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.Tests
{
    public static class TestHelpers
    {
        public static string GetAppDataFolder()
        {
            var dataFolder = @"C:\DEVELOPMENT\HomeProjects\WeatherApp2013\alex.home.WeatherApp.Web\App_Data";

            // The code below should work when running NUnit within the solution
            //var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            //dataFolder = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            //var slash = dataFolder.LastIndexOf('\\');
            //if (slash != -1) dataFolder = dataFolder.Substring(0, slash);
            //dataFolder = Path.Combine(dataFolder, "App_Data");

            return dataFolder;
        }
        public static Settings SetupDefaultSettings()
        {
            var settings = new Settings();

            settings.WeatherSources.Add(new WeatherSource
            {
                Name = "BBC Weather",
                BaseUrl = "http://localhost:17855/",
                TemperatureUnit = TemperatureUnit.Celsius,
                WindSpeedUnit = WindSpeedUnit.Kph,
                IsEnabled = true
            });

            settings.WeatherSources.Add(new WeatherSource
            {
                Name = "Accu Weather",
                BaseUrl = "http://localhost:18888/",
                TemperatureUnit = TemperatureUnit.Fahrenheit,
                WindSpeedUnit = WindSpeedUnit.Mph,
                IsEnabled = true
            });

            return settings;
        }
    }
}
