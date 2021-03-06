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
    
    public partial class Topic
    {
        public Topic()
        {
            this.CourseTopics = new HashSet<CourseTopic>();
            this.EnrollmentTopics = new HashSet<EnrollmentTopic>();
        }
    
        public long TopicId { get; set; }
        public long CourseTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> TopicTypeId { get; set; }
        public string Code { get; set; }
        public string AddUserId { get; set; }
        public System.DateTime AddDateTime { get; set; }
        public string UpdUserId { get; set; }
        public Nullable<System.DateTime> UpdDateTime { get; set; }
    
        public virtual ICollection<CourseTopic> CourseTopics { get; set; }
        public virtual CourseType CourseType { get; set; }
        public virtual ICollection<EnrollmentTopic> EnrollmentTopics { get; set; }
        public virtual TopicType TopicType { get; set; }
    }
}
