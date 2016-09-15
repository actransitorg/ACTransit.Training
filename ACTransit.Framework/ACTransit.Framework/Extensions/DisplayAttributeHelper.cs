using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace ACTransit.Framework.Extensions
{
    public static class DisplayAttributeHelper
    {
        public static string Description(this Enum value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}