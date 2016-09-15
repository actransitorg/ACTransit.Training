using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ACTransit.Framework.DataAccess;
using ACTransit.Framework.DataAccess.Extensions;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ACTransit.DataAccess.Training.UnitOfWorks
{
    public class UnitOfWork : UnitOfWorkBase<TrainingEntities>
    {     
        public UnitOfWork() : this(new TrainingEntities(), null) { }
        public UnitOfWork(TrainingEntities context) : this(context, null) { }
        public UnitOfWork(string currentUserName): this(new TrainingEntities(),currentUserName){}
        public UnitOfWork(TrainingEntities context, string currentUserName) : base(context) { CurrentUserName = currentUserName; }

        public object GetEntityKeyValue<T>(T entity) where T:class, new()
        {
            var keyvalues = Context.CreateEntityKey(entity);
            if (keyvalues==null || keyvalues.Length == 0)
                throw new MissingPrimaryKeyException();
            return keyvalues.Length>1 ? keyvalues : keyvalues[0].Value;
        }

        /// <summary>
        /// Update the entered entity. This funtion won't update the properties past to unChangedProperties parameter.
        /// </summary>
        /// <typeparam name="T">Type of the object to update</typeparam>
        /// <param name="entity">The object to save into database.</param>
        /// <param name="unChangedProperties">list of properties that should not to be changed by this operation.</param>
        /// <returns>returns the updated entity.</returns>
        public T Update<T>(T entity, params Expression<Func<T, object>>[] unChangedProperties) where T : class, new()
        {
            var attachedEntity = Context.AttachToOrGet(entity);
            Context.Entry(attachedEntity).CurrentValues.SetValues(entity);

            if (attachedEntity.Equals(entity))
                Context.Entry(attachedEntity).State = EntityState.Modified;
            if (unChangedProperties != null)
            {
                foreach (var prop in unChangedProperties)
                    Context.Entry(attachedEntity).Property(prop).IsModified = false;
            }

            ApplyPreSaveChanges(attachedEntity, false);
            entity = attachedEntity;
            return entity;
        }

        /// <summary>
        /// Delete EnrollmentVehicles along with all of its EnrollmentVehicleRoutes for the given enrollmet. Because this method execute two different query, please run this in a transaction.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <returns>the number of EnrollmentVehicles deleted.</returns>
        public int DeleteEnrollmentVehiclesByEnrollmentId(long enrollmentId)
        {
            const string sql1 = "Delete EnrollmentVehicleRoute WHERE EnrollmentVehicleId IN (SELECT EnrollmentVehicleId FROM  EnrollmentVehicle WHERE EnrollmentId=@EnrollmentId)";
            const string sql2 = "Delete EnrollmentVehicle WHERE EnrollmentId=@EnrollmentId";            
            var parameters1 = new SqlParameter("@EnrollmentId", enrollmentId);
            var parameters2 = new SqlParameter("@EnrollmentId", enrollmentId);            
            ExecuteSqlCommand(sql1, parameters1);
            return ExecuteSqlCommand(sql2, parameters2);            
        }
        public int DeleteEnrollmentTopicsByEnrollmentId(long enrollmentId)
        {
            const string sql = "Delete EnrollmentTopic WHERE EnrollmentId=@EnrollmentId";
            var parameters = new SqlParameter("@EnrollmentId", enrollmentId);
            return ExecuteSqlCommand(sql, parameters);
        }
        public int DeleteEnrollmentInstructorsByEnrollmentId(long enrollmentId)
        {
            const string sql = "Delete EnrollmentInstructor WHERE EnrollmentId=@EnrollmentId";
            var parameters = new SqlParameter("@EnrollmentId", enrollmentId);
            return ExecuteSqlCommand(sql, parameters);
        }

        private int ExecuteSqlCommand(string sql,params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public void DisableProxy()
        {
            Context.Configuration.ProxyCreationEnabled = false;
        }

        public void SyncWithEnterprise()
        {
            var result = ExecuteSqlCommand("Apprentice.SyncWithEnterprise");
        }

        public List<Entities.Training.GetActionItems_Result> GetActionItems(string Badge)
        {
            return Context.GetActionItems(Badge).ToList();
        }
    }

    public static class UnitOfWorkExtensions
    {
        public static int GetKeyPropertyCount<T>(this T t) where T : Type
        {
            var result = 0;
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;
                if (attribute != null) // This property has a KeyAttribute
                    result++;
            }
            return result;
        }

    }
}
