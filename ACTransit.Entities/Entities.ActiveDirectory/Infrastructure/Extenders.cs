using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACTransit.Entities.ActiveDirectory.Infrastructure
{
    public static class Extenders
    {
        public static TAttribute GetAttribute<TAttribute>(this PropertyInfo value)
                where TAttribute : Attribute
        {
            return value 
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();

        }

        public static string GetActiveDirectoryName(this PropertyInfo value)
        {
            var attr = value.GetAttribute<ActiveDirectoryPropertyAttribute>();
            return attr == null ? null : (attr.PropertyName??value.Name);
        }

        public static bool IsActiveDirectory(this PropertyInfo value)
        {
            var attr = value.GetAttribute<ActiveDirectoryPropertyAttribute>();
            return attr != null;
        }

        public static bool UpdatableOnActiveDirectory(this PropertyInfo value)
        {
            var attr = value.GetAttribute<ActiveDirectoryPropertyAttribute>();
            return attr != null && attr.Updatable;
        }

        public static ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType UpdateTypeOnActiveDirectory(this PropertyInfo value)
        {
            var attr = value.GetAttribute<ActiveDirectoryPropertyAttribute>();
            return attr == null ? ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.Na : attr.UpdateType;
        }

        public static bool UseAsFilterOnActiveDirectory(this PropertyInfo value)
        {
            var attr = value.GetAttribute<ActiveDirectoryPropertyAttribute>();
            return attr != null && attr.UseAsFilter;
        }

    }
}
