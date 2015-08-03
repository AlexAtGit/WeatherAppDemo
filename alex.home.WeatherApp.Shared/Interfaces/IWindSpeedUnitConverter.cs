namespace alex.home.WeatherApp.Shared
{
    public interface IWindSpeedUnitConverter
    {
        double Convert(double sourceValue, WindSpeedUnit sourceUnit, WindSpeedUnit targetUnit);
    }
}
