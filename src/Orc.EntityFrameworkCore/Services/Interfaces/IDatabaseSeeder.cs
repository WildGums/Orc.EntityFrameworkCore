// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseSeeder.cs" company="WildGums">
//   Copyright (c) 2008 - 2019 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Orc.EntityFrameworkCore
{
    using Microsoft.AspNetCore.Builder;

    public interface IDatabaseSeeder
    {
        void InitializeDatabase(IApplicationBuilder @this);
    }
}
