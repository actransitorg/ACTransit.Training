using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CoursePageViewModel : ViewModelBase
    {
        public CoursePageViewModel()
        {
            Course=new CourseViewModel();
            CourseTypes = new List<CourseTypeViewModel>();
        }
        public List<CourseTypeViewModel> CourseTypes { get; set; }
        public CourseViewModel Course { get; set; }
        public bool HasEnrollment { get; set; }
    }

    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Topics = new List<TopicViewModel>();
            ComponentTopics = new List<TopicViewModel>();
        }
        public long CourseId { get; set; }
        public long CourseTypeId { get; set; }
        [MaxLength(50)]
        public string CourseTypeName { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool HasWheelTime { get; set; }
        public List<TopicViewModel> Topics { get; set; }
        public List<TopicViewModel> ComponentTopics { get; set; }
        
    }
}
