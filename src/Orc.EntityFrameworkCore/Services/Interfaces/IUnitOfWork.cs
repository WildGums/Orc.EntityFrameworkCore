namespace Orc.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.Storage;

    /*
     public interface KeyedEntity<KeyType> 
    {
        KeyType Key { set; get; }
    }
    */

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Methods
        /// <summary>
        /// Gets a repository instance.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <typeparam name="TKey">
        /// The entity key.
        /// </typeparam>
        /// <returns>
        /// The repository instance.
        /// </returns>
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;

        /// <summary>
        /// Save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbContextTransaction" />.
        /// </returns>
        IDbContextTransaction BeginTransaction();
        #endregion
    }
}
