using System;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.DataAccess.Training.UnitOfWorks;

namespace ACTransit.Training.Web.Business.Training
{
    public abstract class TrainingServiceBase<T> : BaseService where T : class, new()
    {
        protected TrainingServiceBase()
        {
        }

        protected TrainingServiceBase(string currentUserName)
        {            
            UnitOfWork = new UnitOfWork(currentUserName);
        }

        public T GetById<TId>(TId entityId)
        {
            return UnitOfWork.GetById<T, TId>(entityId);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] paths)
        {
            var result = UnitOfWork.Get(paths);
            if (DefaultGetFilter != null)
                result = result.Where(DefaultGetFilter);
            if (filter != null)
                result=result.Where(filter);
            return result;
        }

        protected object AddInternal(T entity)
        {
            UnitOfWork.Create(entity);
            UnitOfWork.SaveChanges();
            return UnitOfWork.GetEntityKeyValue(entity);
        }
        protected object UpdateInternal(T entity)
        {
            entity=UnitOfWork.Update(entity);
            UnitOfWork.SaveChanges();
            return UnitOfWork.GetEntityKeyValue(entity);
        }

        protected object UpdateInternal(T entity, params Expression<Func<T, object>>[] unChangedProperties)
        {
            entity = UnitOfWork.Update(entity, unChangedProperties);
            UnitOfWork.SaveChanges();
            return UnitOfWork.GetEntityKeyValue(entity);
        }

        public virtual void Delete<TId>(TId entityId)
        {
            LogInfo("Delete " + typeof(T).Name, "Deleting " + typeof(T).Name + " :" + entityId);
            UnitOfWork.Delete<T, TId>(entityId);
            UnitOfWork.SaveChanges();
        }

        protected UnitOfWork UnitOfWork { get; set; }

        protected virtual Expression<Func<T, bool>> DefaultGetFilter
        {
            get { return null; }
        }

        protected override void Dispose(bool disposing)
        {
            if (Disposed)
                return;            
            if (disposing)
            {
                if (UnitOfWork != null)
                {
                    UnitOfWork.Dispose();
                    UnitOfWork = null;
                }
            }
            base.Dispose(disposing);
        }

        public void DisableProxy()
        {
            UnitOfWork.DisableProxy();
        }
    }
}
