using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ProgramLevelGroupMetadata))]
	public partial class ProgramLevelGroup : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class ProgramLevelGroupMetadata
    {
        [Key]
        public int ProgramLevelGroupId { get; set; }

        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int StartLevel { get; set; }

        public int EndLevel { get; set; }

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
