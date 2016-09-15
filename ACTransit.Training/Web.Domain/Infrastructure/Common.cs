using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Framework.Caching;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Training;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Infrastructure
{
    internal static class Common
    {
        public static string CurrentUserName
        {
            get
            {
                try
                {
                    return HttpContext.Current.User.Identity.Name;
                }
                catch (Exception e)
                {
                    return System.Environment.UserName;
                }
                
            }
        }

        public static string AclPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/ACL.xml");
            }
        }

        public static string GetName(CourseEnrollment courseEnrollment, IEnumerable<EmployeeAll> employees, IEnumerable<NonEmployee> nonEmployees)
        {
            return GetName(courseEnrollment.Badge, courseEnrollment.NonEmployeeId, employees, nonEmployees);
        }


        public static string GetName(string badge,long? nonEmployeeId, IEnumerable<EmployeeAll> employees, IEnumerable<NonEmployee> nonEmployees)
        {
            if (nonEmployeeId != null && nonEmployeeId != 0)
            {
                if (nonEmployees != null)
                {
                    var ne = nonEmployees.FirstOrDefault(m => m.NonEmployeeId == nonEmployeeId);
                    if (ne != null)
                        return ne.Name;
                }                
            }
            else if (employees != null)
            {
                var emp = employees.FirstOrDefault(m => m.Badge == badge);
                return emp.FullName();
            }
            return "";
        }

        public static string GetDept(CourseEnrollment courseEnrollment, IEnumerable<EmployeeAll> employees)
        {
            if (!string.IsNullOrWhiteSpace(courseEnrollment.Badge))
            {
                var emp = employees.FirstOrDefault(m => m.Badge == courseEnrollment.Badge);
                if (emp != null)
                    return emp.DeptName;
            }
            return "";
        }

        public static string GetCourseNameByEnrollmentId(long enrollmentId)
        {
            using (var service = new EnrollmentService(CurrentUserName))
            {
                var course= service.GetCourse(enrollmentId);
                return course == null ? string.Empty:course.Name;
            }
        }

        private static Dictionary<long, CourseTypeViewModel> _courseTypeNameId;
        public static Dictionary<long, CourseTypeViewModel> CourseTypeNameId
        {
            get
            {
                if (_courseTypeNameId == null)
                {
                    using (var service = new CourseTypeService(Common.CurrentUserName))
                    {
                        _courseTypeNameId = service.GetCourseTypes().ToList().Select(Converter.ToViewModel).ToDictionary(m => m.CourseTypeId, m => m);
                    }
                }
                return _courseTypeNameId;
            }
        }


        public static string Trim(string str)
        {
            if (str != null)
                return str.Trim();
            return null;
        }
        public static string Trim(string str, string defaultValue)
        {
            if (str != null)
                return str.Trim();
            return defaultValue;
        }

        public static int RowsPerPage
        {
            get
            {
                var temp=ConfigurationManager.AppSettings["RowsPerPage"];
                int result;
                if (!int.TryParse(temp, out result))
                    result = 50;
                return result;
            }
        }

        private static ICache _cache;
        public static ICache Cache
        {
            get
            {
                if (_cache == null)
                    _cache = new Cache("ACTransit.Training.Web.Domain");
                return _cache;
            }
        }    

    }
}
