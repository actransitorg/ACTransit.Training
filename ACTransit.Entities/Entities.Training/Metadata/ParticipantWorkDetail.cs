using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
    [MetadataType(typeof(ParticipantWorkDetailMetadata))]
    public partial class ParticipantWorkDetail
    {
    }

    public partial class ParticipantWorkDetailMetadata
    {
        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }

        [ForeignKey("Program")]
        public int ProgramId { get; set; }

        [ForeignKey("WorkCategory")]
        public int WorkCategoryId { get; set; }

        [ForeignKey("ParticipantStatus")]
        public int ParticipantStatusId { get; set; }
    }
}
