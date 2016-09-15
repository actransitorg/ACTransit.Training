using System.Web.Mvc;
using ACTransit.Training.Web.Infrastructure;

namespace ACTransit.Training.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoggerAttribute());
            filters.Add(new CustomErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
