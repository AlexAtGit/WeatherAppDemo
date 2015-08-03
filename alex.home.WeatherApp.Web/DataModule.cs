using Autofac;

using alex.home.WeatherApp.BLL;

namespace alex.home.WeatherApp.Web
{
    public class DataModule : Module
    {
        public DataModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WeatherProvider>().As<IWeatherProvider>().InstancePerRequest();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerRequest();
            builder.RegisterType<UnitConverter>().As<IUnitConverter>().InstancePerLifetimeScope();            

            base.Load(builder);
        }
    }
}