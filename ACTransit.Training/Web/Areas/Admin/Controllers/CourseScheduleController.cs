using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{
    public class CourseScheduleController : AdminBaseController<CourseScheduleServiceDomain>
    {

        public override ActionResult Index()
        {
            var model = Service.GetModel(null);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CourseSchedulesViewModel model)
        {
            model = Service.GetModel(model);
            return View(model);
        }
        

        public ActionResult New()
        {
            ViewBag.Id = 0;
            return View("Edit");
        }

        public ActionResult Edit(long? id = null)
        {
            if (id == null)
                return RedirectToAction("New");
            ViewBag.Id = id.Value;
            return View("Edit");
        }

        public JsonResult Save(CourseSchedulePageViewModel model)
        {
            Service.ValidateModel(model,ModelState);
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            Service.SaveModel(model);
            return Json(new JsonGeneric(true, "", null));
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Service.DeleteModel(id);
            return Json(new JsonGeneric(true,"",null));
        }


        [HttpPost]
        public JsonResult GetModel(long id)
        {
            var model = Service.GetModel(id);
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetDivisions(long courseScheduleId)
        {
            var divisions = Service.GetDivisions(courseScheduleId);
            return Json(divisions);
        }

        [HttpPost]
        public JsonResult GetInstructors(long? courseScheduleId, long courseTypeId)
        {
            var instructors = Service.GetInstructors(courseScheduleId, courseTypeId);
            return Json(instructors);
        }
        
        [HttpPost]
        public JsonResult GetCourseTypes(long courseScheduleId)
        {
            var courseTypes = Service.GetCourseTypes(courseScheduleId);
            return Json(courseTypes);
        }

        [HttpPost]
        public JsonResult GetCourses(long? courseScheduleId, long courseTypeId)
        {
            var courses= Service.GetCourses(courseScheduleId, courseTypeId);
            return Json(courses);
        }
    }
}