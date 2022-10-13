namespace Orc.EntityFrameworkCore
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseSeeder(this IApplicationBuilder @this)
        {
            ArgumentNullException.ThrowIfNull(@this);

            using (var serviceScope = @this.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
                context.InitializeDatabase(@this);
            }
        }
    }
}
