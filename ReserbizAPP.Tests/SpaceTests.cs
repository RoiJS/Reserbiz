using System;
using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class SpaceTests
    {

        [Test]
        public void Should_ReturnIsNotOccupiedTrue_WhenNoContractAttached()
        {
            // Arrange
            var space = new Space();

            // Act
            var result = space.IsNotOccupied;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_ReturnIsNotOccupiedFalse_WhenHasContractAttached()
        {
            // Arrange
            var space = new Space();
            space.Contracts.Add(new Contract());

            // Act
            var result = space.IsNotOccupied;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_ReturnOccupiedByContractIdActualValue_WhenHasNonArchivedContractAttached()
        {
            // Arrange
            var space = new Space();
            var contract = new Contract
            {
                Id = 1,
                EffectiveDate = new DateTime(2020, 10, 06),
                DurationUnit = DurationEnum.Month,
                DurationValue = 12,
                IsActive = true
            };
            contract.SetCurrentDateTime(new DateTime());

            space.Contracts.Add(contract);

            // Act
            var result = space.OccupiedByContractId;

            // Assert
            Assert.AreEqual(result, 1);
        }

        [Test]
        public void Should_ReturnOccupiedByContractIdZero_WhenHasArchivedContractAttached()
        {
            // Arrange
            var space = new Space();
            var contract = new Contract
            {
                Id = 1,
                EffectiveDate = new DateTime(2020, 10, 06),
                DurationUnit = DurationEnum.Month,
                DurationValue = 12,
                IsActive = false
            };
            contract.SetCurrentDateTime(new DateTime());

            space.Contracts.Add(contract);

            // Act
            var result = space.OccupiedByContractId;

            // Assert
            Assert.AreEqual(result, 0);
        }

    }
}