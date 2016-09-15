using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: PreApplicationStartMethod(typeof(ACTransit.Training.Web.Domain.Infrastructure.AppStart), "Start")]

namespace ACTransit.Training.Web.Domain.Infrastructure
{
    public class AppStart
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(SetFactoryModule));
        }
    }

    public class SetFactoryModule : IHttpModule
    {
        static SetFactoryModule()
        {
            var currentFactory = ControllerBuilder.Current.GetControllerFactory();
            if (!(currentFactory is CookieTempDataControllerFactory))
            {
                ControllerBuilder.Current.SetControllerFactory(new CookieTempDataControllerFactory(currentFactory));
            }
        }

        public void Init(HttpApplication app)
        {
        }

        public void Dispose()
        {
        }
    }
}
