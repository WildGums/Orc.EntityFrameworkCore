namespace Orc.EntityFrameworkCore.Tests
{
    using System.Runtime.CompilerServices;
    using ApiApprover;
    using NUnit.Framework;

    [TestFixture]
    public class PublicApiFacts
    {
        [Test, MethodImpl(MethodImplOptions.NoInlining)]
        public void Orc_EntityFrameworkCore_HasNoBreakingChanges()
        {
            var assembly = typeof(DbContextExtensions).Assembly;

            PublicApiApprover.ApprovePublicApi(assembly);
        }
    }
}
