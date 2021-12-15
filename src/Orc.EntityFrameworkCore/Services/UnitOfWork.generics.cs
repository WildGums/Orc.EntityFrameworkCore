namespace Orc.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;

    using IsolationLevel = System.Data.IsolationLevel;

    /// <summary>
    ///     The unit of work base.
    /// </summary>
    /// <typeparam name="TDbContext">
    ///     The database context type.
    /// </typeparam>
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext, IDisposable
    {
        #region Fields
        /// <summary>
        ///     The database context.
        /// </summary>
        private readonly TDbContext _dbContext;

        /// <summary>
        ///     The repository list.
        /// </summary>
        private readonly List<IRepository> _repositories = new List<IRepository>();

        /// <summary>
        /// The service scope.
        /// </summary>
        private readonly IServiceScope _serviceScope;

        /// <summary>
        /// The service provider.
        /// </summary>
        private readonly IServiceProvider _serviceScopeServiceProvider;
        private bool _disposedValue;
        #endregion

        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnitOfWork{TDbContext}" /> class.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The service provider.
        /// </param>
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            Argument.IsNotNull(() => serviceProvider);

            _serviceScope = serviceProvider.CreateScope();
            _serviceScopeServiceProvider = _serviceScope.ServiceProvider;
            _dbContext = _serviceScopeServiceProvider.GetService<TDbContext>();
        }
        #endregion

        #region IUnitOfWork Members
        /// <summary>
        ///     Gets a repository instance.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The entity type.
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     The entity key type.
        /// </typeparam>
        /// <returns>
        ///     The repository instance.
        /// </returns>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var repository = (IRepository<TEntity, TKey>)_serviceScopeServiceProvider.GetService(typeof(IRepository<TEntity, TKey>));
            _repositories.Add(repository);
            return repository;
        }

        /// <summary>
        ///     Save the changes.
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
            foreach (var repository in _repositories)
            {
                repository.Sync();
            }
        }

        /// <summary>
        ///     The begin transaction.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDbContextTransaction" />.
        /// </returns>
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <param name="isolationLevel">
        /// The isolation level.
        /// </param>
        /// <returns>
        /// The <see cref="IDbContextTransaction"/>.
        /// </returns>
        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _dbContext.Database.BeginTransaction(isolationLevel);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _serviceScope.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class UnitOfWork : UnitOfWork<DbContext>
    {
        #region Constructors
        public UnitOfWork(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        #endregion
    }
}
