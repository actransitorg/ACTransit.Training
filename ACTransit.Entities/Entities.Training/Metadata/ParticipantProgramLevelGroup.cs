using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ParticipantProgramLevelGroupMetadata))]
	public partial class ParticipantProgramLevelGroup : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class ParticipantProgramLevelGroupMetadata
    {
        [Key]
        public int ParticipantProgramLevelGroupId { get; set; }

        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }

        [ForeignKey("ProgramLevelGroup")]
        public int ProgramLevelGroupId { get; set; }

        public System.DateTime BeginEffDate { get; set; }

        public Nullable<System.DateTime> EndEffDate { get; set; }

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