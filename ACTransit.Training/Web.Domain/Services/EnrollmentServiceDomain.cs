using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Models;
using ACTransit.Training.Web.Domain.Extensions;
using ACTransit.Training.Web.Domain.Infrastructure;
using ACTransit.Training.Web.Domain.Models;

namespace ACTransit.Training.Web.Domain.Services
{
    public class EnrollmentServiceDomain:BaseService
    {        
        public EnrollmentPageViewModel GetModel(EnrollmentPageViewModel model)
        {
            const string methodName = "GetModel";
            LogDebug(methodName, "started.");
            try
            {
                if (model == null)
                    model = new EnrollmentPageViewModel();
                if (model.Enrollments == null)
                    model.Enrollments = new List<EnrollmentViewModel>();
                var ce = EnrollmentService.GetCourseEnrollment(model.CourseScheduleId, model.NonEmployeeId, model.EmployeeBadge);
                if (model.CourseEnrollmentId != 0 && ce != null && model.CourseEnrollmentId != ce.CourseEnrollmentId)
                {
                    LogError(methodName,
                        string.Format(
                            "model.CourseEnrollmentId is different that calculated CourseEnrollmentId. model.CourseEnrollmentId:{0}, model.CourseScheduleId:{1}, model.NonEmployeeId:{2}, model.EmployeeBadge:{3}, calculated CourseEnrollmentId:{4}",
                            model.CourseEnrollmentId, model.CourseScheduleId, model.NonEmployeeId, model.EmployeeBadge,
                            ce.CourseEnrollmentId));
                    throw new FriendlyException(FriendlyExceptionType.InvalidModelState);
                }
                if (model.CourseEnrollmentId == 0 && ce != null)
                    model.CourseEnrollmentId = ce.CourseEnrollmentId;                

                if (model.CourseEnrollmentId == 0 && ce==null)
                {
                    model.State = ViewModelState.New;
                    model.PersonName = model.GetEmployee();
                }                    
                else
                {
                    var courseEnrollment = CourseEnrollmentService.Get(m => m.CourseEnrollmentId == model.CourseEnrollmentId,m=>m.CourseSchedule, m=>m.CourseSchedule.Course).FirstOrDefault();
                    if (courseEnrollment==null)
                        throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);

                    model.CourseTypeId = courseEnrollment.CourseSchedule.Course.CourseTypeId;
                    model.CourseId = courseEnrollment.CourseSchedule.CourseId;
                    model.CourseScheduleId = courseEnrollment.CourseScheduleId;
                    model.EmployeeBadge = courseEnrollment.Badge;
                    model.NonEmployeeId = courseEnrollment.NonEmployeeId.GetValueOrDefault();
                    model.PersonName = courseEnrollment.GetEmployee();

                    if (model.IncludeEnrollments)
                    {
                        var enrollments = EnrollmentService.GetEnrollmentsByCourseEnrollment(model.CourseEnrollmentId).ToList();
                        if (enrollments.Count>0)
                            model.State=ViewModelState.Edit;
                                                
                        foreach (var enrollee in enrollments)
                        {
                            var enrollment = Converter.ToViewModel(enrollee);
                            enrollment.EnrollmentTopics = enrollee.EnrollmentTopics.Select(Converter.ToViewModel).ToList();
                            enrollment.EnrollmentVehicles = enrollee.EnrollmentVehicles.Select(Converter.ToViewModel).ToList();
                            enrollment.EnrollmentInstructors = enrollee.EnrollmentInstructors.Select(Converter.ToViewModel).ToList();

                            model.Enrollments.Add(enrollment);
                        }
                    }                    
                }
                

                bool canAddInActive = model.State == ViewModelState.Edit;

                if (model.IncludeCourseTypes)
                {
                    var courseTypes = GetCourseTypes();
                    //jus tin case of edit, we should show inactive if required.
                    if (canAddInActive && model.CourseTypeId != 0 &&
                        courseTypes.All(m => m.CourseTypeId != model.CourseTypeId))
                    {
                        var ct = CourseTypeService.GetById(model.CourseTypeId);
                        courseTypes.Add(Converter.ToViewModel(ct));
                    }
                    model.CourseTypes = courseTypes;
                }
                if (model.IncludeCourses && model.CourseTypeId != 0)
                {
                    var courses = GetCourseList(0, model.CourseTypeId).ToList();
                    if (canAddInActive && model.CourseId != 0 && courses.All(m => m.CourseId != model.CourseId))
                    {
                        var c = CourseService.GetById(model.CourseId);
                        courses.Add(Converter.ToViewModel(c));
                    }
                    model.Courses = courses;
                }
                if (model.IncludeCourseSchedules && model.CourseId != 0)
                {
                    var css = GetCourseScheduleList(model.EmployeeBadge, model.CourseEnrollmentId, model.CourseId).ToList();
                    css = css.OrderByDescending(m => m.BeginEffDate).ToList();
                    if (canAddInActive && model.CourseScheduleId != 0 && css.All(m => m.CourseScheduleId != model.CourseScheduleId))
                    {
                        var cs = CourseScheduleService.GetById(model.CourseScheduleId);
                        css.Add(Converter.ToViewModel(cs));
                    }
                    model.CourseSchedules = css;
                }

                if (model.IncludeGrades)
                    model.Grades = GradeService.Get(null).OrderBy(m => m.LetterGrade).Select(Converter.ToViewModel).ToList();

                if (model.IncludeNonEmployees)
                    model.NonEmployees = NonEmployeeService.GetAvailableNonEmployeesForCourseEnrollment(model.CourseEnrollmentId).Select(Converter.ToViewModel).ToList();

                if (model.IncludeInstructors)
                    model.Instructors = Converter.ToViewModels(InstructorService.Get(m => m.CourseTypeId == model.CourseTypeId).ToList());

                if (model.IncludeTopics)
                    model.Topics = GetTopics(model.CourseTypeId, model.CourseId);

                if (model.IncludeVehicles)
                    model.Vehicles = GetVehicles();

                if (model.IncludeRoutes)
                    model.Routes = GetRoutes();

                return model;
            }
            catch (Exception e)
            {
                LogError(methodName, e.Message);
                throw;
            }
        }

