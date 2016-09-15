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
    
    public partial class ParticipantProgramLevelGroup
    {
        public ParticipantProgramLevelGroup()
        {
            this.Progresses = new HashSet<Progress>();
        }
    
        public int ParticipantProgramLevelGroupId { get; set; }
        public int ParticipantId { get; set; }
        public int ProgramLevelGroupId { get; set; }
        public System.DateTime BeginEffDate { get; set; }
        public Nullable<System.DateTime> EndEffDate { get; set; }
        public string AddUserId { get; set; }
        public System.DateTime AddDateTime { get; set; }
        public string UpdUserId { get; set; }
        public Nullable<System.DateTime> UpdDateTime { get; set; }
    
        public virtual Participant Participant { get; set; }
        public virtual ICollection<Progress> Progresses { get; set; }
        public virtual ProgramLevelGroup ProgramLevelGroup { get; set; }
    }
}