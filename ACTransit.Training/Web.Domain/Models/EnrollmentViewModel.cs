using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class EnrollmentViewModel:ViewModelBase
    {
        public EnrollmentViewModel()
        {
            EnrollmentVehicles=new List<EnrollmentVehicleViewModel>();
            EnrollmentTopics=new List<EnrollmentTopicViewModel>();
            EnrollmentInstructors = new List<EnrollmentInstructorViewModel>();
        }
        public long EnrollmentId { get; set; }        
        public long CourseEnrollmentId { get; set; }
        public long CourseScheduleId{ get; set; }
        public long NonEmployeeId { get; set; }
        [MaxLength(10)]
        public string Badge { get; set; }
        [MaxLength(255)]
        public string Employee { get; set; }        
        [MaxLength(128)]
        public string CourseName { get; set; }
        /// <summary>
        /// Number of ticks
        /// </summary>
        public long SessionDate { get; set; }
        public string SessionDateStr { get; set; }
        /// <summary>
        /// number of ticks
        /// </summary>
        public long LectureTime { get; set; }
        public string LectureTimeStr { get; set; }
        [MaxLength(1000)]
        public string Note { get; set; }
        [MaxLength(2)]
        public string LetterGrade { get; set; }
        public bool NoShow { get; set; }
        public List<EnrollmentVehicleViewModel> EnrollmentVehicles { get; set; }
        public List<EnrollmentTopicViewModel> EnrollmentTopics { get; set; }
        public List<EnrollmentInstructorViewModel> EnrollmentInstructors { get; set; }
    }
}
