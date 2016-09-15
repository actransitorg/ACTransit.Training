using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ParticipantStatusMetadata))]
	public partial class ParticipantStatus
    {
	}
	
    public partial class ParticipantStatusMetadata
    {
        [Key]
        public int ParticipantStatusId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

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
