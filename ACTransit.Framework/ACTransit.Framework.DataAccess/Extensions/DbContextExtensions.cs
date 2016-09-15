using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTransit.Framework.DataAccess.Extensions
{
    public static class DbContextExtensions
    {


        public static T AttachToOrGet<T>(this IObjectContextAdapter contextAdapter, T entity) 
            where T : class
        {
            var context = contextAdapter.ObjectContext;
            string entitySetName = context.CreateObjectSet<T>().EntitySet.Name;
            T attachedEntity;
            ObjectStateEntry entry;
            // Track whether we need to perform an attach
            bool attach = false;
            if (context.ObjectStateManager.TryGetObjectStateEntry(context.CreateEntityKey(entitySetName, entity),out entry))
            {                
                // Re-attach if necessary
                attach = entry.State == EntityState.Detached;
                // Get the discovered entity to the ref
                attachedEntity = (T)entry.Entity;
            }
            else
            {
                attachedEntity = entity;
                // Attach for the first time
                attach = true;
            }
            if (attach)
                context.AttachTo(entitySetName, attachedEntity);
            return attachedEntity;
        }

        public static string EntitySetName<T>(this IObjectContextAdapter contextAdapter, T entity)
                   where T : class
        {
            var context = contextAdapter.ObjectContext;
            return context.CreateObjectSet<T>().EntitySet.Name;
        }

        public static EntityKeyMember[] CreateEntityKey<T>(this IObjectContextAdapter contextAdapter, T entity)
           where T : class
        {
            var context = contextAdapter.ObjectContext;
            var entityKey=context.CreateEntityKey(context.EntitySetName(entity), entity);
            if (entityKey==null)
                throw new Exception("No key found!");
            return entityKey.EntityKeyValues;
        }

    }
}
