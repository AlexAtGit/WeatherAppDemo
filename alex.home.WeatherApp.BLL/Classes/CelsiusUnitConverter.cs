using System;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class CelsiusUnitConverter : ITemperatureUnitConverter
    {
        /// <summary>
        /// This first "converter" only knows about degC and therefore no actual conversion is needed.
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public double Convert(double sourceValue, TemperatureUnit sourceUnit, TemperatureUnit targetUnit)
        {
            if (sourceUnit != TemperatureUnit.Celsius) throw new ArgumentException("Unexpected source unit");

            if (targetUnit != TemperatureUnit.Celsius) throw new ArgumentException("Unexpected target unit");

            return sourceValue;
        }
    }
}