        public void SaveModel(EnrollmentPageViewModel model)
        {
            const string methodName = "SaveModel";
            try
            {
                if (model.Enrollments == null || model.Enrollments.Count == 0)
                {
                    LogError(methodName,"Enrollments is empty. nothing to save.");
                    throw new FriendlyException(FriendlyExceptionType.InvalidModelState);
                }
                var courseEnrollmentId = model.Enrollments[0].CourseEnrollmentId;                
                var courseScheduleId = model.Enrollments[0].CourseScheduleId;
                var badge = model.Enrollments[0].Badge;
                var nonEmployeeId = model.Enrollments[0].NonEmployeeId;
                
                var enrollments = new TransactionEnrollmentsSaveViewModel
                {
                    Badge = badge,
                    CourseEnrollmentId = courseEnrollmentId,
                    CourseScheduleId = courseScheduleId,
                    NonEmployeeId = nonEmployeeId
                };

                foreach (var enrollmentModel in model.Enrollments)
                {
                    if (string.IsNullOrWhiteSpace(enrollmentModel.LectureTimeStr))
                        enrollmentModel.LectureTimeStr = "00:00";
                    enrollmentModel.LectureTime = enrollmentModel.LectureTimeStr.ToTimeSpan().Ticks;
                    enrollmentModel.SessionDate = ConvertToDate(enrollmentModel.SessionDateStr).Ticks;

                    var enrollment = Converter.ToDbObjType(enrollmentModel);
                    var enrollmentVehicles = new List<EnrollmentVehicle>();
                    var enrollmentInstructors = new List<EnrollmentInstructor>();
                    var enrollmentTopics = new List<EnrollmentTopic>();
                    foreach (var ev in enrollmentModel.EnrollmentVehicles)
                    {
                        ev.EnrollmentId = enrollment.EnrollmentId;
                        var obj = Converter.ToDbObjType(ev);
                        if (string.IsNullOrWhiteSpace(ev.WheelTimeStr)) ev.WheelTimeStr = "00:00";
                        obj.WheelTime = TimeSpan.Parse(ev.WheelTimeStr).ToDouble();
                        enrollmentVehicles.Add(obj);
                    }
                    foreach (var ei in enrollmentModel.EnrollmentInstructors)
                    {
                        ei.EnrollmentId = enrollment.EnrollmentId;
                        enrollmentInstructors.Add(Converter.ToDbObjType(ei));
                    }
                    foreach (var et in enrollmentModel.EnrollmentTopics)
                    {
                        et.EnrollmentId = enrollment.EnrollmentId;
                        enrollmentTopics.Add(Converter.ToDbObjType(et));
                    }
                    enrollments.Add(new TransactionEnrollmentSaveViewModel { Enrollment = enrollment, EnrollmentInstructors = enrollmentInstructors, 
                        EnrollmentVehicles = enrollmentVehicles , EnrollmentTopics = enrollmentTopics});
                }
                EnrollmentService.Save(enrollments);

            }
            catch (BusinessException ex)
            {
                throw new FriendlyException(ex.Message);
            }
        }                

