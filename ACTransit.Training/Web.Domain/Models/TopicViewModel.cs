using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class TopicViewModel : ViewModelBase
    {
        public long TopicId { get; set; }
        public long CourseTypeId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string CourseTypeName { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        public long? TopicTypeId { get; set; }

    }
}
