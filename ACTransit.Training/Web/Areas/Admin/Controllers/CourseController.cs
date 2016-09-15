using System;
using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{    
    public class CourseController : AdminBaseController<CourseServiceDomain>
    {
        public override ActionResult Index()
        {
            var model = Service.GetModel(null);
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Show()
        {
            var model = Service.GetModel(null);
            if (model != null)
                model.Readonly = true;
            return View("Index",model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Show(CoursesViewModel model)
        {
            model = Service.GetModel(model);
            if (model != null)
                model.Readonly = true;
            return View("Index", model);
        }

        [HttpPost]        
        [ActionName("Index")]
        public ActionResult Index1(CoursesViewModel model)
        {
            model = Service.GetModel(model);
            return View(model);
        }
        [HttpGet]
        public ActionResult New()
        {
            ViewBag.Id = 0;
            return View("Edit");
        }
        [HttpGet]
        public ActionResult Edit(long? id = null)
        {
            if (id == null)
                return RedirectToAction("New");
            ViewBag.Id = id.Value;
            return View("Edit");
        }

        [HttpPost]
        public JsonResult GetCourse(long? id)
        {
            var topics = Service.GetCoursePageViewModel(id);
            return Json(topics);
        }


        [HttpPost]
        public JsonResult UpdateCourse(CoursePageViewModel model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            if (model == null)
                throw new ArgumentException("Parameter model can't be null");
            

            model.State=ViewModelState.Edit;
            var courseId = Service.SaveCourse(model);
            return Json(courseId);
        }
        [HttpPost]
        public JsonResult AddCourse(CoursePageViewModel model)
        {
            if (!ModelState.IsValid)
                throw new FriendlyException(ModelState);
            if (model == null)
                throw new ArgumentException("Parameter model can't be null");
            model.State = ViewModelState.New;

            var courseId=Service.SaveCourse(model);
            return Json(courseId);
         
        }

        [HttpPost]
        public virtual JsonResult Topics(long courseTypeId, long? courseId)
        {
            var topics = Service.GetTopics(courseTypeId, courseId);
            return Json(topics);
        }

        [HttpPost]
        public virtual JsonResult ComponentTopics(long courseTypeId, long? courseId)
        {
            var topics = Service.GetComponentTopics(courseTypeId, courseId);
            return Json(topics);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            Service.DeleteCourse(id);         
            return Json(new JsonGeneric(true,"",null));
        }
    }
}