namespace Orc.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Logging;
    using Catel.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public static class DbContextExtensions
    {
        #region Constants
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Methods
        public static IEnumerable<object> GetPrimaryKeyValues<TEntity>(this DbContext context, TEntity entity) 
            where TEntity : class
        {
            Argument.IsNotNull(() => entity);

            var entityType = typeof(TEntity);
            var modelEntityType = context.GetModelEntityType(entityType);

            var primaryKey = modelEntityType.GetKeys().FirstOrDefault(key => key.IsPrimaryKey());
            if (primaryKey is not null)
            {
                foreach (var primaryKeyProperty in primaryKey.Properties)
                {
                    var propertyInfo = entityType.GetPropertyEx(primaryKeyProperty.Name);
                    yield return propertyInfo.GetValue(entity);
                }
            }
        }

        public static void UpdateEntity<TEntity>(this DbContext context, TEntity storedEntity, TEntity entity, params string[] ignoreProperties)
            where TEntity : class
        {
            Argument.IsNotNull(() => storedEntity);
            Argument.IsNotNull(() => entity);

            var entityType = typeof(TEntity);
            var modelEntityType = context.GetModelEntityType(entityType);

            foreach (var property in modelEntityType.GetProperties())
            {
                if (!ignoreProperties.Contains(property.Name))
                {
                    var propertyInfo = entityType.GetPropertyEx(property.Name);
                    propertyInfo.SetValue(storedEntity, propertyInfo.GetValue(entity));
                }
            }

            context.Set<TEntity>().Update(storedEntity);
        }

        public static IEntityType GetModelEntityType(this DbContext context, Type entityType)
        {
            Argument.IsNotNull(() => context);
            Argument.IsNotNull(() => entityType);

            var modelEntityType = context.Model.FindEntityType(entityType);
            if (modelEntityType is null)
            {
                throw Log.ErrorAndCreateException<EntityTypeException>("The entity type '{0}' is not available in this context", entityType.FullName);
            }

            return modelEntityType;
        }
#endregion
    }
}
