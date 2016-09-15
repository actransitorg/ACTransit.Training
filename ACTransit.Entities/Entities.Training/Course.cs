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
    
    public partial class Course
    {
        public Course()
        {
            this.CourseSchedules = new HashSet<CourseSchedule>();
            this.CourseTopics = new HashSet<CourseTopic>();
        }
    
        public long CourseId { get; set; }
        public long CourseTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool HasWheelTime { get; set; }
        public string AddUserId { get; set; }
        public System.DateTime AddDateTime { get; set; }
        public string UpdUserId { get; set; }
        public Nullable<System.DateTime> UpdDateTime { get; set; }
    
        public virtual CourseType CourseType { get; set; }
        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; }
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
    }
}