        public EnrollmentPageViewModel GetCourses(long courseEnrollmentId,long courseTypeId)
        {
            var model = new EnrollmentPageViewModel();
            if (courseTypeId == 0)
                return model;


            var courses = GetCourseList(courseEnrollmentId, courseTypeId).ToList();
            model.Courses = courses;
            model.Instructors = Converter.ToViewModels(InstructorService.Get(m => m.CourseTypeId == courseTypeId).ToList());
            return model;
        }

        public void DeleteEnrollment(long courseEnrollmentId)
        {
            EnrollmentService.DeleteByCourseEnrollment(courseEnrollmentId);
        }

        private List<RouteViewModel> GetRoutes()
        {
            return RouteService.Get(null).OrderBy(m => m.SortOrder).Select(Converter.ToViewModel).ToList();
        }

        private List<VehicleRegisterViewModel> GetVehicles()
        {
            return VehicleService.Get(m => m.IsActive== "Y").OrderBy(m=>m.VehicleId).Select(Converter.ToViewModel).ToList();
        }

        private IEnumerable<CourseViewModel> GetCourseList(long courseEnrollmentId,long courseTypeId)
        {
            const string methodName = "GetCourseList";
            long courseId = 0;
            if (courseEnrollmentId != 0)
            {
                var ce = CourseEnrollmentService.GetById(courseEnrollmentId);
                if (ce == null)
                {
                    LogError(methodName, "CourseEnrollment not found for " + courseEnrollmentId);
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);                
                }
                courseId = ce.CourseSchedule.CourseId;
                if (courseTypeId == 0)
                    courseTypeId = ce.CourseSchedule.Course.CourseTypeId;
            }
            var courses = CourseService.GetCoursesByCourseType(courseTypeId).Where(m => m.IsActive).Select(Converter.ToViewModelMinimum).ToList();
            
            if (courseId!=0)
                AddIfNotExistCourse(courseId, courses);
            return courses;
        }

        private IEnumerable<CourseScheduleViewModel> GetCourseScheduleList(string badge,long courseEnrollmentId, long courseId)
        {
            const string methodName = "GetCourseScheduleList";
            long courseScheduleId = 0;
            if (courseEnrollmentId != 0)
            {
                var ce = CourseEnrollmentService.GetById(courseEnrollmentId);
                if (ce == null)
                {
                    LogError(methodName, "CourseEnrollment not found for " + courseEnrollmentId);
                    throw new FriendlyException(FriendlyExceptionType.ObjectNotFound);
                }
                courseScheduleId = ce.IsUnenrolled?0: ce.CourseScheduleId;                
                if (courseId == 0)
                    courseId = ce.CourseSchedule.CourseId;
            }

            var query =CourseScheduleService.GetCourseSchedulesBetween(DateTime.Now.AddDays(-180), DateTime.Now.AddDays(7),null,m=>m.CourseEnrollments).Where(m => m.CourseId == courseId);                    
            if (!string.IsNullOrWhiteSpace(badge))
                query = query.Where(m => m.CourseEnrollments.Any(m1 => m1.Badge == badge && !m1.IsUnenrolled));
            var temp = query.ToList();
            var courseSchedules =temp.Select(Converter.ToViewModel).ToList();
            
            if (courseScheduleId != 0)                
                AddIfNotExistCourseSchedule(courseScheduleId, courseSchedules);

            return courseSchedules;
        }



        public bool ValidateModel(EnrollmentsPageViewModel model ,ModelStateDictionary modelState)
        {
            var result = true;
            if (model.CourseTypeId <= 0)
            {
                modelState.AddModelError("CourseTypeId", "CourseTpye should be selected.");
                result= false;
            }            
            return result;
        }
        public bool ValidateModel(EnrollmentPageViewModel model, ModelStateDictionary modelState)
        {
            return true;
        }

    }
}

