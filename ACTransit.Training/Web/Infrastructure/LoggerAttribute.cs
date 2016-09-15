using System;
using System.Diagnostics;
using System.Web.Mvc;
using ACTransit.Framework.Logging;

namespace ACTransit.Training.Web.Infrastructure
{
    public class LoggerAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly Logger _logger;

        public LoggerAttribute()
        {
            _logger = new Logger(GetType().Name);
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            log4net.NDC.Clear();
            try
            {
                var actionName = filterContext.ActionDescriptor.ActionName;
                var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var user = filterContext.RequestContext.HttpContext.User.Identity.Name;

                log4net.NDC.Push(controllerName + "." + actionName + " (" + filterContext.HttpContext.Request.HttpMethod +
                                 "):" + user);
                _logger.WriteDebug("Called.");
            }
            catch (Exception ex)
            {
                _logger.WriteError("On called, ex:" + ex.Message);
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.WriteDebug("Finished.");
            base.OnActionExecuted(filterContext);
        }
    }
}