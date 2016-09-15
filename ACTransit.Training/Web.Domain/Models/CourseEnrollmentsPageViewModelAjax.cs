using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Collections.Generic;
using ACTransit.Training.Web.Domain.Interfaces;

namespace ACTransit.Training.Web.Domain.Models
{
    [DataContract]
    public class CourseEnrollmentsPageViewModelAjax:ICourseTypeRequired
    {
        public CourseEnrollmentsPageViewModelAjax()
        {
            CourseTypes=new List<SelectListItem>();
            CourseSchedules = new List<SelectListItem>();
            Courses=new List<CourseViewModelAjax>();
        }

        public List<SelectListItem> CourseTypes { get; set; }

        public List<SelectListItem> CourseSchedules { get; set; }

        [DisplayName("Just show current schedules")]
        public bool JustShowCurrent { get; set; }
        public string Badge { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo{ get; set; }
        public long CourseId { get; set; }
        public long CourseScheduleId { get; set; }
        public List<CourseViewModelAjax> Courses { get; set; }
    }

    public class CourseViewModelAjax
    {
        public CourseViewModelAjax()
        {
            CourseSchedules = new List<CourseScheduleViewModelAjax>();
        }
        public long CourseTypeId { get; set; }
        public long CourseId { get; set; }
        public string Name { get; set; }
        public bool Collapsed { get; set; }
        public List<CourseScheduleViewModelAjax> CourseSchedules { get; set; }
    }

    public class CourseScheduleViewModelAjax
    {
        public CourseScheduleViewModelAjax()
        {
            CourseEnrollments = new List<CourseEnrollmentViewModel>();
        }
        public long CourseId { get; set; }
        public long CourseScheduleId { get; set; }
        public DateTime BeginEffDate { get; set; }
        public string BeginEffDateStr { get; set; }
        public DateTime EndEffDate { get; set; }
        public string EndEffDateStr { get; set; }
        public TimeSpan? StartTime { get; set; }
        public string StartTimeStr { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string EndTimeStr { get; set; }
        public int TotalSeat { get; set; }
        public bool Collapsed { get; set; }
        public bool IsCurrent { get; set; }
        public List<CourseEnrollmentViewModel> CourseEnrollments { get; set; }
    }
   

}
