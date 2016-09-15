using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Maintenance
{
    [Table("EquipmentRegister")]
    public partial class EquipmentRegister
    {
        [Key]
        [StringLength(12)]
        public string EquipmentNum { get; set; }

        [Required]
        [StringLength(40)]
        public string EquipmentName { get; set; }

        [Required]
        [StringLength(9)]
        public string StockCode { get; set; }

        [Required]
        [StringLength(30)]
        public string PlantNum { get; set; }

        [Required]
        [StringLength(12)]
        public string EquipmentGroupNum { get; set; }

        [Required]
        [StringLength(2)]
        public string EquipmentClass { get; set; }

        [Required]
        [StringLength(2)]
        public string EquipmentStatus { get; set; }

        [Required]
        [StringLength(5)]
        public string LocationCode { get; set; }

        [Required]
        [StringLength(1)]
        public string CostingFlag { get; set; }

        [Required]
        [StringLength(1)]
        public string IsActive { get; set; }

        [Required]
        [StringLength(24)]
        public string AcctCode { get; set; }

        [Required]
        [StringLength(30)]
        public string SerialNum { get; set; }

        public decimal PurchasePrice { get; set; }

        [Required]
        [StringLength(8)]
        public string PurchaseDate { get; set; }

        [Required]
        [StringLength(8)]
        public string DisposalDate { get; set; }

        [Required]
        [StringLength(10)]
        public string DisposalDocNum { get; set; }

        public decimal SalePrice { get; set; }

        [Required]
        [StringLength(6)]
        public string EquipmentType { get; set; }

        [Required]
        [StringLength(30)]
        public string LicenseNum { get; set; }

        [Required]
        [StringLength(4)]
        public string CompCode { get; set; }

        [StringLength(2)]
        public string EquipmentCode01 { get; set; }

        [StringLength(2)]
        public string EquipmentCode02 { get; set; }
        
        [StringLength(2)]
        public string EquipmentCode19 { get; set; }
        
        [StringLength(2)]
        public string EquipmentCode20 { get; set; }

        public int? RC30 { get; set; }

        public int? RC60 { get; set; }

        public int? RC90 { get; set; }

        public int? Mileage30 { get; set; }

        public int? Mileage60 { get; set; }

        public int? Mileage90 { get; set; }

        public decimal? MBRC30 { get; set; }

        public decimal? MBRC60 { get; set; }

        public decimal? MBRC90 { get; set; }
    }
}
