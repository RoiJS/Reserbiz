using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void Test_CalculateContractExpirationDate_WhenDateDurationUnitIsDay()
        {
            // Arrange
            var contractObject = GetTestContractObject();
            // Set duration unit and value
            contractObject.DurationUnit = DurationEnum.Day;
            contractObject.DurationValue = 10;

            // Act
            var result = contractObject.ExpirationDate;

            // Assert 
            var expectedExpirationDate = new DateTime(2019, 09, 25);
            Assert.AreEqual(expectedExpirationDate, result);
        }

        [Test]
        public void Test_CalculateContractExpirationDate_WhenDateDurationUnitIsWeek()
        {
            // Arrange
            var contractObject = GetTestContractObject();
            // Set duration unit and value
            contractObject.DurationUnit = DurationEnum.Week;
            contractObject.DurationValue = 1;

            // Act
            var result = contractObject.ExpirationDate;

            // Assert 
            var expectedExpirationDate = new DateTime(2019, 09, 22);
            Assert.AreEqual(expectedExpirationDate, result);
        }
        
        [Test]
        public void Test_CalculateContractExpirationDate_WhenDateDurationUnitIsMonth()
        {
            // Arrange
            var contractObject = GetTestContractObject();
            // Set duration unit and value
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.DurationValue = 1;

            // Act
            var result = contractObject.ExpirationDate;

            // Assert 
            var expectedExpirationDate = new DateTime(2019, 10, 15);
            Assert.AreEqual(expectedExpirationDate, result);
        }
        
        [Test]
        public void Test_CalculateContractExpirationDate_WhenDateDurationUnitIsQuarter()
        {
            // Arrange
            var contractObject = GetTestContractObject();
            // Set duration unit and value
            contractObject.DurationUnit = DurationEnum.Quarter;
            contractObject.DurationValue = 1;

            // Act
            var result = contractObject.ExpirationDate;

            // Assert 
            var expectedExpirationDate = new DateTime(2019, 12, 15);
            Assert.AreEqual(expectedExpirationDate, result);
        }
        
        [Test]
        public void Test_CalculateContractExpirationDate_WhenDateDurationUnitIsYear()
        {
            // Arrange
            var contractObject = GetTestContractObject();
            // Set duration unit and value
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.DurationValue = 1;

            // Act
            var result = contractObject.ExpirationDate;

            // Assert 
            var expectedExpirationDate = new DateTime(2020, 09, 15);
            Assert.AreEqual(expectedExpirationDate, result);
        }
        
        
        [Test]
        public void Should_NextDueDateSameWithContractEffectiveDate_WhenContractContainsEmptyAccountStatement()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.AccountStatements = new List<AccountStatement>();
            
            // Act
            var result = contractObject.NextDueDate;

            // Assert 
            var expectedNextDueDate = new DateTime(2019, 09, 15);
            Assert.AreEqual(expectedNextDueDate, result);
        }
        
        
        [Test]
        public void Should_NextDueDateSameWithContractAccountStatementDueDateAddedByDurationValue_WhenContractContainsSingleAccountStatement()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.NextDueDate;

            // Assert 
            var expectedNextDueDate = new DateTime(2019, 11, 15);
            Assert.AreEqual(expectedNextDueDate, result);
        }
        
        [Test]
        public void Should_NextDueDateSameWithContractLastAccountStatementDueDateAddedByDurationValue_WhenContractContainsMultipleAccountStatements()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.NextDueDate;

            // Assert 
            var expectedNextDueDate = new DateTime(2019, 12, 15);
            Assert.AreEqual(expectedNextDueDate, result);
        }
        
        [TestCase("09/15/2020")]
        [TestCase("09/16/2020")]
        public void Should_ContractIsExpiredReturnTrue_WhenCurrentDateIsGreaterThanOrEqualToContractExpirationDate(DateTime currentDateTime)
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value and also the current date time
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            
            // Act
            var result = contractObject.IsExpired;

            // Assert 
            Assert.IsTrue(result);
        }
        
        [TestCase("09/13/2020")]
        [TestCase("09/14/2020")]
        public void Should_ContractIsExpiredReturnFalse_WhenCurrentDateIsNotGreaterThanOrNotEqualToContractExpirationDate(DateTime currentDateTime)
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value and also the current date time
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            
            // Act
            var result = contractObject.IsExpired;

            // Assert 
            Assert.IsFalse(result);
        }
        
        [TestCase("12/15/2019")]
        [TestCase("12/16/2019")]
        public void Should_ContractIsDueReturnTrue_WhenCurrentDateIsGreaterThanOrEqualToContractNextDueDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.SetCurrentDateTime(currentDateTime);

            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.IsDue;

            // Assert 
            Assert.IsTrue(result);
        }
        
        [TestCase("12/13/2019")]
        [TestCase("12/14/2019")]
        public void Should_ContractIsDueReturnFalse_WhenCurrentDateIsNotGreaterThanOrNotEqualToContractNextDueDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.SetCurrentDateTime(currentDateTime);

            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.IsDue;

            // Assert 
            Assert.IsFalse(result);
        }
        
        [TestCase("09/13/2020")]
        [TestCase("09/14/2020")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnTrue_WhenContractIsNotExpired_And_EffectiveDateIsEqualToNextDueDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var  daysBeforeGeneratingAccountStatement = 3;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Act
            var result = contractObject.IsDueForGeneratingAccountStatement(daysBeforeGeneratingAccountStatement);

            // Assert 
            Assert.IsTrue(result);
        }
        
        [TestCase("09/15/2020")]
        [TestCase("09/16/2020")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnFalse_WhenContractIsExpired_And_EffectiveDateIsEqualToNextDueDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var  daysBeforeGeneratingAccountStatement = 3;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Act
            var result = contractObject.IsDueForGeneratingAccountStatement(daysBeforeGeneratingAccountStatement);

            // Assert 
            Assert.IsFalse(result);
        }
        
        [TestCase("12/13/2019")]
        [TestCase("12/14/2019")]
        [TestCase("12/15/2019")]
        [TestCase("12/16/2019")]
        [TestCase("12/17/2019")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnTrue_WhenContractIsNotExpired_And_NextDueDateMinusCurrentDateIsLessThanOrEqualToTheNumberOfDaysBeforeGeneratingAccountStatementSettings(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var  daysBeforeGeneratingAccountStatement = 3;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.IsDueForGeneratingAccountStatement(daysBeforeGeneratingAccountStatement);

            // Assert 
            Assert.IsTrue(result);
        }
        
        [TestCase("12/08/2019")]
        [TestCase("12/09/2019")]
        [TestCase("12/10/2019")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnFalse_WhenContractIsNotExpired_And_NextDueDateMinusCurrentDateIsNotLessThanOrNotEqualToTheNumberOfDaysBeforeGeneratingAccountStatementSettings(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var  daysBeforeGeneratingAccountStatement = 4;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 10, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });
            
            listOfAccountStatements.Add(new AccountStatement {
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            contractObject.AccountStatements.AddRange(listOfAccountStatements);
            
            // Act
            var result = contractObject.IsDueForGeneratingAccountStatement(daysBeforeGeneratingAccountStatement);

            // Assert 
            Assert.IsFalse(result);
        }


        private DateTime GetTestEffectiveDate()
        {
            return new DateTime(2019, 09, 15);
        }

        private Contract GetTestContractObject()
        {
            var contract = new Contract
            {
                Id = 1,
                Code = "C-0001",
                TenantId = 1,
                TermId = 4,
                EffectiveDate = GetTestEffectiveDate(),
                IsOpenContract = false
            };

            return contract;
        }
    }
}