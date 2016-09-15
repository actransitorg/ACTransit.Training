using System.Collections.Generic;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using System.ComponentModel;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseSchedulesViewModel:ICourseTypeRequired, IPagingRequired
    {
        public CourseSchedulesViewModel()
        {
            CourseSchedules =new List<CourseSchedule>();
            CourseTypes =new List<SelectListItem>();
            Courses = new List<SelectListItem>();
        }

        public List<CourseSchedule> CourseSchedules { get; set; }
        public List<SelectListItem> Courses { get; set; }
        public List<SelectListItem> CourseTypes { get; set; }
        
        [DisplayName("Just show current schedules")]
        public bool JustShowActive { get; set; }

        #region IPagingRequired
        public int RowsPerPage { get; set; }
        public int SkipRows { get; set; }
        public long TotalRows { get; set; }
        #endregion
        
    }
}
