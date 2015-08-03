using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;

using alex.home.WeatherApp.Shared;

namespace alex.home.WeatherApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var rootFolder = Server.MapPath("~/Logs");
            var logFilePath = Path.Combine(rootFolder, Definitions.DefaultLogFile);

            LoggerManager.Initialise(logFilePath);
            LoggerManager.WriteInfo(typeof(MvcApplication), "Started the webApp. Version No: {0} -------------------------------------", Utility.GetVersionNo());


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig.ConfigureContainer();
        }
        void Application_End(object sender, EventArgs e)
        {
            LoggerManager.WriteInfo(typeof(MvcApplication), "Ended the webApp");
        }
        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            LoggerManager.WriteError(typeof(MvcApplication), exception.Message);
        }
    }
}
