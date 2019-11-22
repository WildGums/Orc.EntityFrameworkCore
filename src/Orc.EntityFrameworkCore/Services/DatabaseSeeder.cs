﻿// --------------------------------------------------------------------------------------------------------------------
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

        public DatabaseSeeder()
        {
        }

        public void InitializeDatabase(IApplicationBuilder appBuilder)
        {
            Argument.IsNotNull(() => appBuilder);

            Migrate(appBuilder);
            SeedAsync(appBuilder).Wait();
        }

        private void Migrate(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
                context.Database.Migrate();
            }
        }

        protected async virtual Task SeedAsync(IApplicationBuilder appBuilder)
        {
        }
    }
}
#endif

