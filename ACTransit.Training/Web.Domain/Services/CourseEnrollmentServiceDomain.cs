using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Services
{
    public class CourseEnrollmentServiceDomain:BaseService
    {
        public CourseEnrollmentPageViewModelAjax GetCourseEnrollmentViewModel(long? courseEnrollmentId, long? courseScheduleId)
        {
            string employeeName = "", employeeDept = "";
            CourseSchedule courseSchedule = null;
            CourseEnrollment courseEnrollment = null;
            var modelState = courseEnrollmentId == null ? ViewModelState.New : ViewModelState.Edit;

            if (courseEnrollmentId.HasValue)
            {
                courseEnrollment = CourseEnrollmentService.GetById(courseEnrollmentId.Value);
                if (courseEnrollment == null)
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);

                courseSchedule = courseEnrollment.CourseSchedule;

                if (!string.IsNullOrWhiteSpace(courseEnrollment.Badge))
                {
                    var employee = EmployeeAllService.GetEmployees(courseEnrollment.Badge).Select(m => new { m.FirstName, m.LastName , m.DeptName}).FirstOrDefault();
                    if (employee == null)
                        throw new Exception(string.Format("Employee {0} not found.", courseEnrollment.Badge));
                    employeeName = string.Format("{0} {1}", Trim(employee.FirstName), Trim(employee.LastName));
                    employeeDept = string.Format("{0}", employee.DeptName);
                }
            }
            else if (courseScheduleId.HasValue)
            {
                courseSchedule = CourseScheduleService.GetById(courseScheduleId.Value);
                if (courseSchedule==null)
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
            }

            var cea = Converter.ToViewModel(courseEnrollment);
            if (cea==null) cea=new CourseEnrollmentViewModel();
            cea.Name = employeeName;
            cea.Dept = employeeDept;
            cea.Division = courseSchedule == null
                ? ""
                : (courseSchedule.DivisionId != null ? courseSchedule.Division.Name : null);

            return PrepareModel(new CourseEnrollmentPageViewModelAjax
            {
                State = modelState,
                CourseEnrollment = cea
            },courseEnrollment, courseSchedule);

        }

        public CourseEnrollmentsPageViewModelAjax GetCourseEnrollmentsPageViewModel(CourseEnrollmentsPageViewModelAjax model)
        {
            try
            {
                model = PrepareModel(model);
                var selectedCourseTypeIds = model.CourseTypes.Where(ct => ct.Selected).Select(m => m.Value.ToLong()).Where(m => m.HasValue).Select(m => m.Value).ToArray();


                DateTime? dateFrom, dateTo;
                if (model.JustShowCurrent)
                {
                    dateFrom = DateTime.Now.Date;
                    dateTo = null;
                }
                else
                {
                    dateFrom = model.DateFrom;
                    dateTo = model.DateTo;
                }

                if (dateTo != null && Math.Abs(dateTo.Value.TimeOfDay.TotalSeconds) <= 0)
                    dateTo = dateTo.Value.AddDays(1).AddSeconds(-1);

                var tempCourses = CourseService.GetCoursesHasCourseSchedule(selectedCourseTypeIds, dateFrom,
                    dateTo,model.Badge, m => m.CourseSchedules).OrderBy(m=>m.Name).Select(m => new CourseViewModelAjax
                    {
                        CourseId = m.CourseId,
                        CourseTypeId = m.CourseTypeId,
                        Name = m.Name,
                    }).ToList();

                model.Courses = tempCourses.ToList();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in GetCourseEnrollmentsViewModel.", ex);
            }
        }

        public CourseScheduleViewModel GetCourseScheduleViewModel(long? courseScheduleId)
        {
            try
            {
                var cs = CourseScheduleService.GetById(courseScheduleId.GetValueOrDefault());
                return Converter.ToViewModel(cs);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in GetCourseScheduleViewModel.", ex);
            }
        }

        public List<CourseScheduleViewModelAjax> GetCourseSchedulesViewModel(CourseEnrollmentsPageViewModelAjax model)
        {
            try
            {
                DateTime? dateFrom, dateTo;
                if (model.JustShowCurrent)
                {
                    dateFrom = DateTime.Now.Date;
                    dateTo = null;
                }
                else
                {
                    dateFrom = model.DateFrom;
                    dateTo = model.DateTo;
                }
                if (dateTo != null && Math.Abs(dateTo.Value.TimeOfDay.TotalSeconds) <= 0)
                    dateTo = dateTo.Value.AddDays(1).AddSeconds(-1);

                var query = CourseScheduleService.GetCourseSchedulesBetween(dateFrom, dateTo, null, m => m.CourseEnrollments).Where(m => m.CourseId == model.CourseId);
                if (!string.IsNullOrWhiteSpace(model.Badge))
                    query = query.Where(m => m.CourseEnrollments.Any(m1 => m1.Badge == model.Badge));

                var list = query.OrderByDescending(m=>m.EndEffDate).ToList();

                return list.Select(Converter.ToViewModelAjax).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in GetCourseScheduleViewModel.", ex);
            }
        }

        public List<CourseEnrollmentViewModel> GetCourseEnrollmentsViewModel(CourseEnrollmentsPageViewModelAjax model)
        {
            try
            {
                var list=CourseEnrollmentService.GetCourseEnrollmentLists(model.CourseScheduleId, model.Badge);
                var enrollmentLists = list.Select(Converter.ToViewModel).ToList();
                return enrollmentLists;

            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in GetEnrolees.", ex);
            }
        }

        public CourseEnrollmentPageViewModelAjax SaveModel(CourseEnrollmentPageViewModelAjax model)
        {
            try
            {
                ValidateModel(model);

                var courseEnrollment = Converter.ToDbObjType(model.CourseEnrollment);
                long courseEnrollmentId;
                if (model.State == ViewModelState.New)
                {
                    var cs = CourseScheduleService.GetById(courseEnrollment.CourseScheduleId);
                    var csV = Converter.ToViewModelAjax(cs);
                    if (!csV.IsCurrent)
                        throw new FriendlyException("You can't enroll people on old course schedules.");
                    courseEnrollmentId = CourseEnrollmentService.Add(courseEnrollment);                    
                }                    
                else
                    courseEnrollmentId = CourseEnrollmentService.Update(courseEnrollment);

                return GetCourseEnrollmentViewModel(courseEnrollmentId,null);
            }
            catch (BusinessException ex)
            {
                throw new FriendlyException(ex.Message);
            }
        }

        public void DeleteCourseEnrollment(long courseEnrollmentId)
        {
            var ce = CourseEnrollmentService.GetById(courseEnrollmentId);
            if (ce == null)
                throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
            if (ce.Enrollments.Any())
                throw new FriendlyException(FriendlyExceptionType.InUseCanNotDelete);
            var cs = CourseScheduleService.GetById(ce.CourseScheduleId);
            if (cs != null)
            {
                var csV = Converter.ToViewModelAjax(cs);
                if (!csV.IsCurrent)
                    throw new FriendlyException("This item is old and can't be deleted.");
            }
            
            CourseEnrollmentService.Delete(courseEnrollmentId);
        }

        public void ValidateModel(CourseEnrollmentPageViewModelAjax model)
        {
            if (model == null)
                throw new Exception("Invalid Model. Model is empty.");

            if (model.CourseEnrollment == null)
                throw new FriendlyException("CourseEnrollment cannot be null.");

            if (model.CourseEnrollment.CourseScheduleId == 0)
                throw new FriendlyException("CourseSchedule cannot be empty");

            if (string.IsNullOrWhiteSpace(model.CourseEnrollment.Badge) && model.CourseEnrollment.NonEmployeeId == null)
                throw new FriendlyException("Must select Badge or Non-Employee.");
            if (model.CourseEnrollment != null && model.CourseEnrollment.CourseScheduleId != 0)
            {
                if (AlreadyEnrolled(model.CourseEnrollment.CourseEnrollmentId, model.CourseEnrollment.CourseScheduleId, model.CourseEnrollment.Badge, model.CourseEnrollment.NonEmployeeId))
                {
                    throw new FriendlyException("Selected trainee already enrolled.");
                }
            }
            if (model.CourseEnrollment.CourseEnrollmentId != 0)
            {
                var courseEnrollment = CourseEnrollmentService.GetById(model.CourseEnrollment.CourseEnrollmentId);
                if (courseEnrollment == null)
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);

            }
            var badge = model.CourseEnrollment.Badge;
            if (!string.IsNullOrEmpty(badge) && model.CourseEnrollment.NonEmployeeId == null)
            {
                var exist = EmployeeAllService.GetEmployeesByBadges(new[] { badge }).Any();
                if (!exist)
                    throw new FriendlyException("The entered badge is not valid.");                
            }
        }

        public bool AlreadyEnrolled(long courseEnrollmentId, long courseScheduleId, string badge, long? nonEmployeeId)
        {
            var courseEnrollments = CourseEnrollmentService.GetCourseEnrollments(courseScheduleId);
            if (courseEnrollments != null &&
                courseEnrollments.Any(m =>
                    m.CourseEnrollmentId != courseEnrollmentId &&
                    (
                        (badge!=null && m.Badge == badge) ||
                        (nonEmployeeId.HasValue && m.NonEmployeeId == nonEmployeeId.Value)
                    )
                    ))
                return true;
            return false;

        }

        private CourseEnrollmentPageViewModelAjax PrepareModel(CourseEnrollmentPageViewModelAjax model, CourseEnrollment courseEnrollment = null, CourseSchedule courseSchedule = null)
        {
            var courseEnrollmentId = courseEnrollment == null
                ? 0
                : courseEnrollment.CourseEnrollmentId;
            model = (CourseEnrollmentPageViewModelAjax)base.PrepareModel(model);

            if (!model.CourseTypes.Any())
                throw new Exception("No Coursetype found.");

            var tempNonEmployees =
                NonEmployeeService.GetAvailableNonEmployeesForCourseEnrollment(courseEnrollmentId).ToList();
            var tempCourseSchedules = new List<CourseSchedule>();
            if (courseSchedule!=null)
                tempCourseSchedules.Add(courseSchedule);

            var nonEmployees = tempNonEmployees.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.NonEmployeeId.ToString(),
                Selected = courseEnrollment != null && m.NonEmployeeId == courseEnrollment.NonEmployeeId
            }).ToList();



            var courseSchedules = tempCourseSchedules.Select(m => new SelectListItem
            {
                Text = string.Format("{0} - {1} - {2} to {3}", m.Course.CourseType.Name, m.Course.Name, m.BeginEffDate.ToString(ShortDateTimeFormat), m.EndEffDate.ToString(ShortDateTimeFormat)),
                Value = m.CourseScheduleId.ToString(),
                Selected = courseSchedule != null && m.CourseScheduleId == courseSchedule.CourseScheduleId
            }).ToList();


            model.CourseSchedules = courseSchedules;
            model.NonEmployees = nonEmployees;
            model.HasEnrollment = EnrollmentService.GetEnrollmentsByCourseEnrollment(courseEnrollmentId).Any();
            return model;
        }

        private CourseEnrollmentsPageViewModelAjax PrepareModel(CourseEnrollmentsPageViewModelAjax model)
        {
            if (model == null)
                model = new CourseEnrollmentsPageViewModelAjax();
            model = (CourseEnrollmentsPageViewModelAjax)base.PrepareModel(model);

            model.Courses.Clear();
            return model;
        }


    }
}
