using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.DataAccess.Training.UnitOfWorks;
using ACTransit.Framework.Caching;
using ACTransit.Framework.Logging;
using ACTransit.Training.Web.Business.Infrastructure;

namespace ACTransit.Training.Web.Business
{
    public abstract class BaseService : IDisposable 
    {        
        protected readonly Logger Logger;
        

        protected BaseService()
        {
            Logger = new Logger("Business." + GetType().Name);
        }

        //TODO: Shouldn't we actually pull this out of a KPIServiceBase, and just use a logging utility/helper/service?
        protected void LogDebug(string methodName, string message)
        {
            Logger.WriteDebug(message);
        }
        protected void LogInfo(string methodName, string message)
        {
            Logger.Write(message);
        }
        protected void LogError(string methodName, string message)
        {
            Logger.WriteError(message);
        }
        protected void LogFatal(string methodName, string message)
        {
            Logger.WriteFatal(message);
        }

       
        public ICache Cache
        {
            get { return Common.Cache; }
        }

        public abstract void RefreshCache();


        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                //Dispose here
            }

            Disposed = true;
        }
    }
}
