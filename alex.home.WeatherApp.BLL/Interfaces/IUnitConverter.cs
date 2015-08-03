using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public interface IUnitConverter
    {
        double Convert(double sourceValue, TemperatureUnit sourceUnit, TemperatureUnit targetUnit);
        double Convert(double sourceValue, WindSpeedUnit   sourceUnit, WindSpeedUnit   targetUnit);
    }
}
