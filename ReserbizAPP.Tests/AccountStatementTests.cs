using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.Tests.Comparers;

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

        [TestCase(50, 50)]
        [TestCase(80, 80)]
        [TestCase(100.50f, 100.50f)]
        public void Test_ConvertPenaltyValue_WhenValueTypeIsFixed(float penaltyValue, double expectedPenaltyValue)
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
                PenaltyValue = penaltyValue,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act 
            var result = testAccountStatement.PenaltyAmountValue;

            // Assert
            var expectedConvertedValue = expectedPenaltyValue;
            Assert.AreEqual(expectedConvertedValue, result);
        }

        [TestCase(9000, 1, 90)]
        [TestCase(7500, 5, 375)]
        [TestCase(300.50f, 10, 30.05f)]
        public void Test_ConvertPenaltyValue_WhenValueTypeIsPercentage(float termRate, float penaltyValue, float expectedPenaltyValue)
        {
            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = termRate,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ElectricBill = 127,
                WaterBill = 280,
                PenaltyValue = penaltyValue,
                PenaltyValueType = ValueTypeEnum.Percentage,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                IsActive = true
            };

            // Act 
            var result = testAccountStatement.PenaltyAmountValue;

            // Assert
            var expectedConvertedValue = expectedPenaltyValue;
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

        [Test]
        public void Test_GetFilteredAccountStatements_WhenFilterPaymentStatusIsPaid()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                PaymentStatus = PaymentStatusEnum.Paid
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 1
                },
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 9
                },
                new AccountStatement {
                    Id = 10
                },
                new AccountStatement {
                    Id = 12
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredAccountStatements_WhenFilterPaymentStatusIsUnpaid()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                PaymentStatus = PaymentStatusEnum.Unpaid
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 2
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 8
                },
                new AccountStatement {
                    Id = 11
                }
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [Test]
        public void Test_GetFilteredAccountStatements_WhenFilterPaymentStatusIsAll()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                PaymentStatus = PaymentStatusEnum.All
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 1
                },
                new AccountStatement {
                    Id = 2
                },
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 8
                },
                new AccountStatement {
                    Id = 9
                },
                new AccountStatement {
                    Id = 10
                },
                new AccountStatement {
                    Id = 11
                },
                new AccountStatement {
                    Id = 12
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [TestCase("2020-01-01")]
        [TestCase("2020-01-15")]
        public void Test_GetFilteredAccountStatements_WhenFilterFromDateIsAvailable(DateTime fromDate)
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                FromDate = fromDate
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 8
                },
                new AccountStatement {
                    Id = 9
                },
                new AccountStatement {
                    Id = 10
                },
                new AccountStatement {
                    Id = 11
                },
                new AccountStatement {
                    Id = 12
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [TestCase("2020-01-15")]
        [TestCase("2020-01-30")]
        public void Test_GetFilteredAccountStatements_WhenFilterToDateIsAvailable(DateTime toDate)
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                ToDate = toDate
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 1
                },
                new AccountStatement {
                    Id = 2
                },
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 4
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [TestCase("2019-12-01", "2020-05-30")]
        [TestCase("2019-12-15", "2020-05-15")]
        public void Test_GetFilteredAccountStatements_WhenFilterFromDateAndToDateAreAvailable(DateTime fromDate, DateTime toDate)
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                FromDate = fromDate,
                ToDate = toDate
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 8
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [Test]
        public void Test_GetFilteredAccountStatements_WhenFilterSortOrderAscendingIsAvailable()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                SortOrder = SortOrderEnum.Ascending
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 1
                },
                new AccountStatement {
                    Id = 2
                },
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 8
                },
                new AccountStatement {
                    Id = 9
                },
                new AccountStatement {
                    Id = 10
                },
                new AccountStatement {
                    Id = 11
                },
                new AccountStatement {
                    Id = 12
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [Test]
        public void Test_GetFilteredAccountStatements_WhenFilterSortOrderDescendingIsAvailable()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();
            var accountStatementList = GetTestContractObjectWithAccountStatements();
            var comparer = new AccountStatementComparer();
            var accountStatementFilter = new AccountStatementFilter
            {
                SortOrder = SortOrderEnum.Descending
            };

            // Act 
            var actualResult = accountStatementRepository.GetFilteredAccountStatements(accountStatementList, accountStatementFilter);

            // Assert
            var expectedResult = new List<AccountStatement>{
                new AccountStatement {
                    Id = 12
                },
                new AccountStatement {
                    Id = 11
                },
                new AccountStatement {
                    Id = 10
                },
                new AccountStatement {
                    Id = 9
                },
                new AccountStatement {
                    Id = 8
                },
                new AccountStatement {
                    Id = 7
                },
                new AccountStatement {
                    Id = 6
                },
                new AccountStatement {
                    Id = 5
                },
                new AccountStatement {
                    Id = 4
                },
                new AccountStatement {
                    Id = 3
                },
                new AccountStatement {
                    Id = 2
                },
                new AccountStatement {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(expectedResult, actualResult, comparer);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsNotFirst()
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsFirst()
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsNotFirst()
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
                Amount = 4000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsFirst()
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

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsNotFirst()
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
                Amount = 4001,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmountAndTheAccountStatementIsFirst()
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

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

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
                Amount = 4500,
                ReceivedById = 700
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3500,
                ReceivedById = 700
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

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
                Amount = 4501,
                ReceivedById = 700
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithRentalFee()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3501,
                ReceivedById = 700
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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnFalse_WhenCurrentPaidAmountIsLessThanTheAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

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
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

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
                Amount = 4000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsEqualWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsNotFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

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
                Amount = 4001,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Should_IsRentalFeeFullyPaidReturnTrue_WhenCurrentPaidAmountIsGreaterThanWithAccountStatementTotalRentalFeeAmount_And_TheAccountStatementIsFirst_And_MiscellaneousDueDateSameWithUtilityDuedate()
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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700
            });

            // Act 
            var result = testAccountStatement.IsRentalFeeFullyPaid;

            // Assert
            Assert.IsTrue(result);
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

        private List<AccountStatement> GetTestContractObjectWithAccountStatements()
        {
            var contract = new Contract
            {
                Id = 1,
                Code = "G1-Code-1001",
                TenantId = 1,
                TermId = 1,
                EffectiveDate = new DateTime(2019, 09, 15),
                IsOpenContract = false,
                DurationValue = 1,
                DurationUnit = DurationEnum.Year,
                AccountStatements = new List<AccountStatement>
                    {
                        // PAID
                        new AccountStatement
                        {
                            Id = 1,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 0,
                            WaterBill = 0,
                            Rate = 9000,
                            DueDate = new DateTime(2019, 10, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 27000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1150
                                }
                            }
                        },
                        // NOT PAID
                        new AccountStatement
                        {
                            Id = 2,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 300,
                            WaterBill = 250,
                            Rate = 9000,
                            DueDate = new DateTime(2019, 11, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 10000
                                }
                            }
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 3,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 300,
                            WaterBill = 250,
                            Rate = 9000,
                            DueDate = new DateTime(2019, 12, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 10700
                                }
                            }
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 4,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 335,
                            WaterBill = 230,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 01, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 10800
                                }
                            }
                        },
                        // NOT PAID
                        new AccountStatement
                        {
                            Id = 5,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 500,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 02, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PenaltyBreakdowns = new List<PenaltyBreakdown> {
                                new PenaltyBreakdown {
                                    Amount = 50
                                },
                                new PenaltyBreakdown {
                                    Amount = 50,
                                },
                                new PenaltyBreakdown {
                                    Amount = 50,
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1000
                                }
                            },
                        },
                        // NOT PAID
                        new AccountStatement
                        {
                            Id = 6,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 550,
                            WaterBill = 350,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 03, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PenaltyBreakdowns = new List<PenaltyBreakdown> {
                                new PenaltyBreakdown {
                                    Amount = 50
                                },
                                new PenaltyBreakdown {
                                    Amount = 50,
                                },
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1000
                                }
                            },
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 7,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 450,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 04, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1000
                                }
                            },
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 8,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 420,
                            WaterBill = 280,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 05, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                            },
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 9,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 450,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 06, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1900
                                },
                            },
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 10,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 450,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 07, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1900
                                },
                            },
                        },
                         // NOT PAID
                        new AccountStatement
                        {
                            Id = 11,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 450,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 08, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                            },
                        },
                        // PAID
                        new AccountStatement
                        {
                            Id = 12,
                            DurationUnit = DurationEnum.Month,
                            AdvancedPaymentDurationValue = 1,
                            DepositPaymentDurationValue = 2,
                            ElectricBill = 450,
                            WaterBill = 300,
                            Rate = 9000,
                            DueDate = new DateTime(2020, 09, 15),
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Amount = 500
                                },
                                new AccountStatementMiscellaneous {
                                    Amount = 650
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown> {
                                new PaymentBreakdown
                                {
                                    Amount = 6000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 3000
                                },
                                new PaymentBreakdown
                                {
                                    Amount = 1900
                                },
                            },
                        },
                    }
            };

            var accountStatements = new List<AccountStatement> {
                // PAID
                new AccountStatement
                {
                    Id = 1,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 0,
                    WaterBill = 0,
                    Rate = 9000,
                    DueDate = new DateTime(2019, 10, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 27000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1150
                        }
                    }
                },
                // NOT PAID
                new AccountStatement
                {
                    Id = 2,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 300,
                    WaterBill = 250,
                    Rate = 9000,
                    DueDate = new DateTime(2019, 11, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 10000
                        }
                    }
                },
                // PAID
                new AccountStatement
                {
                    Id = 3,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 300,
                    WaterBill = 250,
                    Rate = 9000,
                    DueDate = new DateTime(2019, 12, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 10700
                        }
                    }
                },
                // PAID
                new AccountStatement
                {
                    Id = 4,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 335,
                    WaterBill = 230,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 01, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 10800
                        }
                    }
                },
                // NOT PAID
                new AccountStatement
                {
                    Id = 5,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 500,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 02, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PenaltyBreakdowns = new List<PenaltyBreakdown> {
                        new PenaltyBreakdown {
                            Amount = 50
                        },
                        new PenaltyBreakdown {
                            Amount = 50,
                        },
                        new PenaltyBreakdown {
                            Amount = 50,
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1000
                        }
                    },
                },
                // NOT PAID
                new AccountStatement
                {
                    Id = 6,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 550,
                    WaterBill = 350,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 03, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PenaltyBreakdowns = new List<PenaltyBreakdown> {
                        new PenaltyBreakdown {
                            Amount = 50
                        },
                        new PenaltyBreakdown {
                            Amount = 50,
                        },
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1000
                        }
                    },
                },
                // PAID
                new AccountStatement
                {
                    Id = 7,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 450,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 04, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1000
                        }
                    },
                },
                // NOT PAID
                new AccountStatement
                {
                    Id = 8,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 420,
                    WaterBill = 280,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 05, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                    },
                },
                // PAID
                new AccountStatement
                {
                    Id = 9,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 450,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 06, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1900
                        },
                    },
                },
                // PAID
                new AccountStatement
                {
                    Id = 10,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 450,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 07, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1900
                        },
                    },
                },
                // NOT PAID
                new AccountStatement
                {
                    Id = 11,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 450,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 08, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                    },
                },
                // PAID
                new AccountStatement
                {
                    Id = 12,
                    Contract = contract,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ElectricBill = 450,
                    WaterBill = 300,
                    Rate = 9000,
                    DueDate = new DateTime(2020, 09, 15),
                    AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                        new AccountStatementMiscellaneous {
                            Amount = 500
                        },
                        new AccountStatementMiscellaneous {
                            Amount = 650
                        }
                    },
                    PaymentBreakdowns = new List<PaymentBreakdown> {
                        new PaymentBreakdown
                        {
                            Amount = 6000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 3000
                        },
                        new PaymentBreakdown
                        {
                            Amount = 1900
                        },
                    },
                },
            };

            return accountStatements;
        }
    }
}