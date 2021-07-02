using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class AccountStatementRepositoryTests
    {

        [Test]
        public void Test_CalculateTotalAmountPaid_WhenPaymentBreakdownIsEmpty()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            var accountStatementRepository = new AccountStatementRepository();

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

            // Act 
            var result = accountStatementRepository.CalculateTotalAmountPaid(testAccountStatement.PaymentBreakdowns);

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Test_CalculateTotalAmountPaid_WhenPaymentBreakdownIsNotEmpty()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                IsAmountFromDeposit = true
            });

            // Act 
            var result = accountStatementRepository.CalculateTotalAmountPaid(testAccountStatement.PaymentBreakdowns);

            // Assert
            Assert.AreEqual(result, 21000);
        }

        [Test]
        public void Test_CalculateTotalAmountPaidUsingDeposit_WhenPaymentBreakdownIsEmpty()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();

            // Act 
            var result = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Test_CalculateTotalAmountPaidUsingDeposit_WhenPaymentBreakdownIsNotEmpty()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                IsAmountFromDeposit = false
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                IsAmountFromDeposit = true
            });

            // Act 
            var result = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);

            // Assert
            Assert.AreEqual(result, 6001);
        }

        [Test]
        public void Test_CalculatedSuggestedAmountForRentalPayment_WhenThereIsNoDepositAmount_And_PaymentBreakdownIsEmpty()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

            // Arrange - Initialize test account statement
            var testAccountStatement = new AccountStatement
            {
                Id = 1,
                ContractId = 1,
                DueDate = new DateTime(2019, 11, 15),
                Rate = 9000,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 0,
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

            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount;

            // Act 
            var result = accountStatementRepository.CalculatedSuggestedAmountForRentalPayment(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Test_CalculatedSuggestedAmountForRentalPayment_WhenRemainingUnusedDepositedAmountIsEqualToRemainingUnpaidRentalAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculatedSuggestedAmountForRentalPayment(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 6000);
        }

        [Test]
        public void Test_CalculatedSuggestedAmountForRentalPayment_WhenRemainingUnusedDepositedAmountIsGreaterThanTheRemainingUnpaidRentalAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });

            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount;


            // Act 
            var result = accountStatementRepository.CalculatedSuggestedAmountForRentalPayment(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 6000);
        }

        [Test]
        public void Test_CalculatedSuggestedAmountForRentalPayment_WhenRemainingUnusedDepositedAmountIsLessThanToRemainingUnpaidRentalAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculatedSuggestedAmountForRentalPayment(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 6000);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForElectricBill_WhenRemainingUnusedDepositedAmountIsEqualToRemainingUnpaidElectricBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 7500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForElectricBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1500);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForElectricBill_WhenRemainingUnusedDepositedAmountIsGreaterThanTheRemainingUnpaidElectricBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3001,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForElectricBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1500);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForElectricBill_WhenRemainingUnusedDepositedAmountIsLessThanTheRemainingUnpaidElectricBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForElectricBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1000);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForWaterBill_WhenRemainingUnusedDepositedAmountIsEqualToRemainingUnpaidWaterBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.WaterBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForWaterBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1000);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForWaterBill_WhenRemainingUnusedDepositedAmountIsGreaterThanRemainingUnpaidWaterBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 7000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForWaterBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1200);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForWaterBill_WhenRemainingUnusedDepositedAmountIsLessThanRemainingUnpaidWaterBillAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
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
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForWaterBill(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 500);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForMiscellaneousFees_WhenRemainingUnusedDepositedAmountIsEqualToTheRemainingUnpaidMiscellaneousFees()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 7500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.MiscellaneousFee
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForMiscellaneousFees(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1500);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForMiscellaneousFees_WhenRemainingUnusedDepositedAmountIsGreaterThanTheRemainingUnpaidMiscellaneousFees()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 5000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForMiscellaneousFees(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 2000);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForMiscellaneousFees_WhenRemainingUnusedDepositedAmountIsLessThanTheRemainingUnpaidMiscellaneousFees()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForMiscellaneousFees(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 1000);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForPenaltyAmount_WhenRemainingUnusedDepositedAmountIsEqualToTheRemainingUnpaidPenaltyAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });

            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8600,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForPenaltyAmount(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 400);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForPenaltyAmount_WhenRemainingUnusedDepositedAmountIsGreaterThanTheRemainingUnpaidPenaltyAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });

            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForPenaltyAmount(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 400);
        }

        [Test]
        public void Test_CalculateSuggestedAmountForPenaltyAmount_WhenRemainingUnusedDepositedAmountIsLessThanTheRemainingUnpaidPenaltyAmount()
        {
            // Arrange
            var accountStatementRepository = new AccountStatementRepository();

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
                ElectricBill = 1500,
                WaterBill = 1200,
                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithUtilityBillDueDate,
                IsActive = true
            };

            testAccountStatement.AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>();

            testAccountStatement.PenaltyBreakdowns = new List<PenaltyBreakdown>();
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });
            testAccountStatement.PenaltyBreakdowns.Add(new PenaltyBreakdown
            {
                Amount = 100
            });

            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 1",
                Amount = 500
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 2",
                Amount = 1000
            });
            testAccountStatement.AccountStatementMiscellaneous.Add(new AccountStatementMiscellaneous
            {
                Name = "Miscellaneous 3",
                Amount = 500
            });

            testAccountStatement.PaymentBreakdowns = new List<PaymentBreakdown>();
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 1,
                AccountStatementId = 2,
                Amount = 9000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 2,
                AccountStatementId = 2,
                Amount = 8700,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental,
                IsAmountFromDeposit = true
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 500,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 3000,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.Rental
            });
            testAccountStatement.PaymentBreakdowns.Add(new PaymentBreakdown
            {
                Id = 3,
                AccountStatementId = 2,
                Amount = 200,
                ReceivedById = 700,
                PaymentForType = PaymentForTypeEnum.ElectricBill
            });

            var paymentFromDeposit = accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(testAccountStatement.PaymentBreakdowns);
            var depositedAmountBalance = testAccountStatement.CalculatedDepositAmount - paymentFromDeposit;


            // Act 
            var result = accountStatementRepository.CalculateSuggestedAmountForPenaltyAmount(testAccountStatement, depositedAmountBalance);

            // Assert
            Assert.AreEqual(result, 300);
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