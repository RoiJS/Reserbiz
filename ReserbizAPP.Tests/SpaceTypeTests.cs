using NUnit.Framework;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class SpaceTypeTests
    {
        [Test]
        public void Should_ReturnIsDeleteableTrue_WhenSpaceTypeIsNotAttachedToAnyTerm()
        {
            // Arrange
            var spaceTypeObject = new SpaceType();
            spaceTypeObject.Term = null;

            // Actions
            var result = spaceTypeObject.IsDeletable;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_ReturnIsDeleteableFalse_WhenSpaceTypeIsAttachedToAnyTerm()
        {
            // Arrange
            var spaceTypeObject = new SpaceType();
            spaceTypeObject.Term = new Term();

            // Actions
            var result = spaceTypeObject.IsDeletable;

            // Assert
            Assert.IsFalse(result);
        }
    }
}