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
    
    public partial class Grade
    {
        public Grade()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }
    
        public string LetterGrade { get; set; }
        public string Description { get; set; }
        public bool IsPassing { get; set; }
        public string AddUserId { get; set; }
        public System.DateTime AddDateTime { get; set; }
        public string UpdUserId { get; set; }
        public Nullable<System.DateTime> UpdDateTime { get; set; }
    
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
