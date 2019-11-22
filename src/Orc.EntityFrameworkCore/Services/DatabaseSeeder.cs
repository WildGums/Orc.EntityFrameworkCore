// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSeeder.cs" company="WildGums">
//   Copyright (c) 2008 - 2019 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if NETCOREAPP3_0
namespace Orc.EntityFrameworkCore
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Catel;

    using Microsoft.AspNetCore.Builder;

    public abstract class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly IApplicationBuilder _appBuilder;

        public DatabaseSeeder(IApplicationBuilder appBuilder)
        {
            Argument.IsNotNull(()=> appBuilder);

            _appBuilder = appBuilder;
        }

        public void InitializeDatabase()
        {
            Migrate();
            SeedAsync().Wait();
        }

        private void Migrate()
        {
            using (var serviceScope = _appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
                context.Database.Migrate();
            }
        }

        protected async virtual Task SeedAsync()
        {
        }
    }
}
#endif

