using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(DailyPerformanceProgramLevelGroupMetadata))]
	public partial class DailyPerformanceProgramLevelGroup : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class DailyPerformanceProgramLevelGroupMetadata
    {
        [Key]
        public int DailyPerformanceProgramLevelGroupId { get; set; }

        [ForeignKey("DailyPerformance")]
        public int DailyPerformanceId { get; set; }

        [ForeignKey("ProgramLevelGroup")]
        public int ProgramLevelGroupId { get; set; }

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
