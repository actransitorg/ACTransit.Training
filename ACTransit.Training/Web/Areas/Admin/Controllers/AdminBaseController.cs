using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Infrastructure;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{
    [CustomError]
    [CustomAuthorize(Token="Admin")]
    public abstract class AdminBaseController<T> : Web.Controllers.BaseController<T> where T:BaseService,new()
    {
        protected AdminBaseController()
            : base("Admin.")
        {
        }

        public virtual ActionResult Index()
        {
            return View();
        }
        
    }
}