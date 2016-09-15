using System;
using System.Web;

namespace ACTransit.Training.Web.Models
{
    public class Logger
    {
        private readonly Framework.Logging.Logger _logger;
        private object _lock = new object();

        public Logger(Type type)
        {            
            _logger = new Framework.Logging.Logger(type);
        }

        public Logger(string name)
        {
            _logger = new Framework.Logging.Logger(name);
        }

        public void Debug(string message)
        {
            try
            {
                _logger.WriteDebug(FormatMessage(message));
            }
            catch { }
        }
        
        public void Info(string message)
        {
            try
            {
                _logger.Write(FormatMessage(message));
            }
            catch(Exception) { }
        }

        public void Error(string message)
        {
            try
            {
                _logger.WriteError(FormatMessage(message));
            }
            catch (Exception) { }
        }

        public void Fatal(string message)
        {
            try
            {
                _logger.WriteFatal(FormatMessage(message));
            }
            catch (Exception) { }
        }


        public void Fatal(string message, Exception ex)
        {
            try
            {
                _logger.WriteFatal(FormatMessage(message), ex);
            }
            catch { }
        }

        private string FormatMessage(string message)
        {
            if (HttpContext.Current != null &&
                HttpContext.Current.User != null
                )
            {
                var user = HttpContext.Current.User;
                var request = HttpContext.Current.Request;

                string userName = string.Empty;
                if (user != null && user.Identity != null)
                    userName = user.Identity.Name;
                string iP = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrWhiteSpace(iP))
                    iP = request.UserHostAddress;
                if (string.IsNullOrWhiteSpace(iP))
                    iP = request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrWhiteSpace(iP))
                    iP = "-";
                return string.Format("{0}-{1}-{2}", userName, iP, message);
            }
            return message;
        }
    }
}