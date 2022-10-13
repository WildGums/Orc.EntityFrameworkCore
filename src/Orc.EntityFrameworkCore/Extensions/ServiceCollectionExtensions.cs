namespace Orc.EntityFrameworkCore
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static void AddOrcEntityFrameworkCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddDatabaseSeeder<TDatabaseSeeder>(this IServiceCollection serviceCollection)
            where TDatabaseSeeder : class, IDatabaseSeeder
        {
            serviceCollection.AddScoped<IDatabaseSeeder, TDatabaseSeeder>();
        }

        public static void AddDatabaseSeeder(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
        }
    }
}
