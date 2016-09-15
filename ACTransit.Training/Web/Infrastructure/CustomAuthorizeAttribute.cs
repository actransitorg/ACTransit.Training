using ACTransit.Training.Web.Domain.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {        
        public string Token { get; set; }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SkipAuthorization(filterContext))
                return;

            bool hasAccess = false;
            if (!string.IsNullOrEmpty(Token))
            {
                string[] tokens;
                if (Token.Contains(';'))
                    tokens = Token.Split(';');
                else
                    tokens = Token.Split(',');
                foreach (var token in tokens)
                {
                    hasAccess = AuthorizeHelper.HasAccess(token);
                    if (hasAccess)
                        break;
                }
            }
            if (!hasAccess)
                throw new FriendlyException(FriendlyExceptionType.AccessDenied);


        }

        private bool SkipAuthorization(AuthorizationContext filterContext, bool inherit = true)
        {
            return (filterContext.ActionDescriptor.IsDefined(typeof (AllowAnonymousAttribute), inherit) ||
                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof (AllowAnonymousAttribute), true));
        }

    }
}