using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(WorkCategoryMetadata))]
	public partial class WorkCategory : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class WorkCategoryMetadata
    {
        [Key]
        public int WorkCategoryId { get; set; }

        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int HourGoal { get; set; }

        public Nullable<int> SortOrder { get; set; }

        public Nullable<System.DateTime> InactiveDate { get; set; }

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
