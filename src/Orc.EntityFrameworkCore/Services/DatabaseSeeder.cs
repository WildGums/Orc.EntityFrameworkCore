// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSeeder.cs" company="WildGums">
//   Copyright (c) 2008 - 2019 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if !NET47
namespace Orc.EntityFrameworkCore
{
    using System.Threading.Tasks;

    using Catel;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public  class DatabaseSeeder : IDatabaseSeeder
    {
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
