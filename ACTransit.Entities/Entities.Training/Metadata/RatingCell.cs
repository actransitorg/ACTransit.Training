using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(RatingCellMetadata))]
	public partial class RatingCell : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class RatingCellMetadata
    {
        [Key]
        public int RatingCellId { get; set; }

        [ForeignKey("ProgramLevelGroup")]
        public int ProgramLevelGroupId { get; set; }

        [ForeignKey("RatingCategory")]
        public int RatingCategoryId { get; set; }

        public int SortOrderCategory { get; set; }

        [ForeignKey("RatingArea")]
        public int RatingAreaId { get; set; }

        public int SortOrderArea { get; set; }

        [ForeignKey("RatingItem")]
        public int RatingItemId { get; set; }

        public int SortOrderCell { get; set; }
        
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
