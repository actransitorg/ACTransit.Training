using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Domain.Extensions;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Domain.Services
{
    public class CourseScheduleServiceDomain : BaseService
    {
        public CourseSchedulesViewModel GetModel(CourseSchedulesViewModel model)
        {
            if (model == null)
                model = new CourseSchedulesViewModel {JustShowActive = true,SkipRows = 0, RowsPerPage = Common.RowsPerPage};
            
            model = (CourseSchedulesViewModel)base.PrepareModel(model);

            var selectedCourseTypeIds = model.CourseTypes.Where(m => m.Selected).Select(m => m.Value.ToLong()).Where(m => m.HasValue).Select(m => m.Value).ToArray();
            var selectedCourseIds = model.Courses.Where(m => m.Selected).Select(m => m.Value).ToArray();

            var courses = new SelectList(CourseService.GetCoursesByCourseTypes(selectedCourseTypeIds).OrderBy(m=>m.Name).Select(m => new { m.Name, m.CourseId }).ToList(), "CourseId", "Name").ToList();
            if (selectedCourseIds.Length > 0)
                courses.Where(m => selectedCourseIds.Contains(m.Value)).ToList().ForEach(m=>m.Selected=true);
            else 
                courses.ForEach(m=>m.Selected=true);

            var filteredCourseIds = courses.Where(m => m.Selected).Select(m => m.Value.ToLong()).Where(m=>m.HasValue).Select(m=>m.Value).ToArray();


            var courseSchedules = GetCourseSchedules(model.JustShowActive).Where(m => filteredCourseIds.Contains(m.CourseId) && selectedCourseTypeIds.Contains(m.Course.CourseTypeId));
            var query = courseSchedules
                .OrderBy(m => m.Course.Name)
                .ThenByDescending(m => m.BeginEffDate)
                .ThenByDescending(m => m.EndEffDate);

            model.TotalRows = query.Count();
            if (model.SkipRows > model.TotalRows) model.SkipRows = 0;

            model.Courses = courses;
            model.CourseSchedules = query
                .Skip(model.SkipRows)
                .Take(model.RowsPerPage)
                .ToList();
            
            return model;
        }

        public CourseSchedulePageViewModel GetModel(long courseScheduleId)
        {
            const string methodName = "GetModel";
            var cs = CourseScheduleService.GetById(courseScheduleId, m=>m.CourseScheduleInstructors, m=>m.Course);
            if (cs == null)
            {
                LogError(methodName,"Course Schedule " + courseScheduleId + "not found.");
                throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
            }
            var result= new CourseSchedulePageViewModel
            {
                CourseSchedule = Converter.ToViewModel(cs),
                CourseTypeId = cs.Course.CourseTypeId
            };
            result.CourseSchedule.CourseScheduleInstructors =
                cs.CourseScheduleInstructors.ToList().Select(Converter.ToViewModel).ToList();
            return result;
        }
        
        public void SaveModel(CourseSchedulePageViewModel model)
        {
            var instructorIds = model.CourseSchedule.CourseScheduleInstructors.Select(m => m.InstructorId);
            TimeSpan startTime = TimeSpan.Parse(model.CourseSchedule.StartTimeStr);
            TimeSpan endTime = TimeSpan.Parse(model.CourseSchedule.EndTimeStr);
            model.CourseSchedule.BeginEffDate=ConvertToDate(model.CourseSchedule.BeginEffDateStr).Add(startTime);
            model.CourseSchedule.EndEffDate= ConvertToDate(model.CourseSchedule.EndEffDateStr).Add(endTime);
            model.CourseSchedule.StartTime = startTime;
            model.CourseSchedule.EndTime = endTime;

            if (model.State == ViewModelState.New)
                CourseScheduleService.Add(Converter.ToDbObjType(model.CourseSchedule), instructorIds.ToArray());
            else
                CourseScheduleService.Update(Converter.ToDbObjType(model.CourseSchedule), instructorIds.ToArray());            
        }

        public void DeleteModel(long courseScheduleId)
        {
            var courseEnrollments = CourseEnrollmentService.GetCourseEnrollments(courseScheduleId);
            if (courseEnrollments.Any())
                throw new FriendlyException("Course schedule in use, cannot delete.");
            CourseScheduleService.Delete(courseScheduleId);
        }

        public void ValidateModel(CourseSchedulePageViewModel model, ModelStateDictionary modelState)
        {
            if (model == null) return;
            if (model.CourseSchedule == null)
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course Schedule cannot be null.");
                modelState.AddModelError("CourseSchedule", "Course Schedule cannot be null.");
                return;
            }

            if (model.CourseSchedule.CourseId==0)
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course must be set.");
                modelState.AddModelError("CourseSchedule", "Course must be set.");
                return;
            }

            if (string.IsNullOrWhiteSpace(model.CourseSchedule.BeginEffDateStr))
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course Begin Date must be set.");
                modelState.AddModelError("CourseSchedule.BeginEffDate", "Course Begin Date must be set.");
            }

            if (string.IsNullOrWhiteSpace(model.CourseSchedule.EndEffDateStr))
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course End Date must be set.");
                modelState.AddModelError("CourseSchedule.EndEffDate", "Course End Date must be set.");
            }

            if (string.IsNullOrWhiteSpace(model.CourseSchedule.StartTimeStr))
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course Start Time must be set.");
                modelState.AddModelError("CourseSchedule.BeginEffDate", "Course Start Time must be set.");
            }

            if (string.IsNullOrWhiteSpace(model.CourseSchedule.EndTimeStr))
            {
                if (HttpContext.Current.Request.IsAjaxRequest())
                    throw new FriendlyException("Course End Time must be set.");
                modelState.AddModelError("CourseSchedule.EndEffDate", "Course End Time must be set.");
            }
                
        }

        private IQueryable<CourseSchedule> ExcludeInactiveCourses(IQueryable<CourseSchedule> courseSchedules)
        {
            return courseSchedules.Where(m => m.Course.IsActive);
        }

        private IQueryable<CourseSchedule> GetCourseSchedules(bool justCurrent = true)
        {
            dynamic schedules;

            if (justCurrent)
                schedules = CourseScheduleService.GetCourseSchedulesEndsAfter(DateTime.Now.AddDays(-1),
                    m => m.Course,
                    m => m.CourseScheduleInstructors,
                    m => m.CourseEnrollments);
            else
                schedules = CourseScheduleService.Get(null,
                    m => m.Course,
                    m => m.CourseScheduleInstructors,
                    m => m.CourseEnrollments);

            return schedules;
        }

        /// <summary>
        /// Get all coursetypes. This function returns all active coursetypes and the coursetype that is being used for the given courseSchedule whether active or not.
        /// </summary>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        public List<CourseTypeViewModel> GetCourseTypes(long? courseScheduleId)
        {
            long? courseTypeId = null;
            if (courseScheduleId != null && courseScheduleId != 0)
            {
                var courseSchedule = CourseScheduleService.GetById(courseScheduleId);
                if (courseSchedule != null)
                    courseTypeId = courseSchedule.Course.CourseTypeId;
            }
            return courseTypeId == null
                ? GetCourseTypes(m => m.IsActive)
                : GetCourseTypes(m => m.IsActive || m.CourseTypeId == courseTypeId.Value);
        }

        /// <summary>
        /// Get all courses based on coursetype. This function returns all active courses and the course that is being used for the given courseSchedule whether active or not.
        /// </summary>
        /// <param name="courseScheduleId"></param>
        /// <param name="courseTypeId"></param>
        /// <returns></returns>
        public List<CourseViewModel> GetCourses(long? courseScheduleId, long courseTypeId)
        {
            List<CourseViewModel> result = null;
            if (courseTypeId != 0)
            {
                bool hasAccess = AclService.HasDynamicAccess(new CourseType { CourseTypeId = courseTypeId }, CurrentUserName);
                if (hasAccess)
                {
                    result =
                        CourseService.GetAvailableCoursesForCourseSchedule(courseScheduleId)
                            .Where(m => m.CourseTypeId == courseTypeId).OrderBy(m=>m.Name)
                            .ToList()
                            .Select(Converter.ToViewModel)
                            .ToList();
                }
            }
            return result ?? new List<CourseViewModel>();
        }

        public List<InstructorViewModel> GetInstructors(long? courseScheduleId, long courseTypeId)
        {
            var instructorsWithNonEmployees = InstructorService.GetAvailableInstructorsForCourseSchedule(courseScheduleId, courseTypeId,m=>m.NonEmployee).ToList();
            return Converter.ToViewModels(instructorsWithNonEmployees);
        }

        public List<DivisionViewModel> GetDivisions(long? courseScheduleId)
        {
            return DivisionService.GetDivisionsForCourseSchedule(courseScheduleId)
                .ToList()
                .Select(Converter.ToViewModel)
                .ToList();
        }
    }
}
