//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACTransit.Entities.Training
{
    using System;
    
    public partial class GetAttendanceList_Result
    {
        public Nullable<long> EnrollmentId { get; set; }
        public Nullable<System.DateTime> SessionDate { get; set; }
        public Nullable<double> LectureTime { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
        public string Note { get; set; }
        public Nullable<long> CourseRosterRow { get; set; }
        public string CourseName { get; set; }
        public string Division { get; set; }
        public string Trainee { get; set; }
        public string Badge { get; set; }
        public string TraineeDeptName { get; set; }
        public string TraineeLocation { get; set; }
        public string SessionTerm { get; set; }
        public System.DateTime BeginEffDate { get; set; }
        public System.DateTime EndEffDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public string CourseType { get; set; }
        public long CourseTypeId { get; set; }
        public bool IsUnenrolled { get; set; }
        public long CourseId { get; set; }
        public Nullable<long> DivisionId { get; set; }
        public long CourseScheduleId { get; set; }
        public long CourseEnrollmentId { get; set; }
        public Nullable<long> NonEmployeeId { get; set; }
        public string CourseEnrollmentNote { get; set; }
    }
}
