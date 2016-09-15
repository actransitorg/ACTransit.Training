using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(ProgressRatingCellScoreMetadata))]
    public partial class ProgressRatingCellScore : Framework.Interfaces.IAuditableEntity
    {
	}

    public partial class ProgressRatingCellScoreMetadata
    {
        [Column(Order = 0), Key, ForeignKey("Progress")]
        public int ProgressId { get; set; }

        [Column(Order = 1), Key, ForeignKey("RatingCellScore")]
        public int RatingCellScoreId { get; set; }

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