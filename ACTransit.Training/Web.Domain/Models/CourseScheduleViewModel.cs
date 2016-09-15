using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Training.Web.Domain.Models
{
    public class CourseScheduleViewModel:ViewModelBase
    {
        public CourseScheduleViewModel()
        {
            CourseScheduleInstructors=new List<CourseScheduleInstructorViewModel>();
        }

        public long CourseScheduleId { get; set; }
        public long CourseId { get; set; }
        [MaxLength(50)]
        public string Note { get; set; }
        public DateTime BeginEffDate { get; set; }
        [MaxLength(50)]
        public string BeginEffDateStr { get; set; }
        public DateTime EndEffDate { get; set; }
        [MaxLength(50)]
        public string EndEffDateStr { get; set; }
        public TimeSpan? StartTime{ get; set; }
        [MaxLength(50)]
        public string StartTimeStr { get; set; }
        public TimeSpan? EndTime { get; set; }
        [MaxLength(50)]
        public string EndTimeStr { get; set; }
        public int? TotalSeat { get; set; }
        public long? DivisionId { get; set; }
        public long? Frequency { get; set; }
        public string Description { get; set; }
        public List<CourseScheduleInstructorViewModel> CourseScheduleInstructors { get; set; }
    }
}
