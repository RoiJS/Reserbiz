using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class AccountStatementTests
    {
        [Test]
        public void Should_IsFirstAccountStatementReturnTrue_WhenContractContainsSingleAccountStatement()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.IsFirstAccountStatement;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsFirstAccountStatementReturnTrue_WhenContractContainsMultipleAccountStatements_And_AccountStatementIsAtTheFirstOfTheList()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.IsFirstAccountStatement;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsFirstAccountStatementReturnFalse_WhenContractContainsMultipleAccountStatements_And_AccountStatementIsAtTheMiddleOfTheList()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.IsFirstAccountStatement;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsFirstAccountStatementReturnFalse_WhenContractContainsMultipleAccountStatements_And_AccountStatementIsAtTheLastOfTheList()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.IsFirstAccountStatement;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_GetContractActiveAccountStatements_WhenOriginalAccountStatementListContainsAllActiveAccountStatements()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.Contract.AccountStatements.Where(a => a.IsActive).ToList().Count;

            // Assert
            var expectedActiveAccountStatements = 3;
            Assert.AreEqual(expectedActiveAccountStatements, result);
        }

        [Test]
        public void Test_GetContractActiveAccountStatements_WhenOriginalAccountStatementListContainsAllInActiveAccountStatements()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = false
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = false
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = false
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.Contract.AccountStatements.Where(a => a.IsActive).ToList().Count; ;

            // Assert
            var expectedActiveAccountStatements = 0;
            Assert.AreEqual(expectedActiveAccountStatements, result);
        }

        [Test]
        public void Test_GetContractActiveAccountStatements_WhenOriginalAccountStatementListContainsInActiveAndActiveAccountStatements()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = false
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = false
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };


            // Act 
            var result = testAccountStatement.Contract.AccountStatements.Where(a => a.IsActive).ToList().Count;

            // Assert
            var expectedActiveAccountStatements = 1;
            Assert.AreEqual(expectedActiveAccountStatements, result);
        }

        [Test]
        public void Test_GetPenaltyNextDueDate_WhenAccountStatementContainsEmptyPenaltyBreakdowns()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();

            // Act 
            var result = testAccountStatement.PenaltyNextDueDate;

            // Assert
            var expectedPenaltyNextDueDate = new DateTime(2019, 11, 18);
            Assert.AreEqual(expectedPenaltyNextDueDate, result);
        }

        [Test]
        public void Test_GetPenaltyNextDueDate_WhenAccountStatementContainsMultiplePenaltyBreakdowns()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            // Act 
            var result = testAccountStatement.PenaltyNextDueDate;

            // Assert
            var expectedPenaltyNextDueDate = new DateTime(2019, 11, 20);
            Assert.AreEqual(expectedPenaltyNextDueDate, result);
        }

        [TestCase("12-20-2019")]
        [TestCase("12-21-2019")]
        public void Should_IsDueToGeneratePenaltyReturnTrue_WhenCurrentDateIsGreaterThanOrEqualToPenaltyNextDueDate_And_PenaltyNextDueDateIsLessThanAccountStatementNextDueDate(DateTime currentDateTime)
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.SetCurrentDateTime(currentDateTime);

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 12, 12),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 12, 13),
                Amount = 50
            });

            // Act 
            var result = testAccountStatement.IsDueToGeneratePenalty;

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("12-20-2019")]
        [TestCase("12-21-2019")]
        public void Should_IsDueToGeneratePenaltyReturnFalse_WhenCurrentDateIsGreaterThanOrEqualToPenaltyNextDueDate_And_PenaltyNextDueDateIsNotLessThanAccountStatementNextDueDate(DateTime currentDateTime)
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.SetCurrentDateTime(currentDateTime);

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 12, 13),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 12, 14),
                Amount = 50
            });

            // Act 
            var result = testAccountStatement.IsDueToGeneratePenalty;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test_ConvertPenaltyValue_WhenValueTypeIsFixed()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act 
            var result = testAccountStatement.PenaltyAmountValue;

            // Assert
            var expectedConvertedValue = 50;
            Assert.AreEqual(expectedConvertedValue, result);
        }

        [Test]
        public void Test_ConvertPenaltyValue_WhenValueTypeIsPercentage()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act 
            var result = testAccountStatement.PenaltyAmountValue;

            // Assert
            var expectedConvertedValue = 90;
            Assert.AreEqual(expectedConvertedValue, result);
        }

        [Test]
        public void Test_CalculatePenaltyTotalAmount_WhenAccountStatementContainsEmptyPenaltyBreakdowns()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();

            // Act 
            var result = testAccountStatement.PenaltyTotalAmount;

            // Assert
            var expectedPenaltyTotalAmount = 0;
            Assert.AreEqual(expectedPenaltyTotalAmount, result);
        }

        [Test]
        public void Test_CalculatePenaltyTotalAmount_WhenAccountStatementContainsPenaltyBreakdowns()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            // Act 
            var result = testAccountStatement.PenaltyTotalAmount;

            // Assert
            var expectedPenaltyTotalAmount = 100;
            Assert.AreEqual(expectedPenaltyTotalAmount, result);
        }

        [Test]
        public void Test_CalculateMiscellaneousTotalAmount_WhenAccountStatementContainsEmptyMiscellaneous()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();

            // Act 
            var result = testAccountStatement.MiscellaneousTotalAmount;

            // Assert
            var expectedMiscellaneousTotalAmount = 0;
            Assert.AreEqual(expectedMiscellaneousTotalAmount, result);
        }

        [Test]
        public void Test_CalculateMiscellaneousTotalAmount_WhenAccountStatementContainsMiscellaneous()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            // Act 
            var result = testAccountStatement.MiscellaneousTotalAmount;

            // Assert
            var expectedMiscellaneousTotalAmount = 500;
            Assert.AreEqual(expectedMiscellaneousTotalAmount, result);
        }

        [Test]
        public void Test_CalculateCurrentAmountPaid_WhenAccountStatementContainsEmptyPaymentBreakdown()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();

            // Act 
            var result = testAccountStatement.CurrentAmountPaid;

            // Assert
            var expectedCurrentAmountPaid = 0;
            Assert.AreEqual(expectedCurrentAmountPaid, result);
        }

        [Test]
        public void Test_CalculateCurrentAmountPaid_WhenAccountStatementContainsPaymentBreakdown()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 1,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                ReceivedById = 1,
                Amount = 7800
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                ReceivedById = 1,
                Amount = 3200
            });

            // Act 
            var result = testAccountStatement.CurrentAmountPaid;

            // Assert
            var expectedCurrentAmountPaid = 11000;
            Assert.AreEqual(expectedCurrentAmountPaid, result);
        }

        [Test]
        public void Test_CalculateAccountStatementTotalAmount_WhenAccountStatementIsFirstOnTheListOfContractAccountStatementsWithNoPenaltyBreakdownAndNoMiscellaneous()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            // Act 
            var result = testAccountStatement.AccountStatementTotalAmount;

            // Assert
            var expectedAccountStatementTotalAmount = 27407;
            Assert.AreEqual(expectedAccountStatementTotalAmount, result);
        }

        [Test]
        public void Test_CalculateAccountStatementTotalAmount_WhenAccountStatementIsFirstOnTheListOfContractAccountStatementsWithPenaltyBreakdownAndMiscellaneous()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            // Act 
            var result = testAccountStatement.AccountStatementTotalAmount;

            // Assert
            var expectedAccountStatementTotalAmount = 28007;
            Assert.AreEqual(expectedAccountStatementTotalAmount, result);
        }

        [Test]
        public void Test_CalculateAccountStatementTotalAmount_WhenAccountStatementIsNotFirstOnTheListOfContractAccountStatementsWithNoPenaltyBreakdownAndMiscellaneous()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();

            // Act 
            var result = testAccountStatement.AccountStatementTotalAmount;

            // Assert
            var expectedAccountStatementTotalAmount = 9407;
            Assert.AreEqual(expectedAccountStatementTotalAmount, result);
        }

        [Test]
        public void Test_CalculateAccountStatementTotalAmount_WhenAccountStatementIsNotFirstOnTheListOfContractAccountStatementsWithPenaltyBreakdownAndMiscellaneous()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            // Act 
            var result = testAccountStatement.AccountStatementTotalAmount;

            // Assert
            var expectedAccountStatementTotalAmount = 10007;
            Assert.AreEqual(expectedAccountStatementTotalAmount, result);
        }

        [Test]
        public void Test_CurrentBalance_WhenAccountStatementContainsEmptyPaymentBreakdown()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();

            // Act 
            var result = testAccountStatement.CurrentBalance;

            // Assert
            var expectedCurrentBalance = 10007;
            Assert.AreEqual(expectedCurrentBalance, result);
        }

        [Test]
        public void Test_CurrentBalance_WhenAccountStatementContainsWithPaymentBreakdownButNotFullyPaid()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.CurrentBalance;

            // Assert
            var expectedCurrentBalance = 2007;
            Assert.AreEqual(expectedCurrentBalance, result);
        }

        [Test]
        public void Test_CurrentBalance_WhenAccountStatementContainsWithPaymentBreakdownAndFullyPaid()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 2007,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.CurrentBalance;

            // Assert
            var expectedCurrentBalance = 0;
            Assert.AreEqual(expectedCurrentBalance, result);
        }

        [Test]
        public void Test_CurrentBalance_WhenAccountStatementContainsWithPaymentBreakdownAndMoreThanFullyPaid()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 2007,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 150,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.CurrentBalance;

            // Assert
            var expectedCurrentBalance = -150;
            Assert.AreEqual(expectedCurrentBalance, result);
        }

        [Test]
        public void Should_IsFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalAmount()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualToTheAccountStatementTotalAmount()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 2007,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanTheAccountStatementTotalAmount()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(new DateTime(2019, 12, 31));
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 1,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            contractObject.AccountStatements.Add(new AccountStatement
            {
                Id = 3,
                ContractId = 1,
                DueDate = new DateTime(2019, 12, 15),
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            });

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                Contract = contractObject,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 1,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 18),
                Amount = 50
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Id = 2,
                AccountStatementId = 1,
                DueDate = new DateTime(2019, 11, 19),
                Amount = 50
            });

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 1,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 150
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Id = 2,
                AccountStatementId = 1,
                Name = "Utility",
                Amount = 350
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsPenaltySettingActiveReturnTrue_WhenPenaltyValueIsGreaterThanZero()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act
            var result = testAccountStatement.IsPenaltySettingActive;

            // Assert 
            Assert.IsTrue(result);
        }
       
        [Test]
        public void Should_IsPenaltySettingActiveReturnFalse_WhenPenaltyValueIsNotGreaterThanZero()
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 2,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = 0,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act
            var result = testAccountStatement.IsPenaltySettingActive;

            // Assert 
            Assert.IsFalse(result);
        }

        private Contract GetTestContractObject()
        {
            var contract = new Contract
            {
                Id = 1,
                Code = "C-0001",
                TenantId = 1,
                TermId = 4,
                EffectiveDate = new DateTime(2019, 09, 15),
                IsOpenContract = false
            };

            return contract;
        }
    }
}