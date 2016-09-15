using System;
using System.Collections.Generic;
using System.Linq;

namespace ACTransit.Training.Web.Business.Infrastructure
{
    public static class Extensions
    {
        public static string FullName(this Entities.Employee.EmployeeAll employee)
        {
            if (employee == null) return "";
            var name = "";
            if (!string.IsNullOrWhiteSpace(employee.FirstName))
                name += employee.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(employee.LastName))
                name += (name != "" ? " " : "") + employee.LastName.Trim();
            if (!string.IsNullOrWhiteSpace(employee.Suffix))
                 name += (name != "" ? " " : "") + employee.Suffix.Trim();
            return name;
        }
        public static string FullName(this Entities.Employee.Employee employee)
        {
            if (employee == null) return "";
            var name = "";
            if (!string.IsNullOrWhiteSpace(employee.FirstName))
                name += employee.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(employee.LastName))
                name += (name != "" ? " " : "") + employee.LastName.Trim();
            if (!string.IsNullOrWhiteSpace(employee.Suffix))
                name += (name != "" ? " " : "") + employee.Suffix.Trim();
            return name;
        }

        public static TimeSpan ToTimeSpan(this double? number)
        {
            return number.GetValueOrDefault().ToTimeSpan();
        }

        public static TimeSpan ToTimeSpan(this double number)
        {
            var totalseconds = number*3600;

            var hours = (int)Math.Floor(number);
            var minutes = (int)Math.Round((totalseconds%3600)/60);
            var seconds = 0;
            return new TimeSpan(0, hours, minutes, seconds);
        }

        public static TimeSpan ToTimeSpan(this string str)
        {            
            if (!string.IsNullOrWhiteSpace(str))
            {
                var parts = str.Split(':');
                int hours, minutes;
                if (!int.TryParse(parts[0], out hours))
                    hours = 0;
                if (parts.Length <= 1 || !int.TryParse(parts[1], out minutes))
                    minutes = 0;
                return new TimeSpan(hours, minutes, 0);                
            }
            return new TimeSpan(0);        
        }

        public static string ToTimeFormat(this TimeSpan time)
        {
            return ((int)time.TotalHours).ToString().PadLeft(2,'0') + ":" + time.Minutes.ToString().PadLeft(2,'0');
        }

        public static double ToDouble(this TimeSpan time)
        {
            return time.TotalSeconds / 3600;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
