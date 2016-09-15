using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACTransit.Entities.Maintenance
{
    [Table("VehicleRegister")]
    public partial class VehicleRegister
    {
        [StringLength(4)]
        public string VehicleId { get; set; }

        [StringLength(2)]
        public string Division { get; set; }

        [StringLength(24)]
        public string AcctCode { get; set; }

        [StringLength(40)]
        public string EquipmentName { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(12)]
        public string EquipmentNum { get; set; }

        //[Key]
        //[Column(Order = 0)]
        [StringLength(2)]
        public string EquipmentClass { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [StringLength(2)]
        public string EquipmentStatus { get; set; }

        [StringLength(5)]
        public string LocationCode { get; set; }

        [StringLength(9)]
        public string StockCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateInService { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [StringLength(1)]
        public string CostingFlag { get; set; }

        //[Key]
        //[Column(Order = 3)]
        [StringLength(1)]
        public string IsActive { get; set; }

        [StringLength(30)]
        public string SerialNum { get; set; }

        [StringLength(41)]
        public string PurchasePrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DisposalDate { get; set; }

        [StringLength(10)]
        public string DisposalDocNum { get; set; }

        //[Key]
        //[Column(Order = 4)]
        public decimal SalePrice { get; set; }

        [StringLength(6)]
        public string EquipmentType { get; set; }

        [StringLength(30)]
        public string LicenseNum { get; set; }

        //[Key]
        //[Column(Order = 5)]
        [StringLength(4)]
        public string CompCode { get; set; }

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
