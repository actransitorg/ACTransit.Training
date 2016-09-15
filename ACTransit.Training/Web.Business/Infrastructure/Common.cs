using ACTransit.Framework.Caching;

namespace ACTransit.Training.Web.Business.Infrastructure
{
    internal static class Common
    {
        private static ICache _cache;
        public static ICache Cache
        {
            get
            {
                if (_cache == null)
                    _cache = new Cache("ACTransit.Training.Web.Business");
                return _cache;
            }
        }
    }
}
