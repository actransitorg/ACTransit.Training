using System;
using System.Data;
using ACTransit.DataAccess.Maintenance;
using ACTransit.Framework.DataAccess;
using ACTransit.Framework.DataAccess.Extensions;

namespace ACTransit.DataAccess.Maintenance.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase<MaintenanceContext>
    {
        public UnitOfWork() : this(new MaintenanceContext(), null) { }
        public UnitOfWork(MaintenanceContext context) : this(context, null) { }
        public UnitOfWork(string currentUserName) : this(new MaintenanceContext(), currentUserName) { }
        public UnitOfWork(MaintenanceContext context, string currentUserName) : base(context) { CurrentUserName = currentUserName; }

        public object GetEntityKeyValue<T>(T entity) where T:class, new()
        {
            var keyvalues = Context.CreateEntityKey(entity);
            if (keyvalues==null || keyvalues.Length!=1)
                throw new MissingPrimaryKeyException();
            if (keyvalues.Length>1) 
                throw new Exception("more than one Key found.");

            return keyvalues[0].Value;
        }
    }
}
