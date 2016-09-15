namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseScheduleInstructorViewModel
    {
        public long CourseScheduleId { get; set; }
        public long InstructorId { get; set; }
        public bool IsPrimary{ get; set; }
    }
}
