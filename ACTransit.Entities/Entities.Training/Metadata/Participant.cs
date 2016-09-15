using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ParticipantMetadata))]
	public partial class Participant : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class ParticipantMetadata
    {
        [Key]
        public int ParticipantId { get; set; }

        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [StringLength(6)]
        public string Badge { get; set; }
		
        public Nullable<System.DateTime> CompletedDate { get; set; }
        
        [Display(Name = "Estimated Completed Date")]
        public Nullable<System.DateTime> EstimatedCompletedDate { get; set; }

        [ForeignKey("ParticipantStatus")]
        public int ParticipantStatusId { get; set; }

        [ForeignKey("ProgramLevel")]
        public Nullable<int> ProgramLevelId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string AddUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime AddDateTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string UpdUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> UpdDateTime { get; set; }
        public bool UseEmployeeStep { get; set; }
	}	
}
