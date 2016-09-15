using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
    [MetadataType(typeof(ParticipantWorkSeedMetadata))]
    public partial class ParticipantWorkSeed : Framework.Interfaces.IAuditableEntity
    {
    }

    public partial class ParticipantWorkSeedMetadata
    {
        [Key]
        public int ParticipantWorkSeedId { get; set; }

        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }

        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [ForeignKey("WorkCategory")]
        public int WorkCategoryId { get; set; }

        public System.DateTime StartDate { get; set; }

        public int WorkHour { get; set; }

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
