using System;
using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class MphUnitConverter : IWindSpeedUnitConverter
    {
        /// <summary>
        /// This converter converts from kph to mph and vice-versa
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public double Convert(double sourceValue, WindSpeedUnit sourceUnit, WindSpeedUnit targetUnit)
        {
            double targetValue = sourceValue;

            // If this is the same unit, then return the same value
            if (targetUnit == sourceUnit) return targetValue;

            if (targetUnit == WindSpeedUnit.Kph) 
            {
                if (sourceUnit == WindSpeedUnit.Mph) 
                {
                    targetValue = (sourceValue * 8.0) / 5.0;                }
                else
                {
                    throw new ArgumentException("Unexpected source unit");
                }
            }
            else if (targetUnit == WindSpeedUnit.Mph) 
            {
                if (sourceUnit == WindSpeedUnit.Kph)
                {
                    targetValue = (sourceValue * 5.0) / 8.0;
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
