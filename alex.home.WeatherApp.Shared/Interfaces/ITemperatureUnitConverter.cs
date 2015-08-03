namespace alex.home.WeatherApp.Shared
{
    public interface ITemperatureUnitConverter
    {
        double Convert(double sourceValue, TemperatureUnit sourceUnit, TemperatureUnit targetUnit);
    }
}
