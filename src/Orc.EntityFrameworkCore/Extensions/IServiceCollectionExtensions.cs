// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceCollectionExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2019 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.EntityFrameworkCore
{
    using Microsoft.Extensions.DependencyInjection;

    public static class IServiceCollectionExtensions
    {
        public static void AddEntityFrameworkCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApplicationDatabaseSeeder<TDatabaseSeeder>(this IServiceCollection serviceCollection)
            where TDatabaseSeeder : class, IDatabaseSeeder
        {
            serviceCollection.AddTransient<IDatabaseSeeder, TDatabaseSeeder>();
        }
    }
}
