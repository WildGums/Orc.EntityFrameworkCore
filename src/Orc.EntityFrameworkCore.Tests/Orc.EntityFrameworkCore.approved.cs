[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Orc.EntityFrameworkCore.Tests")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.7", FrameworkDisplayName=".NET Framework 4.7")]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.EntityFrameworkCore
{
    public class static DbContextExtensions
    {
        public static Microsoft.EntityFrameworkCore.Metadata.IEntityType GetModelEntityType(this Microsoft.EntityFrameworkCore.DbContext context, System.Type entityType) { }
        public static System.Collections.Generic.IEnumerable<object> GetPrimaryKeyValues<TEntity>(this Microsoft.EntityFrameworkCore.DbContext context, TEntity entity)
            where TEntity :  class { }
        public static void UpdateEntity<TEntity>(this Microsoft.EntityFrameworkCore.DbContext context, TEntity storedEntity, TEntity entity, params string[] ignoreProperties)
            where TEntity :  class { }
    }
    public class EntityTypeException : System.Exception
    {
        public EntityTypeException(string message) { }
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
        bool Contains(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        void Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        System.Linq.IQueryable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        TEntity Get(TKey key);
        void SaveChanges();
        System.Threading.Tasks.Task SaveChangesAsync();
        TEntity Single(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate);
        void Sync(TEntity entity);
        TEntity TryAddOrUpdate(TEntity entity, params string[] ignoreProperties);
        void Update(TEntity entity);
    }
    public interface IRepository<TEntity, TKey, TDbContext> : Orc.EntityFrameworkCore.IRepository, Orc.EntityFrameworkCore.IRepository<TEntity, TKey>
        where TEntity :  class { }
    public interface IUnitOfWork
    {
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();
        Orc.EntityFrameworkCore.IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity :  class
        ;
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
        public bool Contains(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public void Delete(TEntity entity) { }
        public void Delete(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public System.Linq.IQueryable<TEntity> Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public TEntity Get(TKey key) { }
        public void SaveChanges() { }
        public System.Threading.Tasks.Task SaveChangesAsync() { }
        public TEntity Single(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate) { }
        public void Sync(TEntity entity) { }
        public void Sync() { }
        public TEntity TryAddOrUpdate(TEntity entity, params string[] ignoreProperties) { }
        public void Update(TEntity entity) { }
    }
    public class UnitOfWork : Orc.EntityFrameworkCore.UnitOfWork<Microsoft.EntityFrameworkCore.DbContext>
    {
        public UnitOfWork(System.IServiceProvider serviceProvider) { }
    }
    public class UnitOfWork<TDbContext> : Orc.EntityFrameworkCore.IUnitOfWork
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext, System.IDisposable
    {
        public UnitOfWork(System.IServiceProvider serviceProvider) { }
        public Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction() { }
        public void Dispose() { }
        public Orc.EntityFrameworkCore.IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity :  class { }
        public void SaveChanges() { }
    }
}