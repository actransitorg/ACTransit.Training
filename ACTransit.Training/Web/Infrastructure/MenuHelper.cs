using System;
using System.Web;
using System.Web.Caching;
using ACTransit.Entities.Training;
using System.Collections.Generic;
using ACTransit.Training.Web.Domain.Services;
using System.Linq;

namespace ACTransit.Training.Web.Infrastructure
{
    public static class MenuHelper
    {
        private static DateTime _absolluteExpirationDateTime;
        private static object CacheLock = new object();

        public static List<Menu> GetResources()
        {
            const string key = "Resources_1";
            var res = Cache[key] as List<Menu>;
            if (res == null)
            {
                lock (CacheLock)
                {
                    res = Cache[key] as List<Menu>;
                    if (res == null)
                    {
                        using (var service = new MenuServiceDomain())
                        {
                            Console.WriteLine("add cache! " + key);
                            res = service.GetResources();
                            AddCache(key, res);
                        }
                    }
                }
            }
            return res;
        }

        private static void AddCache(string key, object value)
        {
            Cache.Add(key, value, null, GetAbsolluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }


        private static string GetUser()
        {
            string user = HttpContext.Current.User.Identity.Name;

            if (!string.IsNullOrWhiteSpace(user))
            {
                if (user.Contains('\\'))
                    user = user.Split('\\')[1];
                if (user.Contains("@"))
                    user = user.Split('@')[0];
            }
            return user;
        }
        private static DateTime GetAbsolluteExpiration
        {
            get
            {
                if (DateTime.Now > _absolluteExpirationDateTime)
                    _absolluteExpirationDateTime = DateTime.Now.AddMinutes(5);
                return _absolluteExpirationDateTime;

            }
        }
        private static Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;

            }
        }
    }
}