using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;
using ACTransit.Training.Web.Infrastructure;

namespace ACTransit.Training.Web.Controllers
{
    [CustomAuthorize(Token = "Enrollment")]
    public class EnrollmentController : BaseController<EnrollmentServiceDomain>
    {
        // GET: Entollments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetModel(EnrollmentPageViewModel model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            model = Service.GetModel(model);
            return Json(model);
        }

        [HttpPost]
        public JsonResult Save(EnrollmentPageViewModel model)
        {
            if (!Service.ValidateModel(model, ModelState))
                throw new FriendlyException(ModelState);

            Service.SaveModel(model);
            return Json(new JsonGeneric(true, "", null));
        }

        [HttpDelete]
        public JsonResult Delete(long courseEnrollmentId)
        {
            Service.DeleteEnrollment(courseEnrollmentId);
            return Json(new JsonGeneric(true, "", null));
        }
    }
}