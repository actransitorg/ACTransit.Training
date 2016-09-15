using System;
using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Infrastructure;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Controllers
{
    [CustomAuthorize(Token = "CourseEnrollment")]
    public class CourseEnrollmentController : BaseController<CourseEnrollmentServiceDomain>
    {
        [AllowAnonymous]
        public ActionResult Index(long? coursescheduleId)
        {
            if (coursescheduleId != null)
            {
                using (var csService = new CourseScheduleServiceDomain())
                {
                    var cs = csService.GetModel(coursescheduleId.GetValueOrDefault());
                    if (cs != null)
                    {
                        ViewBag.CourseTypeId = cs.CourseTypeId;
                        ViewBag.CourseId = cs.CourseSchedule.CourseId;
                        ViewBag.CourseScheduleId = cs.CourseSchedule.CourseScheduleId;
                        ViewBag.BeginEffDateStr = cs.CourseSchedule.BeginEffDateStr;
                        ViewBag.EndEffDateStr = cs.CourseSchedule.EndEffDateStr;
                    }
                }                
            }
            return View("Index");
        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetContacts(string badge, string lastName, string firstName)
        {
            var results = Service.GetEmployees(badge, lastName, firstName);
            return Json(results);
        }

      

        //-------------------------Ajax parts
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult GetCourseEnrollmentsPage(CourseEnrollmentsPageViewModelAjax model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            model = Service.GetCourseEnrollmentsPageViewModel(model);
            return Json(model);
        }

        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult GetCourseSchedules(CourseEnrollmentsPageViewModelAjax model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            var result = Service.GetCourseSchedulesViewModel(model);
            return Json(result);
        }

        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult GetCourseEnrollments(CourseEnrollmentsPageViewModelAjax model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            var result = Service.GetCourseEnrollmentsViewModel(model);
            return Json(result);
        }

        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult CourseEnrollemntViewModel(long? id, long? courseScheduleId)
        {
            var model = Service.GetCourseEnrollmentViewModel(id, courseScheduleId);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        public JsonResult CourseEnrollemntViewModel(CourseEnrollmentPageViewModelAjax model)
        {
            var success = false;
            var message = "";
            if (model==null)
                throw new Exception("Wrong parameters sent to GetCourseEnrollemntViewModel");

            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);

            model = Service.SaveModel(model);
            success = true;

            return Json(new JsonGeneric(success, message, model));
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpDelete]
        public JsonResult Delete(long id)
        {
            Service.DeleteCourseEnrollment(id);
            return Json(new JsonGeneric(true, "", null));
        }
    }
}