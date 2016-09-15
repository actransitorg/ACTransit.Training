using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseTypeViewModel
    {
        public long CourseTypeId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
