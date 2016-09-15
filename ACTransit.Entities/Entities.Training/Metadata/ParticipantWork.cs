using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Training
{
    [MetadataType(typeof(ParticipantWorkMetadata))]
    public partial class ParticipantWork : Framework.Interfaces.IAuditableEntity
    {
    }

    public partial class ParticipantWorkMetadata
    {
        [Key, Column(Order =0)]
        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }

        [Key, Column(Order = 1)]
        public string WorkOrderNum { get; set; }

        [Key, Column(Order = 2)]
        public string TaskNum { get; set; }

        public bool IsReadOnly { get; set; }

        public System.DateTime StartDate { get; set; }

        [ForeignKey("WorkCategory")]
        public int WorkCategoryId { get; set; }

        public System.DateTime WorkDate { get; set; }

        public decimal WorkHour { get; set; }

        public string VehicleId { get; set; }

        public string SeriesNum { get; set; }

        public string TrainingSeriesName { get; set; }

        public string CompCode { get; set; }

        public string CompDescription { get; set; }

        public string TaskComment { get; set; }

        public string JobDescCode { get; set; }

    }
}
