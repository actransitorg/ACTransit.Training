using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{
    public class InstructorController : AdminBaseController<InstructorServiceDomain>
    {

        public override ActionResult Index()
        {
            var model = Service.GetInstructorsPageViewModel(null);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index(InstructorsPageViewModel model)
        {
            model = Service.GetInstructorsPageViewModel(model);
            return View("Index", model);
        }

        public ActionResult New()
        {
            var model = Service.GetInstructorPageViewMode(null);
            return View("Edit", model);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return RedirectToAction("New");

            var model = Service.GetInstructorPageViewMode(id);
            return View("Edit",model);
        }

        [HttpPost]
        public ActionResult Save(InstructorPageViewModel model)
        {
            if (!Service.ValidateModel(model, ModelState)|| !ModelState.IsValid)
                return View("Edit", model);

            model = Service.SaveInstructorPageViewMode(model);
            return RedirectToAction("Index");            
        }

        [HttpDelete]
        public JsonResult Delete(long id)
        {

            Service.DeleteInstructor(id);
            return Json(new JsonGeneric(true, "", null));
        }
    }
}