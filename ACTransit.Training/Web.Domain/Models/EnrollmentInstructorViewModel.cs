namespace ACTransit.Training.Web.Domain.Models
{
    public class EnrollmentInstructorViewModel
    {
        public long EnrollmentInstructorId { get; set; }
        public long EnrollmentId{ get; set; }
        public long InstructorId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
