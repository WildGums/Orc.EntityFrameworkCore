namespace Orc.EntityFrameworkCore
{
    using Microsoft.AspNetCore.Builder;

    public interface IDatabaseSeeder
    {
        void InitializeDatabase(IApplicationBuilder @this);
    }
}
