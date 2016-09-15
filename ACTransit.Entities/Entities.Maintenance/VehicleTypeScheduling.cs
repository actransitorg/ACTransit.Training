using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Maintenance
{
    [Table("VehicleTypeScheduling")]
    public class VehicleTypeScheduling
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(18)]
        public string Code { get; set; }

        [StringLength(10)]
        public string VehicleType { get; set; }

        [StringLength(10)]
        public string VehicleGroup { get; set; }

        [StringLength(128)]
        public string Description { get; set; }
    }
}
