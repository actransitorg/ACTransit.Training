using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Services;

namespace ACTransit.Training.Web.Controllers
{
    public class EmployeeSearchController : BaseController<EmployeeServiceDomain>
    {
        [AllowAnonymous]
        [HttpPost]
        [OutputCache(Duration=60, VaryByParam = "*")]
        public JsonResult GetContacts(string badge, string lastName, string firstName,string name)
        {
            var results = Service.GetEmployees(badge, lastName, firstName,name,name);
            return Json(results);
        }
    }
}