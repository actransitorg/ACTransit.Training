using System;

namespace ACTransit.Training.Web.Business.Models
{
    public class EnrollmentBusinessViewModel
    {
        public long EnrollmentId{ get; set; }
        public long CourseEnrollmentId{ get; set; }
        public long CourseId{ get; set; }
        public long CourseScheduleId{ get; set; }
        public long? NonEmployeeId { get; set; }        
        public string Badge { get; set; }        
        public string CourseName { get; set; }


        public bool IsAbsent { get; set; }
        public double? LectureTime { get; set; }
        public string LetterGrade { get; set; }

        public string Note { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
