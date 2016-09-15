using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{
    public class NonEmployeeController : AdminBaseController<NonEmployeeServiceDomain>
    {
        public override ActionResult Index()
        {
            var model = Service.GetNonEmployeesViewModel(null);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(NonEmployeesPageViewModel model)
        {
            model = Service.GetNonEmployeesViewModel(model);
            return View("Index", model);
        }

        public ActionResult New()
        {
            var model = Service.GetNonEmployeeViewModel(null);
            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var model = Service.GetNonEmployeeViewModel(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Save(NonEmployeeViewModel model)
        {
            if (!ModelState.IsValid || !Service.ValidateModel(model, ModelState))
                return View("Edit", model);

            model = Service.SaveNonEmployeeViewModel(model);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(long id)
        {
            Service.DeleteNonEmployee(id);
            return Json(new JsonGeneric(true, "", null));
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetNonEmployees()
        {
            var result = Service.GetNonEmployees();
            return Json(result);
        }
    }
}