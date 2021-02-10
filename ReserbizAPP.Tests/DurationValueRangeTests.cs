using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Custom_Validations;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class DurationValueRangeTests
    {

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(364)]
        [TestCase(365)]
        public void Should_ReturnNotNull_WhenDurationUnitIsDay_And_ValueIsNotLessThanZero_Or_NotGreaterThan365(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Day };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNull(result);
        }

        [TestCase(-1)]
        [TestCase(366)]
        public void Should_ReturnNotNull_WhenDurationUnitIsDay_And_ValueIsLessThanZero_Or_GreaterThan365(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Day };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNotNull(result);
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(51)]
        [TestCase(52)]
        public void Should_ReturnNull_WhenDurationUnitIsWeek_And_ValueIsGreaterThanZero_Or_NotGreaterThan52(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Week };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNull(result);
        }

        [TestCase(-1)]
        [TestCase(53)]
        public void Should_ReturnNotNull_WhenDurationUnitIsWeek_And_ValueIsLessThanZero_Or_GreaterThan52(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Week };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(11)]
        [TestCase(12)]
        public void Should_ReturnNull_WhenDurationUnitIsMonth_And_ValueIsGreaterThanZero_Or_NotGreaterThan12(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Month };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNull(result);
        }

        [TestCase(-1)]
        [TestCase(13)]
        public void Should_ReturnNotNull_WhenDurationUnitIsMonth_And_ValueIsLessThanZero_Or_GreaterThan12(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Month };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNotNull(result);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        public void Should_ReturnNull_WhenDurationUnitIsQuarter_And_ValueIsGreaterThanZero_Or_NotGreaterThan4(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Quarter };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNull(result);
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void Should_ReturnNotNull_WhenDurationUnitIsQuarter_And_ValueIsLessThanZero_Or_GreaterThan4(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Quarter };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNotNull(result);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        public void Should_ReturnNull_WhenDurationUnitIsYear_And_ValueIsGreaterThanZero_Or_NotGreaterThan1(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Year };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNull(result);
        }

        [TestCase(-1)]
        [TestCase(2)]
        public void Should_ReturnNotNull_WhenDurationUnitIsYear_And_ValueIsLessThanZero_Or_GreaterThan1(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = GetDurationValue("DurationUnit");
            var termObj = new TermForManageDto { DurationUnit = DurationEnum.Year };

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(value, new ValidationContext(termObj));

            // Assert
            Assert.IsNotNull(result);
        }

        private DurationValueRange GetDurationValue(string propertyName)
        {
            return new DurationValueRange(propertyName);
        }
    }
}