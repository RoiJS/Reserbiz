using NUnit.Framework;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class TenantTests
    {
        [Test]
        public void Should_ReturnIsDeletableTrue_WhenNoContracts()
        {
            // Arrange
            var tenant = new Tenant();

            // Act
            var result = tenant.IsDeletable;

            // Arrange
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_ReturnIsDeletableFalse_WhenHaveContracts()
        {

            // Arrange
            var tenant = new Tenant();
            tenant.Contracts.Add(new Contract());
            tenant.Contracts.Add(new Contract());

            // Act
            var result = tenant.IsDeletable;

            // Arrange
            Assert.IsFalse(result);
        }
    }
}