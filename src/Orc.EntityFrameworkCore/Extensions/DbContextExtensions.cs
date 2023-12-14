namespace Orc.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catel.Logging;
    using Catel.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public static class DbContextExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static IEnumerable<object?> GetPrimaryKeyValues<TEntity>(this DbContext context, TEntity entity) 
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(entity);

            var entityType = typeof(TEntity);
            var modelEntityType = context.GetModelEntityType(entityType);

            var primaryKey = modelEntityType.GetKeys().FirstOrDefault(key => key.IsPrimaryKey());
            if (primaryKey is not null)
            {
                foreach (var primaryKeyProperty in primaryKey.Properties)
                {
                    var propertyInfo = entityType.GetPropertyEx(primaryKeyProperty.Name);
                    if (propertyInfo is not null)
                    {
                        yield return propertyInfo.GetValue(entity);
                    }
                }
            }
        }

        public static void UpdateEntity<TEntity>(this DbContext context, TEntity storedEntity, TEntity entity, params string[] ignoreProperties)
            where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(storedEntity);
            ArgumentNullException.ThrowIfNull(entity);

            var entityType = typeof(TEntity);
            var modelEntityType = context.GetModelEntityType(entityType);

            foreach (var property in modelEntityType.GetProperties())
            {
                if (!ignoreProperties.Contains(property.Name))
                {
                    var propertyInfo = entityType.GetPropertyEx(property.Name);
                    if (propertyInfo is not null)
                    {
                        propertyInfo.SetValue(storedEntity, propertyInfo.GetValue(entity));
                    }
                }
            }

            context.Set<TEntity>().Update(storedEntity);
        }

        public static IEntityType GetModelEntityType(this DbContext context, Type entityType)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(entityType);

            var modelEntityType = context.Model.FindEntityType(entityType);
            if (modelEntityType is null)
            {
                throw Log.ErrorAndCreateException<EntityTypeException>("The entity type '{0}' is not available in this context", entityType.FullName);
            }

            return modelEntityType;
        }
    }
}
