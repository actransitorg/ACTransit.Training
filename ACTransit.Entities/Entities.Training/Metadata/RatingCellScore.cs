using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(RatingCellScoreMetadata))]
	public partial class RatingCellScore : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class RatingCellScoreMetadata
    {
        [Key]
        public int RatingCellScoreId { get; set; }

        [ForeignKey("RatingCell")]
        public int RatingCellId { get; set; }

        public int Score { get; set; }

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
