using System;
using System.Collections.Generic;
using System.Linq;
using ACTransit.Entities.Employee;
using ACTransit.Entities.Maintenance;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Business.Employee;
using ACTransit.Training.Web.Business.Infrastructure;
using ACTransit.Training.Web.Business.Models;
using ACTransit.Training.Web.Domain.Extensions;
using ACTransit.Training.Web.Domain.Infrastructure;
using Division = ACTransit.Entities.Training.Division;

namespace ACTransit.Training.Web.Domain.Models
{
    internal static class Converter
    {
        const string ShortDateFormat = "MM/dd/yyyy";
        const string TimeFormat = @"HH:mm";
        public static CourseTypeViewModel ToViewModel(CourseType courseType)
        {
            return new CourseTypeViewModel
            {
                CourseTypeId = courseType.CourseTypeId,
                Name = courseType.Name.NullableTrim(),
                Description = courseType.Description.NullableTrim(),
                IsActive = courseType.IsActive
            };
        }
        public static CourseType ToDbObjType(CourseTypeViewModel courseTypeVeiwModel)
        {
            return new CourseType
            {
                CourseTypeId = courseTypeVeiwModel.CourseTypeId,
                Name = courseTypeVeiwModel.Name.NullableTrim(),
                Description = courseTypeVeiwModel.Description.NullableTrim(),
                IsActive = courseTypeVeiwModel.IsActive
            };
        }

        public static TopicViewModel ToViewModel(ComponentTopic componentTopic)
        {
            return new TopicViewModel
            {
                TopicId = componentTopic.TopicId,
                CourseTypeId = componentTopic.CourseTypeId,
                TopicTypeId = componentTopic.TopicTypeId,
                Name = componentTopic.Name.NullableTrim(),
                Description = componentTopic.Description.NullableTrim(),
                IsActive = componentTopic.IsActive,
            };
        }

        public static TopicViewModel ToViewModel(Topic topic)
        {
            return new TopicViewModel
            {
                TopicId = topic.TopicId,
                CourseTypeId = topic.CourseTypeId,
                TopicTypeId = topic.TopicTypeId,
                Name = topic.Name.NullableTrim(),
                Description = topic.Description.NullableTrim(),
                IsActive = topic.IsActive,
            };
        }
        public static Topic ToDbObjType(TopicViewModel topicVeiwModel)
        {
            return new Topic
            {
                TopicId = topicVeiwModel.TopicId,
                CourseTypeId = topicVeiwModel.CourseTypeId,
                TopicTypeId = topicVeiwModel.TopicTypeId,
                Name = topicVeiwModel.Name,
                Description = topicVeiwModel.Description,
                IsActive = topicVeiwModel.IsActive
            };
        }

