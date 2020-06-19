using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class TermTests
    {
        [Test]
        public void Should_ReturnIsDeletableTrue_WhenNoContractsAttached()
        {
            // Arrange
            var term = new Term();

            // Act
            var result = term.IsDeletable;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_ReturnIsDeletableFalse_WhenHaveContractsAttached()
        {
            // Arrange
            var term = new Term();
            term.Contracts.Add(new Contract());

            // Act
            var result = term.IsDeletable;

            // Assert
            Assert.IsFalse(result);
        }
    
        [TestCase(50, 50)]
        [TestCase(80, 80)]
        [TestCase(100.50f, 100.50f)]
        public void Should_ReturnCorrectPenaltyAmount_WhenValueTypeIsPercentage(float penaltyValue, double expectedPenaltyValue) {

            // Arrange
            var termTest = new Term();
            termTest.PenaltyValueType = ValueTypeEnum.Fixed;
            termTest.PenaltyValue = penaltyValue;

            // Act
            var actualPenaltyAmount = termTest.PenaltyAmount;

            // Assert
            Assert.AreEqual(expectedPenaltyValue, actualPenaltyAmount);
        }

        [TestCase(9000, 1, 90)]
        [TestCase(7500, 5, 375)]
        [TestCase(300.50f, 10, 30.05f)]
        public void Should_ReturnCorrectPenaltyAmount_WhenValueTypeIsPercentage(float termRate, float penaltyValue, float expectedPenaltyValue) {

            // Arrange
            var termTest = new Term();
            termTest.Rate = termRate;
            termTest.PenaltyValueType = ValueTypeEnum.Percentage;
            termTest.PenaltyValue = penaltyValue;

            // Act
            var actualPenaltyAmount = termTest.PenaltyAmount;

            // Assert
            Assert.AreEqual(expectedPenaltyValue, actualPenaltyAmount);
        }
    }
}