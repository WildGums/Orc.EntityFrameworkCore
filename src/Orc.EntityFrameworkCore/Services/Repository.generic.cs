namespace Orc.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Catel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key.
    /// </typeparam>
    /// <typeparam name="TDbContext">
    /// The database context type.
    /// </typeparam>
    public class Repository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey, TDbContext>
        where TEntity : class where TDbContext : DbContext
    {
        #region Fields
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly TDbContext _context;

        /// <summary>
        /// List of dirty entities.
        /// </summary>
        private readonly List<TEntity> _dirtyEntities = new List<TEntity>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity,TKey,TDbContext}" /> class.
        /// </summary>
        /// <param name="context">
        /// The database context.
        /// </param>
        public Repository(TDbContext context)
        {
            Argument.IsNotNull(() => context);

            _context = context;
        }
        #endregion

        #region IRepository<TEntity,TKey> Members
        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Add(TEntity entity)
        {
            _dirtyEntities.Add(_context.Set<TEntity>().Add(entity).Entity);
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>
        /// The entities.
        /// </returns>
        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbContextTransaction" />.
        /// </returns>
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Indicates whether at least one entity matches with the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// S
        /// The predicate.
        /// </param>
        /// <returns>
        /// <c>True</c> if at least one entity matches with the predicates otherwise <c>False</c>.
        /// </returns>
        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Delete entities match with the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicate
        /// </param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _context.Set<TEntity>().RemoveRange(Find(predicate));
        }

        /// <summary>
        /// Finds entities match with the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicates.
        /// </param>
        /// <returns>
        /// The entities.
        /// </returns>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Gets an entity by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The entity.
        /// </returns>
        public TEntity Get(TKey key)
        {
            return _context.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
            Sync();
        }

        /// <summary>
        /// Save changes async.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            Sync();
        }

        public TEntity TryAddOrUpdate(TEntity entity, params string[] ignoreProperties)
        {
            Argument.IsNotNull(() => entity);

            var keyValues = _context.GetPrimaryKeyValues(entity).ToArray();
            if (keyValues.Length > 0)
            {
                var storedEntity = _context.Set<TEntity>().Find(keyValues);
                if (storedEntity != null)
                {
                    _context.UpdateEntity(storedEntity, entity, ignoreProperties);
                    return storedEntity;
                }
            }

            return _context.Add(entity).Entity;
        }

        /// <summary>
        /// Finds a single entity matches with the specified predicate.
        /// </summary>
        /// <param name="predicate">
        /// The predicates
        /// </param>
        /// <returns>
        /// The entity.
        /// </returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Sync dirty entities.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public void Sync(TEntity entity)
        {
            _context.Entry(entity).Reload();
        }

        /// <summary>
        /// Sync dirty entities with database values.
        /// </summary>
        public void Sync()
        {
            foreach (var dirtyEntity in _dirtyEntities)
            {
                Sync(dirtyEntity);
            }

            _dirtyEntities.Clear();
        }

        public void Update(TEntity entity)
        {
            Argument.IsNotNull(() => entity);

            var keyValues = _context.GetPrimaryKeyValues(entity).ToArray();
            var storedEntity = _context.Set<TEntity>().Find(keyValues);
            if (storedEntity != null)
            {
                _context.UpdateEntity(storedEntity, entity);
            }
        }
        #endregion
    }

    public class Repository<TEntity, TKey> : Repository<TEntity, TKey, DbContext> 
        where TEntity : class
    {
        public Repository(DbContext context) 
            : base(context)
        {
        }
    }
}
