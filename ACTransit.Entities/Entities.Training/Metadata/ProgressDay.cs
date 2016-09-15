using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ProgressDayMetadata))]
	public partial class ProgressDay : Framework.Interfaces.IAuditableEntity
    {
        [JsonIgnore]
	    public bool IsDayOff
	    {
            get { return ApprenticeDayOff; }
	    }

        [JsonIgnore]
        public virtual ICollection<ParticipantWork> ParticipantWork { get; set; }
    }
	
    public partial class ProgressDayMetadata
    {
        [Key]
        public int ProgressDayId { get; set; }

        [ForeignKey("Progress")]
        public int ProgressId { get; set; }

        [ForeignKey("Division")]
        public long DivisionId { get; set; }

        [ForeignKey("DailyPerformance")]
        public Nullable<int> DailyPerformanceId { get; set; }

        [ForeignKey("ProgramLevel")]
        public int ProgramLevelId { get; set; }

        public System.DateTime CalendarDate { get; set; }

        public bool ApprenticeDayOff { get; set; }

        [StringLength(6)]
        public string SupervisorBadge { get; set; }

        [StringLength(2048)]
        public string Comment { get; set; }

        public Nullable<System.DateTime> CommentDate { get; set; }

        [StringLength(6)]
        public string CommentBadge { get; set; }

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
