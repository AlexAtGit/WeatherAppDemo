using System;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class FahrenheitUnitConverter : ITemperatureUnitConverter
    {
        /// <summary>
        /// Convert from Celsius to Fahrenheit and vice-versa
        /// Celsius    to Fahrenheit:   (°C × 9/5) + 32 = °F
        /// Fahrenheit to Celsius:      (°F − 32) x 5/9 = °C
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public double Convert(double sourceValue, TemperatureUnit sourceUnit, TemperatureUnit targetUnit)
        {
            double targetValue = sourceValue;

            // If this is the same unit, then return the same value
            if (targetUnit == sourceUnit) return targetValue;

            if (targetUnit == TemperatureUnit.Celsius) 
            {
                if (sourceUnit == TemperatureUnit.Fahrenheit) 
                {
                    // Fahrenheit to Celsius: (°F − 32) x 5/9 = °C
                    targetValue = ((sourceValue - 32.0) * 5.0) / 9.0;
                }
                else
                {
                    throw new ArgumentException("Unexpected source unit");
                }
            }
            else if (targetUnit == TemperatureUnit.Fahrenheit) 
            {
                if (sourceUnit == TemperatureUnit.Celsius)
                {
                    // Celsius    to Fahrenheit:   (°C × 9/5) + 32 = °F
                    targetValue = 32.0 + ((sourceValue * 9.0) / 5.0);
                }
                else
                {
                    throw new ArgumentException("Unexpected source unit");
                }
            }
            else
            {
                throw new ArgumentException("Unexpected source/target unit");
            }

            return targetValue;
        }
    }
}
