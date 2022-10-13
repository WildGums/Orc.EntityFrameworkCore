namespace Orc.EntityFrameworkCore
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public  class DatabaseSeeder : IDatabaseSeeder
    {
        public void InitializeDatabase(IApplicationBuilder appBuilder)
        {
            ArgumentNullException.ThrowIfNull(appBuilder);

            Migrate(appBuilder);
            SeedAsync(appBuilder).Wait();
        }

        private void Migrate(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
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
