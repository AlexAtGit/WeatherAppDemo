using System;
using System.IO;
using System.Reflection;

using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class Repository : IRepository
    {
        public Settings LoadSettings(string rootFolder)
        {
            var settings = new Settings();
            if (Directory.Exists(rootFolder))
            {
                var settingsFile = Path.Combine(rootFolder, Definitions.SettingFile);
                try
                {
                    settings = Utility.LoadXML<Settings>(settingsFile);
                }
                catch (Exception ex)
                {
                    LoggerManager.WriteError(typeof(Repository), ex.Message);
                }

                if (settings == null || settings.WeatherSources.Count == 0) settings = SetupDefaultSettings(settingsFile);
            }

            return settings;
        }

        private Settings SetupDefaultSettings(string settingsFile)
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

            // Create the first settings file
            Utility.SaveXML<Settings>(settingsFile, settings);
            return settings;
        }
    }
}
