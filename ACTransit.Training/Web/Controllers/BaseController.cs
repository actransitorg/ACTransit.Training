using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Infrastructure;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Controllers
{
    [CustomError]
    public abstract class BaseController<T> : Controller where T:BaseService,new()
    {
        protected T Service;

        [HttpPost]
        [OutputCache(CacheProfile = "courseTypeCache")]
        public virtual JsonResult CourseTypes()
        {
            var courseTypes = Service.GetCourseTypes(m=>m.IsActive);
            return Json(courseTypes);
        }

        protected readonly Logger Logger;
        protected BaseController()
        {
            Logger=new Logger(GetType().Name);
            Service = new T();
        }

        protected BaseController(string loggerPrefixName)
        {
            Logger = new Logger(loggerPrefixName + GetType().Name);
            Service = new T();
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (Service != null)
            {
                Service.Dispose();
                Service = null;
            }                
        }
    }
}