        /// <summary>
        /// Convert to viewModel without Description.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public static CourseViewModel ToViewModelMinimum(Course course)
        {
            return new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseTypeId = course.CourseTypeId,
                Name = course.Name,
                Description = "",
                IsActive = course.IsActive,
                HasWheelTime = course.HasWheelTime,
                CourseTypeName = course.GetCourseTypeName()
            };
        }
        public static CourseViewModel ToViewModel(Course course)
        {
            return new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseTypeId = course.CourseTypeId,
                Name = course.Name.NullableTrim(),
                Description = course.Description.NullableTrim(),
                IsActive = course.IsActive,
                HasWheelTime = course.HasWheelTime,
                CourseTypeName = course.GetCourseTypeName()
            };
        }
        public static Course ToDbObjType(CourseViewModel courseVeiwModel)
        {
            return new Course
            {
                CourseId = courseVeiwModel.CourseId,
                CourseTypeId = courseVeiwModel.CourseTypeId,
                Name = courseVeiwModel.Name,
                Description = courseVeiwModel.Description,
                IsActive = courseVeiwModel.IsActive,
                HasWheelTime = courseVeiwModel.HasWheelTime
            };
        }

        public static CourseScheduleViewModel ToViewModel(CourseSchedule courseSchedule)
        {
            if (courseSchedule != null)
                return new CourseScheduleViewModel
                {
                    CourseId = courseSchedule.CourseId,
                    CourseScheduleId = courseSchedule.CourseScheduleId,
                    BeginEffDate = courseSchedule.BeginEffDate,
                    BeginEffDateStr = courseSchedule.BeginEffDate.ToString(ShortDateFormat),
                    EndEffDate = courseSchedule.EndEffDate,
                    EndEffDateStr = courseSchedule.EndEffDate.ToString(ShortDateFormat),
                    StartTime = courseSchedule.StartTime,
                    StartTimeStr = courseSchedule.StartTime == null ? "" : ((new DateTime(1, 1, 1, 0, 0, 0)).Add(courseSchedule.StartTime.Value)).ToString(TimeFormat),
                    EndTime = courseSchedule.EndTime,
                    EndTimeStr = courseSchedule.EndTime == null ? "" : ((new DateTime(1, 1, 1, 0, 0, 0)).Add(courseSchedule.EndTime.Value)).ToString(TimeFormat),
                    Note = courseSchedule.Note,
                    Description = courseSchedule.Description,
                    DivisionId = courseSchedule.DivisionId,
                    Frequency = courseSchedule.Frequency,
                    TotalSeat = courseSchedule.TotalSeat
                };
            return null;
        }
        public static CourseScheduleViewModelAjax ToViewModelAjax(CourseSchedule courseSchedule)
        {
            if (courseSchedule!=null)
                return new CourseScheduleViewModelAjax
                {
                    CourseId = courseSchedule.CourseId,
                    CourseScheduleId = courseSchedule.CourseScheduleId,
                    BeginEffDate = courseSchedule.BeginEffDate,
                    BeginEffDateStr = courseSchedule.BeginEffDate.ToString(ShortDateFormat),
                    EndEffDate = courseSchedule.EndEffDate,
                    EndEffDateStr = courseSchedule.EndEffDate.ToString(ShortDateFormat),
                    StartTime = courseSchedule.StartTime.HasValue ? DateTime.Now.Date.Add(courseSchedule.StartTime.Value).TimeOfDay : (TimeSpan?) null,
                    StartTimeStr = courseSchedule.StartTime.HasValue ? DateTime.Now.Date.Add(courseSchedule.StartTime.Value).ToString(TimeFormat) : "",
                    EndTime = courseSchedule.EndTime.HasValue ? DateTime.Now.Date.Add(courseSchedule.EndTime.Value).TimeOfDay : (TimeSpan?) null,
                    EndTimeStr = courseSchedule.EndTime.HasValue ? DateTime.Now.Date.Add(courseSchedule.EndTime.Value).ToString(TimeFormat) : "",
                    IsCurrent = (courseSchedule.EndEffDate >= DateTime.Now.Date.AddDays(-60)),
                    TotalSeat = courseSchedule.TotalSeat.HasValue ? courseSchedule.TotalSeat.Value : 0,
                };
            return null;
        }
        public static CourseSchedule ToDbObjType(CourseScheduleViewModel courseVeiwModel)
        {
            return new CourseSchedule
            {
                CourseId = courseVeiwModel.CourseId,
                CourseScheduleId = courseVeiwModel.CourseScheduleId,
                BeginEffDate = courseVeiwModel.BeginEffDate,
                EndEffDate = courseVeiwModel.EndEffDate,
                StartTime = courseVeiwModel.StartTime,
                EndTime = courseVeiwModel.EndTime,
                Note = courseVeiwModel.Note,
                Description = courseVeiwModel.Description,
                DivisionId = courseVeiwModel.DivisionId,
                Frequency = courseVeiwModel.Frequency,
                TotalSeat = courseVeiwModel.TotalSeat
            };
        }
        
        public static CourseScheduleInstructorViewModel ToViewModel(CourseScheduleInstructor courseScheduleInstructor)
        {
            if (courseScheduleInstructor != null)
                return new CourseScheduleInstructorViewModel
                {
                    CourseScheduleId = courseScheduleInstructor.CourseScheduleId,
                    InstructorId = courseScheduleInstructor.InstructorId,
                    IsPrimary = courseScheduleInstructor.IsPrimary
                };
            return null;
        }
        public static CourseScheduleInstructor ToDbObjType(CourseScheduleInstructorViewModel courseScheduleInstructorViewModel)
        {
            return new CourseScheduleInstructor
            {
                CourseScheduleId = courseScheduleInstructorViewModel.CourseScheduleId,
                InstructorId = courseScheduleInstructorViewModel.InstructorId,
                IsPrimary = courseScheduleInstructorViewModel.IsPrimary
            };
        }

        public static EnrollmentViewModel ToViewModel(EnrollmentBusinessViewModel enrollment)
        {
            if (enrollment != null)
                return new EnrollmentViewModel
                {
                    EnrollmentId = enrollment.EnrollmentId,
                    CourseScheduleId = enrollment.CourseScheduleId,
                    CourseEnrollmentId = enrollment.CourseEnrollmentId,
                    SessionDate = enrollment.SessionDate.Ticks,
                    SessionDateStr = enrollment.SessionDate.ToString(ShortDateFormat),
                    LectureTime = enrollment.LectureTime.ToTimeSpan().Ticks,
                    LectureTimeStr = enrollment.LectureTime.ToTimeSpan().ToTimeFormat(),
                    Note = enrollment.Note,
                    LetterGrade = enrollment.LetterGrade,
                    NoShow = enrollment.IsAbsent,
                    Badge = enrollment.Badge,
                    NonEmployeeId = enrollment.NonEmployeeId.GetValueOrDefault(0),
                    CourseName = enrollment.CourseName
                };
            return null;
        }
        public static EnrollmentViewModel ToViewModel(Enrollment enrollment)
        {
            if (enrollment != null)
                return new EnrollmentViewModel
                {
                    EnrollmentId = enrollment.EnrollmentId,
                    CourseScheduleId = enrollment.GetCourseScheduleId(),
                    CourseEnrollmentId = enrollment.CourseEnrollmentId,
                    SessionDate = enrollment.SessionDate.Ticks,
                    SessionDateStr = enrollment.SessionDate.ToString(ShortDateFormat),
                    LectureTime = enrollment.LectureTime.ToTimeSpan().Ticks,
                    LectureTimeStr =  enrollment.LectureTime.ToTimeSpan().ToTimeFormat(),
                    Note = enrollment.Note,
                    LetterGrade = enrollment.LetterGrade,
                    NoShow = enrollment.IsAbsent,
                    Badge = enrollment.GetBadge(),
                    NonEmployeeId = enrollment.GetNonEmployeeId(),
                    Employee = enrollment.GetEmployee(),                    
                    CourseName = enrollment.GetCourseName(),                    
                };
            return null;
        }
        public static Enrollment ToDbObjType(EnrollmentViewModel enrollmentViewModel)
        {
            return new Enrollment
            {
                EnrollmentId = enrollmentViewModel.EnrollmentId,
                CourseEnrollmentId = enrollmentViewModel.CourseEnrollmentId,
                SessionDate = new DateTime(enrollmentViewModel.SessionDate),
                LectureTime = new TimeSpan(enrollmentViewModel.LectureTime).ToDouble(),
                Note = enrollmentViewModel.Note,
                LetterGrade = enrollmentViewModel.LetterGrade,
                IsAbsent = enrollmentViewModel.NoShow
            };
        }

        public static EnrollmentVehicleViewModel ToViewModel(EnrollmentVehicle enrollmentVehicle)
        {
            if (enrollmentVehicle != null)
                return new EnrollmentVehicleViewModel
                {
                    EnrollmentVehicleId = enrollmentVehicle.EnrollmentVehicleId,
                    EnrollmentId = enrollmentVehicle.EnrollmentId,
                    VehicleId = enrollmentVehicle.VehicleId.NullableTrim(),
                    VehicleDescription = enrollmentVehicle.GetVehicleDescription(),
                    WheelTime =  enrollmentVehicle.WheelTime.HasValue ? enrollmentVehicle.WheelTime.ToTimeSpan().Ticks:(long?)null,
                    WheelTimeStr = enrollmentVehicle.WheelTime.HasValue ? enrollmentVehicle.WheelTime.ToTimeSpan().ToTimeFormat() : "",
                    QualifiedTraining = enrollmentVehicle.QualifiedTraining,
                    OugLiftRampOps = enrollmentVehicle.OUGLiftRampOps,
                    OugSecurement = enrollmentVehicle.OUGSecurement,
                    RouteAlpha = enrollmentVehicle.EnrollmentVehicleRoutes.Select(m => m.RouteAlpha).ToArray(),
                };
            return null;
        }
        public static EnrollmentVehicle ToDbObjType(EnrollmentVehicleViewModel enrollmentVehicleViewModel)
        {
            var result = new EnrollmentVehicle
            {
                EnrollmentVehicleId = enrollmentVehicleViewModel.EnrollmentVehicleId,
                EnrollmentId = enrollmentVehicleViewModel.EnrollmentId,
                VehicleId = enrollmentVehicleViewModel.VehicleId.NullableTrim(),
                WheelTime = enrollmentVehicleViewModel.WheelTime.HasValue ? new TimeSpan(enrollmentVehicleViewModel.WheelTime.GetValueOrDefault()).ToDouble() : (Double?)null,
                QualifiedTraining = enrollmentVehicleViewModel.QualifiedTraining,
                OUGLiftRampOps = enrollmentVehicleViewModel.OugLiftRampOps,
                OUGSecurement = enrollmentVehicleViewModel.OugSecurement,
                EnrollmentVehicleRoutes = enrollmentVehicleViewModel.RouteAlpha==null?null:enrollmentVehicleViewModel.RouteAlpha.Select(m => new EnrollmentVehicleRoute
                {
                    RouteAlpha = m
                    ,EnrollmentVehicleId = enrollmentVehicleViewModel.EnrollmentVehicleId
                }).ToList()
            };
            return result;
        }
      
        public static EnrollmentTopicViewModel ToViewModel(EnrollmentTopic enrollmentTopic)
        {
            if (enrollmentTopic != null)
                return new EnrollmentTopicViewModel
                {
                    EnrollmentTopicId = enrollmentTopic.EnrollmentTopicId,
                    EnrollmentId = enrollmentTopic.EnrollmentId,
                    TopicId = enrollmentTopic.TopicId
                };
            return null;
        }
        public static EnrollmentTopic ToDbObjType(EnrollmentTopicViewModel enrollmentTopicViewModel)
        {
            if (enrollmentTopicViewModel != null)
            {
                return new EnrollmentTopic
                {
                    EnrollmentTopicId = enrollmentTopicViewModel.EnrollmentTopicId,
                    EnrollmentId = enrollmentTopicViewModel.EnrollmentId,
                    TopicId = enrollmentTopicViewModel.TopicId
                };                
            }
            return null;
        }

        public static EnrollmentInstructorViewModel ToViewModel(EnrollmentInstructor enrollmentInstructor)
        {
            if (enrollmentInstructor != null)
                return new EnrollmentInstructorViewModel
                {
                    EnrollmentInstructorId = enrollmentInstructor.EnrollmentInstructorId,
                    EnrollmentId = enrollmentInstructor.EnrollmentId,
                    InstructorId = enrollmentInstructor.InstructorId,
                    IsPrimary = enrollmentInstructor.IsPrimary
                };
            return null;
        }
        public static EnrollmentInstructor ToDbObjType(EnrollmentInstructorViewModel enrollmentInstructorViewModel)
        {
            if (enrollmentInstructorViewModel != null)
            {
                return new EnrollmentInstructor
                {
                    EnrollmentInstructorId = enrollmentInstructorViewModel.EnrollmentInstructorId,
                    EnrollmentId = enrollmentInstructorViewModel.EnrollmentId,
                    InstructorId = enrollmentInstructorViewModel.InstructorId,
                    IsPrimary = enrollmentInstructorViewModel.IsPrimary,
                };
            }
            return null;
        }

        public static CourseEnrollmentViewModel ToViewModel(CourseEnrollment courseEnrollment)
        {
            if (courseEnrollment != null)
                return new CourseEnrollmentViewModel
                {
                    CourseEnrollmentId = courseEnrollment.CourseEnrollmentId,
                    CourseScheduleId = courseEnrollment.CourseScheduleId,
                    NonEmployeeId = courseEnrollment.NonEmployeeId,
                    Badge = string.IsNullOrWhiteSpace(courseEnrollment.Badge) ? "" : courseEnrollment.Badge.PadLeft(6, '0'),
                    Note = courseEnrollment.Note,
                    NoShow = courseEnrollment.IsUnenrolled                  
                };
            return null;
        }

        public static CourseEnrollmentViewModel ToViewModel(CourseScheduleDetail courseScheduleDetail)
        {
            if (courseScheduleDetail != null)
                return new CourseEnrollmentViewModel
                {
                    CourseEnrollmentId = courseScheduleDetail.CourseEnrollmentId.GetValueOrDefault(),
                    CourseScheduleId = courseScheduleDetail.CourseScheduleId,
                    NonEmployeeId = courseScheduleDetail.NonEmployeeId,
                    Badge = string.IsNullOrWhiteSpace(courseScheduleDetail.Badge) ? "" : courseScheduleDetail.Badge.PadLeft(6, '0'),
                    Note = courseScheduleDetail.CourseEnrollmentNote,
                    NoShow = courseScheduleDetail.IsUnenrolled.GetValueOrDefault(),
                    Name = courseScheduleDetail.Trainee,
                    Dept = courseScheduleDetail.TraineeDeptName,
                    Division = courseScheduleDetail.TraineeLocation
                };
            return null;
        }

        public static CourseEnrollment ToDbObjType(CourseEnrollmentViewModel courseEnrollmentViewModel)
        {
            if (courseEnrollmentViewModel != null)
            {
                return new CourseEnrollment
                {
                    CourseEnrollmentId = courseEnrollmentViewModel.CourseEnrollmentId,
                    Badge = courseEnrollmentViewModel.Badge,
                    NonEmployeeId = courseEnrollmentViewModel.NonEmployeeId,
                    Note = courseEnrollmentViewModel.Note,
                    CourseScheduleId = courseEnrollmentViewModel.CourseScheduleId,
                    IsUnenrolled = courseEnrollmentViewModel.NoShow
                };
            }
            return null;
        }

        public static GradeViewModel ToViewModel(Grade grade)
        {
            if (grade != null)
                return new GradeViewModel
                {
                    LetterGrade = grade.LetterGrade,
                    Description = grade.Description,
                    IsPassing = grade.IsPassing
                };
            return null;
        }
        public static Grade ToDbObjType(GradeViewModel gradeViewModel)
        {
            return new Grade
            {
                LetterGrade = gradeViewModel.LetterGrade,
                Description = gradeViewModel.Description,
                IsPassing = gradeViewModel.IsPassing
            };
        }

        public static DivisionViewModel ToViewModel(Division division)
        {
            if (division != null)
                return new DivisionViewModel
                {
                    DivisionId = division.DivisionId,
                    Name = division.Name,
                    Description = division.Description,
                    IsActive = division.IsActive
                };
            return null;
        }
        public static Division ToDbObjType(DivisionViewModel divisionViewModel)
        {
            return new Division
            {
                DivisionId = divisionViewModel.DivisionId,
                Name = divisionViewModel.Name,
                Description = divisionViewModel.Description,
                IsActive = divisionViewModel.IsActive
            };
        }
     
        public static RouteViewModel ToViewModel(RouteList route)
        {
            if (route != null)
                return new RouteViewModel
                {
                    RouteId = route.RouteAlpha,
                    RouteAlpha = route.RouteAlpha
                };
            return null;
        }

        public static VehicleRegisterViewModel ToViewModel(VehicleRegister vehicle)
        {
            if (vehicle != null)
                return new VehicleRegisterViewModel
                {
                    VehicleId = vehicle.VehicleId,
                    Description = vehicle.EquipmentName,
                    Active = string.Equals(vehicle.IsActive,"Y",StringComparison.OrdinalIgnoreCase)
                };
            return null;
        }
        public static VehicleRegister ToDbObjType(VehicleRegisterViewModel vehicleViewModel)
        {
            if (vehicleViewModel != null)
            {
                return new VehicleRegister
                {
                    VehicleId = vehicleViewModel.VehicleId,
                    EquipmentName= vehicleViewModel.Description,
                    IsActive = vehicleViewModel.Active?"Y":"N"
                };
            }
            return null;
        }

        public static EmployeeTrainee ToViewModel(Employee employee)
        {
            if (employee != null)
                return new EmployeeTrainee
                {
                    Badge = employee.Badge.NullableTrim(),
                    LastName = employee.LastName.NullableTrim(),
                    FirstName = employee.FirstName.NullableTrim(),
                    CellPhone = employee.CellPhone.NullableTrim(),
                    BusinessPhone = employee.WorkPhone.NullableTrim(),
                    Department = employee.DeptName.NullableTrim(),
                    Name = employee.Name.NullableTrim(),
                    LoginId = employee.NTLogin.NullableTrim()
                };
            return null;
        }      
        public static Employee ToDbObjType(EmployeeTrainee employeeTrainee)
        {
            if (employeeTrainee != null)
            {
                return new Employee
                {
                    Badge = employeeTrainee.Badge.NullableTrim(),
                    LastName = employeeTrainee.LastName.NullableTrim(),
                    FirstName = employeeTrainee.FirstName.NullableTrim(),
                    CellPhone = employeeTrainee.CellPhone.NullableTrim(),
                    WorkPhone = employeeTrainee.BusinessPhone.NullableTrim(),
                    DeptName = employeeTrainee.Department.NullableTrim(),
                    Name = employeeTrainee.Name.NullableTrim(),
                    NTLogin = employeeTrainee.LoginId.NullableTrim()
                };
            }
            return null;
        }

        public static EmployeeTrainee ToViewModel(EmployeeAll employee)
        {
            if (employee != null)
                return new EmployeeTrainee
                {
                    Badge = employee.Badge.NullableTrim(),
                    LastName = employee.LastName.NullableTrim(),
                    FirstName = employee.FirstName.NullableTrim(),
                    Department = employee.DeptName.NullableTrim(),
                    Name = employee.EmployeeName.NullableTrim(),
                    LoginId= employee.NTLogin.NullableTrim(),
                };
            return null;
        }
       
        public static NonEmployeeViewModel ToViewModel(NonEmployee nonEmployee)
        {
            if (nonEmployee != null)
                return new NonEmployeeViewModel
                {
                    Name = nonEmployee.Name,
                    NonEmployeeId = nonEmployee.NonEmployeeId,
                    IsActive = nonEmployee.IsActive
                };
            return null;
        }

        public static List<InstructorViewModel> ToViewModels(List<Instructor> instructorsWithNonEmployee)
        {
            var model = new List<InstructorViewModel>();
            if (instructorsWithNonEmployee != null)
            {
                var badges = instructorsWithNonEmployee.Where(m => !string.IsNullOrEmpty(m.Badge)).Select(m => m.Badge).ToList();
                var nonEmployees = instructorsWithNonEmployee.Where(m => string.IsNullOrEmpty(m.Badge)).Select(m => new
                {
                    m.InstructorId,
                    m.NonEmployee.Name,
                    NonEmployeeId = m.NonEmployeeId ?? 0,
                    m.IsActive,
                    m.CourseTypeId,
                    CourseTypeName = m.CourseType != null ? m.CourseType.Name : ""
                }).ToList();

                List<EmployeeAll> employees;
                using (var employeeAllService = new EmployeeAllService())
                {
                    employees = employeeAllService.GetEmployeesByBadges(badges.ToArray()).ToList();    
                }
                foreach (var emp in employees)
                {
                    var instructor = instructorsWithNonEmployee.FirstOrDefault(m => m.Badge == emp.Badge);
                    if (instructor == null)
                        continue;
                    
                    model.Add(new InstructorViewModel
                    {
                        Badge = Common.Trim(emp.Badge),
                        NonEmployeeId = 0,
                        Instructor = emp.FullName(),
                        InstructorId = instructor.InstructorId,
                        IsNonEmployee = false,
                        IsActive = instructor.IsActive != null && instructor.IsActive.Value,
                        CourseTypeId = instructor.CourseTypeId.GetValueOrDefault(0),
                        CourseTypeName = instructor.CourseType != null ? instructor.CourseType.Name : ""
                    });
                }

                foreach (var nonEmp in nonEmployees)
                {
                    model.Add(new InstructorViewModel
                    {
                        Badge = "",
                        NonEmployeeId = nonEmp.NonEmployeeId,
                        Instructor = Common.Trim(nonEmp.Name),
                        InstructorId = nonEmp.InstructorId,
                        IsNonEmployee = true,
                        IsActive = nonEmp.IsActive != null && nonEmp.IsActive.Value,
                        CourseTypeId = nonEmp.CourseTypeId.GetValueOrDefault(0),
                        CourseTypeName = nonEmp.CourseTypeName
                    });
                }
            }
            return model.OrderBy(m => m.Instructor).ToList();
        }

    }
}
