﻿[assembly: System.Resources.NeutralResourcesLanguage("en-US")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Orc.EntityFrameworkCore.Tests")]
[assembly: System.Runtime.Versioning.TargetFramework(".NETCoreApp,Version=v6.0", FrameworkDisplayName="")]
public static class LoadAssembliesOnStartup { }
public static class ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.EntityFrameworkCore
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeder(this Microsoft.AspNetCore.Builder.IApplicationBuilder @this) { }
    }
    public class DatabaseSeeder : Orc.EntityFrameworkCore.IDatabaseSeeder
    {
        public DatabaseSeeder() { }
        public void InitializeDatabase(Microsoft.AspNetCore.Builder.IApplicationBuilder appBuilder) { }
        protected virtual System.Threading.Tasks.Task SeedAsync(Microsoft.AspNetCore.Builder.IApplicationBuilder appBuilder) { }
    }
    public static class DbContextExtensions
    {
        public static Microsoft.EntityFrameworkCore.Metadata.IEntityType GetModelEntityType(this Microsoft.EntityFrameworkCore.DbContext context, System.Type entityType) { }
        public static System.Collections.Generic.IEnumerable<object?> GetPrimaryKeyValues<TEntity>(this Microsoft.EntityFrameworkCore.DbContext context, TEntity entity)
            where TEntity :  class { }
        public static void UpdateEntity<TEntity>(this Microsoft.EntityFrameworkCore.DbContext context, TEntity storedEntity, TEntity entity, params string[] ignoreProperties)
            where TEntity :  class { }
    }
    public class EntityTypeException : System.Exception
    {
        public EntityTypeException(string message) { }
    }
    public interface IDatabaseSeeder
    {
        void InitializeDatabase(Microsoft.AspNetCore.Builder.IApplicationBuilder @this);
    }
    public interface IRepository
    {
        void Sync();
    }
    public interface IRepository<TEntity, TKey> : Orc.EntityFrameworkCore.IRepository
        where TEntity :  class
    {
        void Add(TEntity entity);
        System.Linq.IQueryable<TEntity> All();
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel);
        bool Contains(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        void Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        System.Linq.IQueryable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        TEntity? Get(TKey key);
        void SaveChanges();
        System.Threading.Tasks.Task SaveChangesAsync();
        TEntity Single(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        void Sync(TEntity entity);
        TEntity TryAddOrUpdate(TEntity entity, params string[] ignoreProperties);
        void Update(TEntity entity);
    }
    public interface IRepository<TEntity, TKey, TDbContext> : Orc.EntityFrameworkCore.IRepository, Orc.EntityFrameworkCore.IRepository<TEntity, TKey>
        where TEntity :  class { }
    public interface IUnitOfWork : System.IDisposable
    {
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel);
        Orc.EntityFrameworkCore.IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity :  class;
        void SaveChanges();
    }
    public class Repository<TEntity, TKey> : Orc.EntityFrameworkCore.Repository<TEntity, TKey, Microsoft.EntityFrameworkCore.DbContext>
        where TEntity :  class
    {
        public Repository(Microsoft.EntityFrameworkCore.DbContext context) { }
    }
    public class Repository<TEntity, TKey, TDbContext> : Orc.EntityFrameworkCore.IRepository, Orc.EntityFrameworkCore.IRepository<TEntity, TKey>, Orc.EntityFrameworkCore.IRepository<TEntity, TKey, TDbContext>
        where TEntity :  class
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public Repository(TDbContext context) { }
        public void Add(TEntity entity) { }
        public System.Linq.IQueryable<TEntity> All() { }
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction() { }
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel) { }
        public bool Contains(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public void Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public void Delete(TEntity entity) { }
        public System.Linq.IQueryable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public TEntity? Get(TKey key) { }
        public void SaveChanges() { }
        public System.Threading.Tasks.Task SaveChangesAsync() { }
        public TEntity Single(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public void Sync() { }
        public void Sync(TEntity entity) { }
        public TEntity TryAddOrUpdate(TEntity entity, params string[] ignoreProperties) { }
        public void Update(TEntity entity) { }
    }
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseSeeder(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection) { }
        public static void AddDatabaseSeeder<TDatabaseSeeder>(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection)
            where TDatabaseSeeder :  class, Orc.EntityFrameworkCore.IDatabaseSeeder { }
        public static void AddOrcEntityFrameworkCore(this Microsoft.Extensions.DependencyInjection.IServiceCollection serviceCollection) { }
    }
    public class UnitOfWork : Orc.EntityFrameworkCore.UnitOfWork<Microsoft.EntityFrameworkCore.DbContext>
    {
        public UnitOfWork(System.IServiceProvider serviceProvider) { }
    }
    public class UnitOfWork<TDbContext> : Orc.EntityFrameworkCore.IUnitOfWork, System.IDisposable
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext, System.IDisposable
    {
        public UnitOfWork(System.IServiceProvider serviceProvider) { }
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction() { }
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction(System.Data.IsolationLevel isolationLevel) { }
        public void Dispose() { }
        protected virtual void Dispose(bool disposing) { }
        public Orc.EntityFrameworkCore.IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity :  class { }
        public void SaveChanges() { }
    }
}