using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Models;

namespace ACTransit.Training.Web.Areas.Admin.Controllers
{
    public class TopicController : AdminBaseController<TopicServiceDomain>
    {
        // GET: Admin/Topic
        public override ActionResult Index()
        {
            var model = Service.GetTopicsPageViewModel(null);
            return View("Index",model);
        }

        [HttpPost]
        public  ActionResult Index(TopicsPageViewModel model)
        {
            model = Service.GetTopicsPageViewModel(model);
            return View("Index", model);
        }

        public ActionResult New()
        {
            var model = Service.GetTopicPageViewModel(null);
            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var model = Service.GetTopicPageViewModel(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Save(TopicPageViewModel model)
        {
            if (!Service.ValidateModel(model, ModelState) || !ModelState.IsValid)
                return View("Edit", model);

            model = Service.SaveTopic(model);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(long id)
        {
            Service.DeleteTopic(id);
            return Json(new JsonGeneric(true, "", null));
        }

    }
}