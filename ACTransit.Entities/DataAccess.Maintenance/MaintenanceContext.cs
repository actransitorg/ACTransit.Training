using System.Data.Entity;
using ACTransit.Entities.Maintenance;

namespace ACTransit.DataAccess.Maintenance
{
    public partial class MaintenanceContext : DbContext
    {
        public MaintenanceContext()
            : base("name=MaintenanceEntities")
        {
        }

        static MaintenanceContext()
        {
            Database.SetInitializer<MaintenanceContext>(null); 
        }

        public virtual DbSet<EquipmentRegister> EquipmentRegisters { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleRegister> VehicleRegisters { get; set; }
        public virtual DbSet<VehicleStatusChange> VehicleStatusChanges { get; set; }
        public virtual DbSet<VehicleTypeScheduling> VehicleTypeSchedulings { get; set; }
    }
}
