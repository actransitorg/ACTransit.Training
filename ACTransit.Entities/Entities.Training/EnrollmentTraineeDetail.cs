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
    using System.Collections.Generic;
    
    public partial class EnrollmentTraineeDetail
    {
        public string Trainee { get; set; }
        public string Badge { get; set; }
        public string CourseName { get; set; }
        public System.DateTime SessionDate { get; set; }
        public double LectureTime { get; set; }
        public Nullable<double> WheelTime { get; set; }
        public Nullable<double> TotalMinute { get; set; }
        public Nullable<double> WheelMinute { get; set; }
        public string VehicleId { get; set; }
        public string Vehicle { get; set; }
        public Nullable<bool> QualifiedTraining { get; set; }
        public string RouteAlpha { get; set; }
        public bool IsUnenrolled { get; set; }
        public bool IsAbsent { get; set; }
        public string EquipmentGroupNum { get; set; }
        public string TrainingSeriesName { get; set; }
        public string SessionTerm { get; set; }
        public string TraineeLocation { get; set; }
        public string Division { get; set; }
        public Nullable<long> DivisionId { get; set; }
        public string Note { get; set; }
        public Nullable<long> NonEmployeeId { get; set; }
        public long CourseId { get; set; }
        public long CourseTypeId { get; set; }
        public Nullable<long> EnrollmentVehicleId { get; set; }
        public long EnrollmentId { get; set; }
        public string LetterGrade { get; set; }
        public Nullable<long> InstructorId { get; set; }
        public string Trainer { get; set; }
    }
}