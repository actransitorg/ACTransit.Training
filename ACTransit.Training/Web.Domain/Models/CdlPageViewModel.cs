using System.Collections.Generic;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CdlPageViewModel
    {
        
        public List<SelectListItem> CouseSchedules { get; set; }
        public List<SelectListItem> Instructors { get; set; }
        public List<SelectListItem> Grades { get; set; }
        public List<SelectListItem> Topics { get; set; }

        public long CouseEnrollmentId { get; set; }
        public long CourseScheduleId { get; set; }
        public string Badge { get; set; }

        public long PrimaryInstructorId { get; set; }
        public long AssistantInstructorId { get; set; }

        public long SuggestedPrimaryInstructorId { get; set; }  //Comes from courseSchedule, but can be changed
        public long SuggestedAssistantInstructorId { get; set; } //Comes from courseSchedule, but can be changed

        public EnrollmentViewModel Enrollment { get; set; }               
    }

}
