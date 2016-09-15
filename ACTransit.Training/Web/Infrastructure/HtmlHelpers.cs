using System.Web.Routing;

namespace ACTransit.Training.Web.Infrastructure
{
    public static class HtmlHelpers
    {
        public static RouteValueDictionary EnabledIf(this object htmlAttributes, bool enabled)
        {
            var attributes = new RouteValueDictionary(htmlAttributes);
            if (!enabled)
                attributes["disabled"] = "disabled";
            return attributes;
        }

        public static RouteValueDictionary DisabledIf(this object htmlAttributes, bool disabled)
        {
            var attributes = new RouteValueDictionary(htmlAttributes);
            if (disabled)
                attributes["disabled"] = "disabled";
            return attributes;
        }
    }
}