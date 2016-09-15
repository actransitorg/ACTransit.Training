using System.Collections.Generic;
using System.Linq;
using ACTransit.Training.Web.Business.Employee;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Maintenance;
using ACTransit.Training.Web.Business.Models;
using ACTransit.Training.Web.Business.Training;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Extensions
{
    public static class DbEntryExtensions
    {
        private const int CourseEnrollmentCacheTimeout = 5;
        public static string GetBadge(this Enrollment enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId == 0 || enrollment.CourseEnrollmentId == 0)
                return string.Empty;
            var cacheKey = "CourseEnrollmentId_" + enrollment.CourseEnrollmentId;
            var ce = (CourseEnrollment)Common.Cache.GetCache(cacheKey);
            if (ce == null)
            {
                using (var service = new CourseEnrollmentService(Common.CurrentUserName))
                {
                    ce = service.GetById(enrollment.CourseEnrollmentId);
                }
                Common.Cache.AddShortCache(cacheKey, ce, CourseEnrollmentCacheTimeout);
            }
            if (ce != null)
                return ce.Badge;
            return "";
        }

        public static long GetNonEmployeeId(this Enrollment enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId == 0 || enrollment.CourseEnrollmentId == 0)
                return 0;
            var cacheKey = "CourseEnrollmentId_" + enrollment.CourseEnrollmentId;
            var ce=(CourseEnrollment)Common.Cache.GetCache(cacheKey);
            if (ce == null)
            {
                using (var service = new CourseEnrollmentService(Common.CurrentUserName))
                {
                    ce = service.GetById(enrollment.CourseEnrollmentId);
                }
                Common.Cache.AddShortCache(cacheKey, ce, CourseEnrollmentCacheTimeout);
            }
            if (ce != null)
                return ce.NonEmployeeId.GetValueOrDefault(0);

            return 0;
        }

        public static long GetCourseScheduleId(this Enrollment enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId == 0 || enrollment.CourseEnrollmentId == 0)
                return 0;
            var cacheKey = "CourseEnrollmentId_" + enrollment.CourseEnrollmentId;
            var ce = (CourseEnrollment)Common.Cache.GetCache(cacheKey);
            if (ce == null)
            {
                using (var service = new CourseEnrollmentService(Common.CurrentUserName))
                {
                    ce = service.GetById(enrollment.CourseEnrollmentId);
                }
                Common.Cache.AddShortCache(cacheKey, ce, CourseEnrollmentCacheTimeout);
            }
            if (ce != null)
                return ce.CourseScheduleId;
            return 0;
        }



        public static string GetEmployee(this EnrollmentPageViewModel enrollmentPageViewModel)
        {
            if (enrollmentPageViewModel == null)
                return string.Empty;

            var nonEmployeeIds = new[] { enrollmentPageViewModel.NonEmployeeId };
            IEnumerable<EmployeeAll> employees;
            IEnumerable<NonEmployee> nonEmployees;

            using (var employeeAllService = new EmployeeAllService())
            {
                employees = employeeAllService.GetEmployeesByBadges(new[] { enrollmentPageViewModel.EmployeeBadge }).ToList();
            }
            using (var nonEmployeeService = new NonEmployeeService(Common.CurrentUserName))
            {
                nonEmployees = nonEmployeeService.Get(m => nonEmployeeIds.Contains(m.NonEmployeeId)).ToList();
            }
            return Common.GetName(enrollmentPageViewModel.EmployeeBadge, enrollmentPageViewModel.NonEmployeeId, employees, nonEmployees);
        }
        public static string GetEmployee(this Enrollment enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId == 0 || enrollment.CourseEnrollmentId == 0)
                return string.Empty;
                        var cacheKey = "CourseEnrollmentId_" + enrollment.CourseEnrollmentId;
            var ce = (CourseEnrollment)Common.Cache.GetCache(cacheKey);
            if (ce == null)
            {
                using (var service = new CourseEnrollmentService(Common.CurrentUserName))
                {
                    ce = service.GetById(enrollment.CourseEnrollmentId);
                    Common.Cache.AddShortCache(cacheKey, ce, CourseEnrollmentCacheTimeout);
                }
            }
            if (ce != null)
                return ce.GetEmployee();
            
            return string.Empty;
        }

        public static string GetEmployee(this EnrollmentBusinessViewModel enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId == 0 || enrollment.CourseEnrollmentId == 0)
                return string.Empty;
            var cacheKey = "CourseEnrollmentId_" + enrollment.CourseEnrollmentId;
            var ce = (CourseEnrollment)Common.Cache.GetCache(cacheKey);
            if (ce == null)
            {
                using (var service = new CourseEnrollmentService(Common.CurrentUserName))
                {
                    ce = service.GetById(enrollment.CourseEnrollmentId);
                    Common.Cache.AddShortCache(cacheKey, ce, CourseEnrollmentCacheTimeout);
                }
            }
            if (ce != null)
                return ce.GetEmployee();

            return string.Empty;
        }
      
        public static string GetEmployee(this CourseEnrollment courseEnrollment)
        {
            if (courseEnrollment == null || courseEnrollment.CourseEnrollmentId == 0)
                return string.Empty;

            var ce = courseEnrollment;

            var nonEmployeeIds = new[] { ce.NonEmployeeId };
            IEnumerable<EmployeeAll> employees;
            IEnumerable<NonEmployee> nonEmployees;

            using (var employeeAllService = new EmployeeAllService())
            {
                employees = employeeAllService.GetEmployeesByBadges(new[] { ce.Badge }).ToList();
            }
            using (var nonEmployeeService = new NonEmployeeService(Common.CurrentUserName))
            {
                nonEmployees = nonEmployeeService.Get(m => nonEmployeeIds.Contains(m.NonEmployeeId)).ToList();
            }
            return Common.GetName(ce, employees, nonEmployees);
        }     

        public static string GetVehicleDescription(this EnrollmentVehicle enrollmentVehicle)
        {
            if (enrollmentVehicle == null || string.IsNullOrWhiteSpace(enrollmentVehicle.VehicleId))
                return string.Empty;

            using (var service = new VehicleRegisterService(Common.CurrentUserName))
            {
                var ce = service.GetById(enrollmentVehicle.VehicleId);
                if (ce != null)
                {
                    return ce.EquipmentName;
                }
            }
            return string.Empty;
        }

        public static string GetCourseTypeName(this Instructor instructor)
        {
            if (instructor == null || !instructor.CourseTypeId.HasValue)
                return string.Empty;
            CourseTypeViewModel ct;
            if (!Common.CourseTypeNameId.TryGetValue(instructor.CourseTypeId.Value, out ct))
                return string.Empty;
            return ct.Name;
        }

        public static string GetCourseTypeName(this Course course)
        {
            if (course == null || course.CourseTypeId==0)
                return string.Empty;
            CourseTypeViewModel ct;
            if (!Common.CourseTypeNameId.TryGetValue(course.CourseTypeId, out ct))
                return string.Empty;
            return ct.Name;
        }

        public static string GetCourseName(this Enrollment enrollment)
        {
            if (enrollment == null || enrollment.EnrollmentId== 0)
                return string.Empty;
            return Common.GetCourseNameByEnrollmentId(enrollment.EnrollmentId);
        }
       
    }
}
