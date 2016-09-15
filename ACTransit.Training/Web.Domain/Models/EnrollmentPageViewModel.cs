using System.Collections.Generic;

namespace ACTransit.Training.Web.Domain.Models
{

    public class EnrollmentPageViewModel : ViewModelBase
    {
        public EnrollmentPageViewModel()
        {
            Enrollments = new List<EnrollmentViewModel>();
            CourseTypes = new List<CourseTypeViewModel>();
            Courses = new List<CourseViewModel>();
            CourseSchedules = new List<CourseScheduleViewModel>();
            Grades = new List<GradeViewModel>();
            Topics = new List<TopicViewModel>();
            NonEmployees=new List<NonEmployeeViewModel>();
            Instructors = new List<InstructorViewModel>();
            Routes=new List<RouteViewModel>();
            Vehicles = new List<VehicleRegisterViewModel>();
        }
        public long CourseEnrollmentId { get; set; }
        public long CourseTypeId { get; set; }
        public long CourseId { get; set; }
        public long CourseScheduleId { get; set; }

        public string PersonName { get; set; }
        public string EmployeeBadge { get; set; }
        public long NonEmployeeId { get; set; }

        public bool IncludeEnrollments { get; set; }
        public bool IncludeCourseTypes { get; set; }
        public bool IncludeCourses{ get; set; }
        public bool IncludeCourseSchedules{ get; set; }
        public bool IncludeRoutes { get; set; }
        public bool IncludeVehicles { get; set; }
        public bool IncludeNonEmployees { get; set; }
        public bool IncludeInstructors{ get; set; }
        public bool IncludeTopics{ get; set; }
        public bool IncludeGrades{ get; set; }

        public List<EnrollmentViewModel> Enrollments { get; set; }
        public List<CourseTypeViewModel> CourseTypes { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<CourseScheduleViewModel> CourseSchedules { get; set; }

        public List<GradeViewModel> Grades { get; set; }
        public List<TopicViewModel> Topics { get; set; }
        public List<NonEmployeeViewModel> NonEmployees { get; set; }
        public List<InstructorViewModel> Instructors { get; set; }
        public List<RouteViewModel> Routes { get; set; }
        public List<VehicleRegisterViewModel> Vehicles { get; set; }
    }
}
