using System;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class KphUnitConverter : IWindSpeedUnitConverter
    {
        /// <summary>
        /// This first "converter" only knows about km/h and therefore no actual conversion is needed.
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public double Convert(double sourceValue, WindSpeedUnit sourceUnit, WindSpeedUnit targetUnit)
        {
            if (sourceUnit != WindSpeedUnit.Kph) throw new ArgumentException("Unexpected source unit");

            if (targetUnit != WindSpeedUnit.Kph) throw new ArgumentException("Unexpected target unit");

            return sourceValue;
        }
    }
}
