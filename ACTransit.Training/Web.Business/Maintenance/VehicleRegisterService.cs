using ACTransit.Entities.Maintenance;

namespace ACTransit.Training.Web.Business.Maintenance
{
    public class VehicleRegisterService:MaintenanceServiceBase<VehicleRegister>
    {
        public VehicleRegisterService(string currentUserName) : base(currentUserName) { }
        public override void RefreshCache()
        {            
        }
    }
}
