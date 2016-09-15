using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseEnrollmentViewModel
    {
        public long CourseEnrollmentId { get; set; }
        public long CourseScheduleId { get; set; }
        public long? NonEmployeeId { get; set; }
        [MaxLength(10)]
        public string Badge { get; set; }
        public string Name { get; set; }        
        [MaxLength(255)]
        public string Note { get; set; }
        [MaxLength(1000)]
        public string Dept { get; set; }
        [MaxLength(50)]
        public string Division { get; set; }
        public bool NoShow { get; set; }
    }
}
