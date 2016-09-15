using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ACTransit.Entities.Maintenance;

namespace ACTransit.DataAccess.Maintenance.UnitOfWork
{
    public class HastusUnitOfWork : UnitOfWork
    {
        public HastusUnitOfWork() : this(new MaintenanceContext(), null) { }
        public HastusUnitOfWork(MaintenanceContext context) : this(context, null) { }
        public HastusUnitOfWork(string currentUserName) : this(new MaintenanceContext(), currentUserName) { }
        public HastusUnitOfWork(MaintenanceContext context, string currentUserName) : base(context) { CurrentUserName = currentUserName; }

        public IEnumerable<VehicleStatusChange> GetVehicleStatusChanges(DateTime? lastRunDate = null, int? skip = null, int? count = null)
        {
            var list = Context.Database.SqlQuery<VehicleStatusChange>("GetHastusVehicleStatusChanges @LastRunDate",
                new SqlParameter("@LastRunDate", lastRunDate.HasValue ? lastRunDate.Value : (Object)DBNull.Value)
                    {
                        IsNullable = true, SqlDbType = SqlDbType.DateTime
                    }).ToList();
            var skipList = (skip != null ? list.Skip(skip.Value) : list);
            var takeList = (count != null ? skipList.Take(count.Value) : skipList);
            return takeList.ToList();
        }

        public IEnumerable<VehicleTypeScheduling> GetVehicleTypeSchedulings()
        {
            return Context.VehicleTypeSchedulings.ToList();
        }
    }
}
