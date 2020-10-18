using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Custom_Validations;

namespace ReserbizAPP.Tests
{
    public class BillAmountAttributeTests
    {
        [Test]
        public void Should_ReturnNull_When_ExcludeElectricBillIsSetToFalse_And_ElectricBillAmountIsSetToZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeElectricBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeElectricBill");
            var electricBillAmount = 0;

            // Act
            var result = billAmountAttribute.GetValidationResult(electricBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Should_ReturnNull_When_ExcludeElectricBillIsSetToTrue_And_ElectricBillAmountIsSetToZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeElectricBill = true };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeElectricBill");
            var electricBillAmount = 0;

            // Act
            var result = billAmountAttribute.GetValidationResult(electricBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Should_ReturnNull_When_ExcludeElectricBillIsSetToFalse_And_ElectricBillAmountIsGreaterThanZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeElectricBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeElectricBill");
            var electricBillAmount = 100;
            var expectedResult = "The field ExcludeElectricBill is invalid.";
            // Act
            var result = billAmountAttribute.GetValidationResult(electricBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.AreEqual(expectedResult, result?.ErrorMessage);
        }

        [Test]
        public void Should_ReturnErrorMessage_When_ExcludeElectricBillIsSetToFalse_And_ElectricBillAmountIsGreaterThanZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeElectricBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeElectricBill");
            var electricBillAmount = 100;
            var expectedResult = "The field ExcludeElectricBill is invalid.";
            // Act
            var result = billAmountAttribute.GetValidationResult(electricBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.AreEqual(expectedResult, result?.ErrorMessage);
        }

        [Test]
        public void Should_ReturnNull_When_ExcludeWaterBillIsSetToFalse_And_ElectricBillAmountIsSetToZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeWaterBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeWaterBill");
            var waterBillAmount = 0;

            // Act
            var result = billAmountAttribute.GetValidationResult(waterBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Should_ReturnNull_When_ExcludeWaterBillIsSetToTrue_And_ElectricBillAmountIsSetToZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeWaterBill = true };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeWaterBill");
            var waterBillAmount = 0;

            // Act
            var result = billAmountAttribute.GetValidationResult(waterBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Should_ReturnNull_When_ExcludeWaterBillIsSetToFalse_And_ElectricBillAmountIsGreaterThanZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeWaterBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeWaterBill");
            var waterBillAmount = 100;
            var expectedResult = "The field ExcludeWaterBill is invalid.";
            // Act
            var result = billAmountAttribute.GetValidationResult(waterBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.AreEqual(expectedResult, result?.ErrorMessage);
        }

        [Test]
        public void Should_ReturnErrorMessage_When_ExcludeWaterBillIsSetToFalse_And_ElectricBillAmountIsGreaterThanZero()
        {
            // Arrange
            var termObject = new TermForManageDto { ExcludeWaterBill = false };
            var billAmountAttribute = GetBillAmountStateAttribute("ExcludeWaterBill");
            var waterBillAmount = 100;
            var expectedResult = "The field ExcludeWaterBill is invalid.";
            // Act
            var result = billAmountAttribute.GetValidationResult(waterBillAmount, new ValidationContext(termObject));

            // Assert
            Assert.AreEqual(expectedResult, result?.ErrorMessage);
        }

        private BillAmountState GetBillAmountStateAttribute(string propertyName)
        {
            return new BillAmountState(propertyName);
        }
    }
}