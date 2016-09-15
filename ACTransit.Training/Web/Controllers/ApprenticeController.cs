using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTransit.Framework.Logging;
using ACTransit.Training.Web.Domain.Apprentice.Models;
using ACTransit.Training.Web.Domain.Apprentice.Services;
using ACTransit.Training.Web.Infrastructure;

namespace ACTransit.Training.Web.Controllers
{
    [CustomAuthorize(Token = "ApprenticeFeature")]
    public class ApprenticeController : BaseController<ApprenticeServiceDomain>
    {
        private static readonly Logger logger = new Logger(typeof(ApprenticeController).Name);

        public ActionResult Index(bool? OnlyActive, DateTime? StartDate, DateTime? EndDate)
        {
            var onlyActive = !OnlyActive.HasValue || OnlyActive.Value;
            return Programs(onlyActive, StartDate, EndDate);
        }

        public ActionResult Programs(bool? OnlyActive, DateTime? StartDate, DateTime? EndDate)
        {
            var model = Service.GetProgramsViewModel(OnlyActive.GetValueOrDefault(), StartDate, EndDate);
            return View("Programs", model);
        }

        public PartialViewResult Program(int id, bool? OnlyActive, DateTime? StartDate, DateTime? EndDate)
        {
            var model = Service.GetProgramViewModel(id, OnlyActive.GetValueOrDefault(), StartDate, EndDate);
            return PartialView("_Program", model);
        }

        public PartialViewResult ParticipantProgress(int id, DateTime? StartDate, DateTime? EndDate)
        {
            var model = Service.GetParticipantProgressViewModel(id, StartDate, EndDate);
            return PartialView("_ParticipantProgress", model);
        }

        public PartialViewResult ParticipantProgressDays(int id)
        {
            var model = Service.GetParticipantProgressDaysViewModel(id);
            return PartialView("_ParticipantProgressDays", model);
        }

        public PartialViewResult ManageParticipant(int id)
        {
            var model = Service.GetParticipantViewModel(id);
            return PartialView("_ManageParticipant", model);
        }

        [HttpPost]
        public ActionResult ManageParticipant(ParticipantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = GetModelStateErrors() }
                };
            }

            //add to database
            Service.SaveParticipantViewModel(model);

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { error = false }
            };
        }

        public ActionResult EvaluateProgressDayForm(int id)
        {
            var model = Service.GetProgressDayViewModel(id);
            return View("EvaluateProgressDayForm", model);
        }

        [HttpPost]
        public ActionResult EvaluateProgressDayForm(ProgressDayViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = GetModelStateErrors() }
                };
            }

            Service.SaveProgressDayViewModel(model);

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { error = false }
            };
        }

        public ActionResult EvaluateProgress(int id)
        {
            var model = Service.GetProgressViewModel(id);
            return View("EvaluateProgress", model);
        }

        [HttpPost]
        public ActionResult EvaluateProgress(ProgressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = GetModelStateErrors() }
                };
            }

            ProgressViewModel progressViewModel = null;

            try
            {
                progressViewModel = Service.SaveProgressViewModel(model);
            }
            catch
            {
                logger.WriteDebug(string.Format("Headers:{0}", HttpContext.Request.Headers));
                logger.WriteDebug(string.Format("Request:{0}", HttpRequestHelper.RequestBody()));
                throw;
            }          

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { error = false, Model = progressViewModel.Serialize }
            };
        }

        private string GetModelStateErrors()
        {
            return ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors).Aggregate("", (current, error) => current + ((current != "" ? ", " : "") + error.ErrorMessage));
        }
    }

    public static class HttpRequestHelper
    {
        public static string RequestBody()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            return bodyText;
        }
    }

}