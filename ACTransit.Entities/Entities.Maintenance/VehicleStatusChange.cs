using System;
using System.ComponentModel.DataAnnotations;

namespace ACTransit.Entities.Maintenance
{
    public class VehicleStatusChange
    {
        [Key]
        public string VehicleIdentifier { get; set; }
        public DateTime? LastChangedDate { get; set; }
        public DateTime? InServiceDate { get; set; }
        public DateTime? LastOperatingStatDate { get; set; }
        public DateTime? DisposalDate { get; set; }
        public string VehicleType { get; set; }

        public string LastEquipmentNum { get; set; }
        public string CurrentEquipmentNum { get; set; }
        public string LastEquipmentName { get; set; }
        public string CurrentEquipmentName { get; set; }
        public bool? LastIsActive { get; set; }
        public bool? CurrentIsActive { get; set; }
        public string LastEquipmentStatus { get; set; }
        public string CurrentEquipmentStatus { get; set; }
        public string LastDivision { get; set; }
        public string CurrentDivision { get; set; }
        public bool? LastIsAccessible { get; set; }
        public bool? CurrentIsAccessible { get; set; }
        public bool? LastHasApc { get; set; }
        public bool? CurrentHasApc { get; set; }
        public bool? LastAllowBicycles { get; set; }
        public bool? CurrentAllowBicycles { get; set; }
        public bool? LastHasCamera { get; set; }
        public bool? CurrentHasCamera { get; set; }
        public bool? LastHasCustomPaint { get; set; }
        public bool? CurrentHasCustomPaint { get; set; }
        public bool? LastHasWifi { get; set; }
        public bool? CurrentHasWifi { get; set; }
        public bool? LastIsDisposed { get; set; }
        public bool? CurrentIsDisposed { get; set; }
    }
}
