using ACTransit.Training.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly Logger _logger;

        public ErrorController()
        {
            _logger = new Logger(this.GetType());
        }

        //
        // GET: /KPI/Error/
        public ActionResult Index()
        {
            _logger.Debug("Called.");
            return View();
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            _logger.Debug("Called.");
            ViewBag.Message = aspxerrorpath;
            return View("NotFound");
        }
    }
}