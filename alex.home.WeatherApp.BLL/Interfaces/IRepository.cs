using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.BLL
{
    public interface IRepository
    {
        Settings LoadSettings(string rootFolder);
    }
}
