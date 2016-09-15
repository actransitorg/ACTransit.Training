using System.Web.Mvc;
using System.Collections.Generic;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseEnrollmentPageViewModelAjax:ViewModelBase,ICourseTypeRequired
    {
        public CourseEnrollmentPageViewModelAjax()
        {
            CourseTypes = new List<SelectListItem>();
            NonEmployees = new List<SelectListItem>();
            CourseSchedules = new List<SelectListItem>();
        }
        public List<SelectListItem> CourseTypes { get; set; }
        public List<SelectListItem> NonEmployees { get; set; }
        public CourseEnrollmentViewModel CourseEnrollment { get; set; }
        public List<SelectListItem> CourseSchedules { get; set; }
        public bool HasEnrollment { get; set; }
    }
}
