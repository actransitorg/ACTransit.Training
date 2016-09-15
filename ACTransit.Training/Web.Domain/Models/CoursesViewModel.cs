using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Collections.Generic;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    [DataContract]
    public class CoursesViewModel:ICourseTypeRequired
    {
        public List<SelectListItem> CourseTypes { get; set; }

        public List<CourseViewModel> Courses { get; set; }

        [DisplayName("Just show active courses")]
        public bool JustShowActive { get; set; }

        /// <summary>
        /// Show if it is a readonly view
        /// </summary>
        public bool Readonly { get; set; }
    }
}
