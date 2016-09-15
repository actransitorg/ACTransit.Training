using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class NonEmployeesPageViewModel
    {
        public NonEmployeesPageViewModel()
        {
            JustShowActive = true;
            NonEmployees = new List<NonEmployeeViewModel>();
        }

        [DisplayName("Just show active non employees")]        
        public bool JustShowActive { get; set; }

        
        public List<NonEmployeeViewModel> NonEmployees { get; set; }

    }

    public class NonEmployeeViewModel:ViewModelBase
    {
        public long NonEmployeeId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }
    }
}
