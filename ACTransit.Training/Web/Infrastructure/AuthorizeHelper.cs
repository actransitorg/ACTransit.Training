using System;
using System.Web.Caching;
using System.Linq;
using System.Web;
using ACTransit.Training.Web.Domain.Services;

namespace ACTransit.Training.Web.Infrastructure
{
    public static class AuthorizeHelper
    {
        private static readonly AclService Service;
        private static DateTime _absolluteExpirationDateTime;

        static AuthorizeHelper()
        {
            Service = AclService.Create();
        }

        public static bool HasAccess(string token)
        {            
            string user = HttpContext.Current.User.Identity.Name;
                        
            if (!string.IsNullOrWhiteSpace(user))
            {
                if (user.Contains('\\'))
                    user = user.Split('\\')[1];
                if (user.Contains("@"))
                    user = user.Split('@')[0];

                string key = CreateCacheKey(user, token);
                var cacheResult = GetCacheAsBool(key);
                if (cacheResult.HasValue)
                    return cacheResult.Value;                
                var result= Service.HasAccess(token,user);
                AddCache(key,result);
                return result;
            }
            return false;
        }

        public static bool HasDynamicAccess<T>(T obj)
        {
            string user = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(user))
            {
                if (user.Contains('\\'))
                    user = user.Split('\\')[1];
                if (user.Contains("@"))
                    user = user.Split('@')[0];

                return Service.HasDynamicAccess(obj, user);
            }
            return false;
        }
        public static string[] GetUsersHasAccessTo(string token)
        {
            var key = CreateCacheKey("", token);
            var result = Cache[key] as string[];
            if (result == null)
            {
                result = Service.GetAuthorizedUsersTo(token, false);
                if (result != null)
                    result = result.OrderBy(m => m).ToArray();
                AddCache(key,result);
            }
            return result;
        }


        private static string CreateCacheKey(string user, string token)
        {
            return string.Format("ACTransit.Training.Web_user_{0}_token_{1}", user,token);
        }


        private static bool? GetCacheAsBool(string key)
        {
            var result = Cache[key];
            if (result != null)
            {
                if (result is bool )
                    return new bool?((bool)result);
                bool temp;
                if (bool.TryParse(result.ToString(), out temp))
                    return new bool?(temp);
            }            
            return new bool?();
        }

        private static void AddCache(string key, object value)
        {
            Cache.Add(key, value, null, GetAbsolluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);            
        }

        private static DateTime GetAbsolluteExpiration 
        {
            get
            {
                if (DateTime.Now > _absolluteExpirationDateTime)
                    _absolluteExpirationDateTime=DateTime.Now.AddMinutes(5);
                return _absolluteExpirationDateTime;

            }
        }

        private static System.Web.Caching.Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
                
            }
        }
    }
}