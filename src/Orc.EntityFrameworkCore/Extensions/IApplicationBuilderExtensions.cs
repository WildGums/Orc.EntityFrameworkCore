// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationBuilderExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2019 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if NETCOREAPP3_0
namespace Orc.EntityFrameworkCore
{
    using Catel;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class IApplicationBuilderExtensions
    {
        #region Methods
        public static void UseDatabaseSeeder(this IApplicationBuilder @this)
        {
            Argument.IsNotNull(() => @this);

            using (var serviceScope = @this.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
                context.InitializeDatabase(@this);
            }
        }

        #endregion
    }
}
#endif
