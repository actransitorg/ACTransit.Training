using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ACTransit.Training.Web.Infrastructure;
using ACTransit.Training.Web.Models;
using ACTransit.Training.Web.App_Start;

namespace ACTransit.Training.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Logger _logger;
        public MvcApplication()
        {
            SetDirectory();
            //HACK: Due to IIS Shutdown/Start event timings, Log4Net config file needs to be loaded during the applicatin's constructor, otherwise no log file may be generated.
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Log4net.config"));
            _logger = new Logger(this.GetType());

        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DatabaseWarmup.Startup();
        } 

        protected void Application_End()
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                _logger.Fatal(exception.Message, exception);

                //Send an email if the error is anything other than Not Found.
                if (exception is HttpException && ((HttpException)exception).GetHttpCode() == (int)HttpStatusCode.NotFound)
                    return;

                Email.EmailError(exception);
            }
        }
        private string SetDirectory()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, "");
            var result = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", result);
            return Path.GetFullPath(path);
        }

    }
}
