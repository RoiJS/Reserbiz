using System.Collections.Generic;
using NUnit.Framework;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class SpaceTypeTests
    {
        [Test]
        public void Should_ReturnIsDeleteableTrue_WhenSpaceTypeIsNotAttachedToAnyTermAndSpace()
        {
            // Arrange
            var spaceTypeObject = new SpaceType();
            spaceTypeObject.Terms = new List<Term>();
            spaceTypeObject.Spaces = new List<Space>();

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
            spaceTypeObject.Spaces = new List<Space>();
            spaceTypeObject.Terms = new List<Term>()
            {
                new Term(),
                new Term()
            };

            // Actions
            var result = spaceTypeObject.IsDeletable;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_ReturnIsDeleteableFalse_WhenSpaceTypeIsAttachedToAnySpace()
        {
            // Arrange
            var spaceTypeObject = new SpaceType();
            spaceTypeObject.Terms = new List<Term>();
            spaceTypeObject.Spaces = new List<Space>()
            {
                new Space(),
                new Space()
            };

            // Actions
            var result = spaceTypeObject.IsDeletable;

            // Assert
            Assert.IsFalse(result);
        }
    }
}