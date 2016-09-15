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
    
    public partial class Participant
    {
        public Participant()
        {
            this.ParticipantProgramLevelGroups = new HashSet<ParticipantProgramLevelGroup>();
            this.ParticipantWorks = new HashSet<ParticipantWork>();
            this.ParticipantWorkSeeds = new HashSet<ParticipantWorkSeed>();
        }
    
        public int ParticipantId { get; set; }
        public int ProgramId { get; set; }
        public string Badge { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> EstimatedCompletedDate { get; set; }
        public int ParticipantStatusId { get; set; }
        public Nullable<int> ProgramLevelId { get; set; }
        public bool UseEmployeeStep { get; set; }
        public string AddUserId { get; set; }
        public System.DateTime AddDateTime { get; set; }
        public string UpdUserId { get; set; }
        public Nullable<System.DateTime> UpdDateTime { get; set; }
    
        public virtual ParticipantStatus ParticipantStatus { get; set; }
        public virtual Program Program { get; set; }
        public virtual ProgramLevel ProgramLevel { get; set; }
        public virtual ICollection<ParticipantProgramLevelGroup> ParticipantProgramLevelGroups { get; set; }
        public virtual ICollection<ParticipantWork> ParticipantWorks { get; set; }
        public virtual ICollection<ParticipantWorkSeed> ParticipantWorkSeeds { get; set; }
    }
}