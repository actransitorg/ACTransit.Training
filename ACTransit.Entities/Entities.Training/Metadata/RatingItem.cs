using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
	[MetadataType(typeof(RatingItemMetadata))]
	public partial class RatingItem : Framework.Interfaces.IAuditableEntity
    {
	}
	
    public partial class RatingItemMetadata
    {
        [Key]
        public int RatingItemId { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public Nullable<int> SortOrder { get; set; }
        
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
