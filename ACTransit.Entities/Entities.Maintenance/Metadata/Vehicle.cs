using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Maintenance
{
    [MetadataType(typeof(Vehicle))]
    public partial class VehicleMetaData
    {
        [Key]
        [StringLength(4)]
        public string Veh { get; set; }

        [StringLength(4)]
        public string Series { get; set; }

        [StringLength(40)]
        public string Description { get; set; }

        public int? TagNum { get; set; }

        [StringLength(1)]
        public string REV { get; set; }

        [StringLength(2)]
        public string EquipStatus { get; set; }

        [StringLength(1)]
        public string ActiveFlag { get; set; }

        [StringLength(2)]
        public string Div { get; set; }

        [StringLength(30)]
        public string SerialNum { get; set; }

        [StringLength(30)]
        public string LICENSE { get; set; }

        public int? RC30 { get; set; }

        public int? RC60 { get; set; }

        public int? RC90 { get; set; }

        public int? Miles30 { get; set; }

        public int? Miles60 { get; set; }

        public int? Miles90 { get; set; }

        public int? MBRC30 { get; set; }

        public int? MBRC60 { get; set; }

        public int? MBRC90 { get; set; }

        public int? Mileage { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DateInService { get; set; }
    }

}
