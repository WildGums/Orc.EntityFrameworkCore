namespace Orc.EntityFrameworkCore
{
    using System.Transactions;

    using Microsoft.EntityFrameworkCore.Storage;

    using IsolationLevel = System.Data.IsolationLevel;

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

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">
        ///     The isolation level.
        /// </param>
        /// <returns>
        /// The <see cref="IDbContextTransaction"/>.
        /// </returns>
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
        #endregion
    }
}
