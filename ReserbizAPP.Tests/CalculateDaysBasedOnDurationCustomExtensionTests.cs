using System;
using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class CalculateDaysBasedOnDurationCustomExtensionTests
    {
        [Test]
        public void Should_ReturnCalculatedDaysBasedOnDuration_WhenDurationUnitIsDay()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 7;
            var testDurationUnit = DurationEnum.Day;
            
            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 7;
            Assert.AreEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnIncorrectCalculatedDaysBasedOnDuration_WhenDurationUnitIsDay()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 7;
            var testDurationUnit = DurationEnum.Day;
            
            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 8;
            Assert.AreNotEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnCalculatedDaysBasedOnDuration_WhenDurationUnitIsWeek()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 2;
            var testDurationUnit = DurationEnum.Week;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 14;
            Assert.AreEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnIncorrectCalculatedDaysBasedOnDuration_WhenDurationUnitIsWeek()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 2;
            var testDurationUnit = DurationEnum.Week;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 15;
            Assert.AreNotEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnCalculatedDaysBasedOnDuration_WhenDurationUnitIsMonth()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 3;
            var testDurationUnit = DurationEnum.Month;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 91;
            Assert.AreEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnIncorrectCalculatedDaysBasedOnDuration_WhenDurationUnitIsMonth()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 3;
            var testDurationUnit = DurationEnum.Month;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 92;
            Assert.AreNotEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnCalculatedDaysBasedOnDuration_WhenDurationUnitIsQuarter()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 2;
            var testDurationUnit = DurationEnum.Quarter;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 182;
            Assert.AreEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnIncorrectCalculatedDaysBasedOnDuration_WhenDurationUnitIsQuarter()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 2;
            var testDurationUnit = DurationEnum.Quarter;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 183;
            Assert.AreNotEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnCalculatedDaysBasedOnDuration_WhenDurationUnitIsYear()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 1;
            var testDurationUnit = DurationEnum.Year;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 366;
            Assert.AreEqual(expectedNumberOfDaysDifference, numberOfDays);
        }
        
        [Test]
        public void Should_ReturnIncorrectCalculatedDaysBasedOnDuration_WhenDurationUnitIsYear()
        {
            // Arrange
            var testDate = GetTestDate();
            var testDurationValue = 1;
            var testDurationUnit = DurationEnum.Year;

            // Act
            var numberOfDays = testDate.CalculateDaysBasedOnDuration(testDurationValue, testDurationUnit);

            // Assert
            var expectedNumberOfDaysDifference = 367;
            Assert.AreNotEqual(expectedNumberOfDaysDifference, numberOfDays);
        }

        private DateTime GetTestDate()
        {
            return new DateTime(2019, 09, 15);
        }
    }
}