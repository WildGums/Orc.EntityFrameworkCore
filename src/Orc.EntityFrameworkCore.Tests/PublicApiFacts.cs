namespace Orc.EntityFrameworkCore.Tests
{
    using System.Runtime.CompilerServices;
    using NUnit.Framework;
    using PublicApiGenerator;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_EntityFrameworkCore_HasNoBreakingChanges()
        {
            var assembly = typeof(DbContextExtensions).Assembly;

            ApiGenerator.GeneratePublicApi(assembly);
        }
    }
}
