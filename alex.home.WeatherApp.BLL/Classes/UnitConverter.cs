using System;
using System.Collections.Generic;

using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public class UnitConverter : IUnitConverter
    {
        private Dictionary<TemperatureUnit, ITemperatureUnitConverter> _temperatureConverters = new Dictionary<TemperatureUnit, ITemperatureUnitConverter>();
        private Dictionary<WindSpeedUnit,   IWindSpeedUnitConverter>   _windSpeedConverters   = new Dictionary<WindSpeedUnit,   IWindSpeedUnitConverter>();

        public UnitConverter()
        {
            _temperatureConverters.Add(TemperatureUnit.Celsius,    new CelsiusUnitConverter());
            _temperatureConverters.Add(TemperatureUnit.Fahrenheit, new FahrenheitUnitConverter());

            _windSpeedConverters.Add(WindSpeedUnit.Kph, new KphUnitConverter());
            _windSpeedConverters.Add(WindSpeedUnit.Mph, new MphUnitConverter());
        }

        public double Convert(double sourceValue, TemperatureUnit sourceUnit, TemperatureUnit targetUnit)
        {
            double targetValue = double.NaN;

            ITemperatureUnitConverter temperatureUnitConverter;
            if (_temperatureConverters.TryGetValue(sourceUnit, out temperatureUnitConverter))
            {
                try
                {
                    targetValue = temperatureUnitConverter.Convert(sourceValue, sourceUnit, targetUnit);
                }
                catch (ArgumentException ex)
                {
                    // OK the above sourceUnit-related converter could not convert to teh target unit,
                    // so try the targetUnit-related converter instead
                    if (_temperatureConverters.TryGetValue(targetUnit, out temperatureUnitConverter))
                    {
                        try
                        {
                            targetValue = temperatureUnitConverter.Convert(sourceValue, sourceUnit, targetUnit);
                        }
                        catch (ArgumentException ex2)
                        {
                            LoggerManager.WriteError(typeof(UnitConverter), ex2.Message);
                        }
                    }
                }
            }

            return targetValue;
        }
        public double Convert(double sourceValue, WindSpeedUnit sourceUnit, WindSpeedUnit targetUnit)
        {
            double targetValue = double.NaN;

            IWindSpeedUnitConverter windSpeedUnitConverter;
            if (_windSpeedConverters.TryGetValue(sourceUnit, out windSpeedUnitConverter))
            {
                try
                {
                    targetValue = windSpeedUnitConverter.Convert(sourceValue, sourceUnit, targetUnit);
                }
                catch (ArgumentException ex)
                {
                    // OK the above sourceUnit-related converter could not convert to teh target unit,
                    // so try the targetUnit-related converter instead
                    if (_windSpeedConverters.TryGetValue(targetUnit, out windSpeedUnitConverter))
                    {
                        try
                        {
                            targetValue = windSpeedUnitConverter.Convert(sourceValue, sourceUnit, targetUnit);
                        }
                        catch (ArgumentException ex2)
                        {
                            LoggerManager.WriteError(typeof(UnitConverter), ex2.Message);
                        }
                    }
                }
            }

            return targetValue;
        }
    }
}
