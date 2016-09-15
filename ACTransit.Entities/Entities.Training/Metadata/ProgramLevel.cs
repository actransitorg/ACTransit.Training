using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ProgramLevelMetadata))]
	public partial class ProgramLevel : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class ProgramLevelMetadata
    {
        [Key]
        public int ProgramLevelId { get; set; }

        [ForeignKey("ProgramLevelGroup")]
        public int ProgramLevelGroupId { get; set; }

        public int Level { get; set; }

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