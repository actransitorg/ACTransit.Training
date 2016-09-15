using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    public class InstructorsPageViewModel:ICourseTypeRequired
    {
        public InstructorsPageViewModel()
        {
            JustShowActive = true;
            Instructors=new List<InstructorViewModel>();
            CourseTypes=new List<SelectListItem>();
        }

        [DisplayName("Just show active instructors")]        
        public bool JustShowActive { get; set; }

        [DisplayName("Just show non employees")]
        public bool JustShowNonEmployees { get; set; }

        public List<InstructorViewModel> Instructors { get; set; }

        public List<SelectListItem> CourseTypes { get; set; }
    }

    public class InstructorPageViewModel:ViewModelBase,ICourseTypeRequired
    {
        public InstructorPageViewModel()
        {
            NonEmployees = new List<SelectListItem>();
            Instructor=new InstructorViewModel();
            CourseTypes = new List<SelectListItem>();
        }

        public List<SelectListItem> NonEmployees { get; set; }
        public InstructorViewModel Instructor { get; set; }
        public List<SelectListItem> CourseTypes { get; set; }
    }

    public class InstructorViewModel
    {
        public long InstructorId { get; set; }
        
        public long? NonEmployeeId { get; set; }
        [MaxLength(10)]
        public string Badge { get; set; }
        [MaxLength(255)]
        public string Instructor { get; set; }

        public bool IsNonEmployee { get; set; }

        public long? CourseTypeId { get; set; }
        [MaxLength(50)]
        public string CourseTypeName { get; set; }

        [DisplayName("Active")]
        [DefaultValue(true)]
        public bool? IsActive { get; set; }
    }

}
