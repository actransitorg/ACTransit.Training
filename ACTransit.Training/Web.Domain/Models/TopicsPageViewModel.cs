using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    public class TopicsPageViewModel : ICourseTypeRequired
    {
        public TopicsPageViewModel()
        {
            JustShowActive = true;
            Topics = new List<TopicViewModel>();
        }

        [DisplayName("Just show active topics")]
        public bool JustShowActive { get; set; }


        public List<TopicViewModel> Topics { get; set; }

        public List<SelectListItem> CourseTypes { get; set; }
    }

    public class TopicPageViewModel : TopicViewModel, ICourseTypeRequired
    {
        public List<SelectListItem> CourseTypes { get; set; }

    }

}
