using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ProgressMetadata))]
	public partial class Progress : Framework.Interfaces.IAuditableEntity
    {
        [JsonIgnore]
        public virtual ICollection<ParticipantWork> ParticipantWork { get; set; }

        [JsonIgnore]
        public DateTime EndDate { get { return StartDate.AddDays(6); } }
    }
	
    public partial class ProgressMetadata
    {
        [Key]
        public int ProgressId { get; set; }

        [ForeignKey("ParticipantProgramLevelGroup")]
        public int ParticipantProgramLevelGroupId { get; set; }

        public System.DateTime StartDate { get; set; }

        public int ScoreTotal { get; set; }

        public Nullable<System.DateTime> EvaluationDate { get; set; }

        [StringLength(6)]
        public string SupervisorBadge { get; set; }

        [StringLength(6)]
        public string SuperintendentBadge { get; set; }

        public Nullable<System.DateTime> SuperintendentApprovalDate { get; set; }

        [StringLength(27)]
        public string ScheduledDaysOff { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string AddUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime AddDateTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string UpdUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> UpdDateTime { get; set; }
	}	
}
