using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Custom_Validations;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class OpenContractTests
    {

        [Test]
        public void Should_ReturnNull_WhenOpenContractIsTrue_And_DurationUnitIsNone_And_DurationValueIsZero()
        {
            //Arrange 
            var durationRangeValueAttribute = new OpenContract("DurationUnit", "DurationValue");
            var contractObj = new ContractManageDto
            {
                DurationUnit = DurationEnum.None,
                DurationValue = 0
            };
            var openContractValue = true;

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(openContractValue, new ValidationContext(contractObj));

            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public void Should_ReturnNull_WhenOpenContractIsFalse_And_DurationUnitIsNone_And_DurationValueIsZero()
        {
            //Arrange 
            var durationRangeValueAttribute = new OpenContract("DurationUnit", "DurationValue");
            var contractObj = new ContractManageDto
            {
                DurationUnit = DurationEnum.None,
                DurationValue = 0
            };
            var openContractValue = false;

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(openContractValue, new ValidationContext(contractObj));

            // Assert
            Assert.IsNull(result);
        }
        
        [TestCase(DurationEnum.Day)]
        [TestCase(DurationEnum.Week)]
        [TestCase(DurationEnum.Month)]
        [TestCase(DurationEnum.Quarter)]
        [TestCase(DurationEnum.Year)]
        public void Should_ReturnNotNull_WhenOpenContractIsTrue_And_DurationUnitIsNotNone_Or_DurationValueIsZero(DurationEnum durationUnit)
        {
            //Arrange 
            var durationRangeValueAttribute = new OpenContract("DurationUnit", "DurationValue");
            var contractObj = new ContractManageDto
            {
                DurationUnit = durationUnit,
                DurationValue = 0
            };
            var openContractValue = true;

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(openContractValue, new ValidationContext(contractObj));

            // Assert
            Assert.IsNotNull(result);
        }
        
        [TestCase(-100)]
        [TestCase(-50)]
        [TestCase(-10)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        public void Should_ReturnNotNull_WhenOpenContractIsTrue_And_DurationUnitIsNone_Or_DurationValueIsNotZero(int value)
        {
            //Arrange 
            var durationRangeValueAttribute = new OpenContract("DurationUnit", "DurationValue");
            var contractObj = new ContractManageDto
            {
                DurationUnit = DurationEnum.None,
                DurationValue = value
            };
            var openContractValue = true;

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(openContractValue, new ValidationContext(contractObj));

            // Assert
            Assert.IsNotNull(result);
        }
        
        // Test cased for day duration unit
        [TestCase(DurationEnum.Day, -1)]
        [TestCase(DurationEnum.Day, -10)]
        [TestCase(DurationEnum.Day, -50)]
        [TestCase(DurationEnum.Day, -100)]
        [TestCase(DurationEnum.Day, 1)]
        [TestCase(DurationEnum.Day, 10)]
        [TestCase(DurationEnum.Day, 50)]
        [TestCase(DurationEnum.Day, 100)]

        // Test cased for week duration unit
        [TestCase(DurationEnum.Week, -1)]
        [TestCase(DurationEnum.Week, -10)]
        [TestCase(DurationEnum.Week, -30)]
        [TestCase(DurationEnum.Week, -50)]
        [TestCase(DurationEnum.Week, 1)]
        [TestCase(DurationEnum.Week, 10)]
        [TestCase(DurationEnum.Week, 30)]
        [TestCase(DurationEnum.Week, 50)]

        // Test cased for month duration unit
        [TestCase(DurationEnum.Month, -1)]
        [TestCase(DurationEnum.Month, -5)]
        [TestCase(DurationEnum.Month, -12)]
        [TestCase(DurationEnum.Month, 1)]
        [TestCase(DurationEnum.Month, 5)]
        [TestCase(DurationEnum.Month, 12)]

        // Test cased for quarter duration unit
        [TestCase(DurationEnum.Quarter, -1)]
        [TestCase(DurationEnum.Quarter, -2)]
        [TestCase(DurationEnum.Quarter, -3)]
        [TestCase(DurationEnum.Quarter, -4)]
        [TestCase(DurationEnum.Quarter, 1)]
        [TestCase(DurationEnum.Quarter, 2)]
        [TestCase(DurationEnum.Quarter, 3)]
        [TestCase(DurationEnum.Quarter, 4)]

        // Test cased for year duration unit
        [TestCase(DurationEnum.Year, -1)]
        [TestCase(DurationEnum.Year, 1)]

        public void Should_ReturnNotNull_WhenOpenContractIsTrue_And_DurationUnitIsNotNone_And_DurationValueIsNotZero(DurationEnum durationUnit, int value)
        {
            //Arrange 
            var durationRangeValueAttribute = new OpenContract("DurationUnit", "DurationValue");
            var contractObj = new ContractManageDto
            {
                DurationUnit = durationUnit,
                DurationValue = value
            };
            var openContractValue = true;

            // Act
            var result = durationRangeValueAttribute.GetValidationResult(openContractValue, new ValidationContext(contractObj));

            // Assert
            Assert.IsNotNull(result);
        }
    }
}