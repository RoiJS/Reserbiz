using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.Tests.Comparers;

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
            contractObject.Term = new Term
            {
                DurationUnit = DurationEnum.Month
            };
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement
            {
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
            contractObject.Term = new Term
            {
                DurationUnit = DurationEnum.Month
            };
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement
            {
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

            listOfAccountStatements.Add(new AccountStatement
            {
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

            // Set up sample account statement
            contractObject.AccountStatements = new List<AccountStatement>();

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

            // Set up sample account statement
            contractObject.AccountStatements = new List<AccountStatement>();

            // Act
            var result = contractObject.IsExpired;

            // Assert 
            Assert.IsFalse(result);
        }

        [Test]
        public void Should_ContractIsExpiredReturnTrue_WhenNextDueDateIsGreaterThanTheExpirationDate()
        {
            // Arrange
            var contractObject = GetTestContractObject();

            // Arrange - Set duration unit and value and also the current date time
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set up sample account statement
            contractObject.AccountStatements = new List<AccountStatement>();
            contractObject.AccountStatements.Add(new AccountStatement
            {
                DurationUnit = DurationEnum.Month,
                DueDate = new DateTime(2020, 08, 17)
            });

            // Act
            var result = contractObject.IsExpired;

            // Assert 
            Assert.IsTrue(result);
        }

        [TestCase("12/15/2019")]
        [TestCase("12/16/2019")]
        public void Should_ContractIsExpiredReturnFalse_WhenNextDueDateIsLessThanTheExpirationDate(DateTime currentDateTime)
        {
            // Arrange
            var contractObject = GetTestContractObject();
            contractObject.SetCurrentDateTime(currentDateTime);

            // Arrange - Set duration unit and value and also the current date time
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.Term = new Term
            {
                DurationUnit = DurationEnum.Month
            };

            // Set up sample account statement
            contractObject.AccountStatements = new List<AccountStatement>();
            contractObject.AccountStatements.Add(new AccountStatement
            {
                DurationUnit = DurationEnum.Month,
                DueDate = new DateTime(2020, 08, 14)
            });

            // Act
            var result = contractObject.IsExpired;

            // Assert 
            Assert.IsFalse(result);
        }

        [TestCase("12/15/2019")]
        [TestCase("12/16/2019")]
        public void Should_ContractIsExpiredReturnFalse_WhenNextDueDateIsEqualToTheExpirationDate(DateTime currentDateTime)
        {
            // Arrange
            var contractObject = GetTestContractObject();
            contractObject.SetCurrentDateTime(currentDateTime);

            // Arrange - Set duration unit and value and also the current date time
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set up sample account statement
            contractObject.AccountStatements = new List<AccountStatement>();
            contractObject.AccountStatements.Add(new AccountStatement
            {
                DurationUnit = DurationEnum.Month,
                DueDate = new DateTime(2020, 08, 15)
            });

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
            listOfAccountStatements.Add(new AccountStatement
            {
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

            listOfAccountStatements.Add(new AccountStatement
            {
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
            listOfAccountStatements.Add(new AccountStatement
            {
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

            listOfAccountStatements.Add(new AccountStatement
            {
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

        [TestCase("10/14/2019")]
        [TestCase("10/15/2019")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnTrue_WhenNextDueDateIsLessThanOrEqualToExpirationDate_And_EffectiveDateIsEqualToNextDueDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var daysBeforeGeneratingAccountStatement = 3;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Month;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Act
            var result = contractObject.IsDueForGeneratingAccountStatement(daysBeforeGeneratingAccountStatement);

            // Assert 
            Assert.IsTrue(result);
        }

        [TestCase("12/13/2019")]
        [TestCase("12/14/2019")]
        [TestCase("12/15/2019")]
        [TestCase("12/16/2019")]
        [TestCase("12/17/2019")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnTrue_WhenNextDueDateIsLessThanOrEqualToExpirationDate_And_NextDueDateMinusCurrentDateIsLessThanOrEqualToTheNumberOfDaysBeforeGeneratingAccountStatementSettings(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();
            var daysBeforeGeneratingAccountStatement = 3;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement
            {
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

            listOfAccountStatements.Add(new AccountStatement
            {
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
        public void Should_ContractIsDueForGeneratingAccountStatementReturnFalse_WhenNextDueDateIsLessThanOrEqualToExpirationDate_And_NextDueDateMinusCurrentDateIsNotLessThanOrNotEqualToTheNumberOfDaysBeforeGeneratingAccountStatementSettings(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();

            // Initialize number of days before considering
            // generating new account statement
            var daysBeforeGeneratingAccountStatement = 4;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement
            {
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

            listOfAccountStatements.Add(new AccountStatement
            {
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

        [TestCase("09/15/2020")]
        [TestCase("09/16/2020")]
        public void Should_ContractIsDueForGeneratingAccountStatementReturnFalse_WhenNextDueDateIsNotLessThanOrNotEqualToExpirationDate(DateTime currentDateTime)
        {
            var contractObject = GetTestContractObject();

            // Initialize number of days before considering
            // generating new account statement
            var daysBeforeGeneratingAccountStatement = 4;

            // Arrange - Set duration unit and value
            contractObject.DurationValue = 1;
            contractObject.DurationUnit = DurationEnum.Year;

            // Set current date time
            contractObject.SetCurrentDateTime(currentDateTime);
            contractObject.AccountStatements = new List<AccountStatement>();

            // Arrange - Create test statement of accounts
            var listOfAccountStatements = new List<AccountStatement>();
            listOfAccountStatements.Add(new AccountStatement
            {
                ContractId = 1,
                DueDate = new DateTime(2020, 07, 16),
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

            listOfAccountStatements.Add(new AccountStatement
            {
                ContractId = 1,
                DueDate = new DateTime(2020, 08, 16),
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

        [TestCase("2019-12-15")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsMoreThanTwoYears(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 3;
            contract.DurationUnit = DurationEnum.Year;
            contract.AccountStatements = new List<AccountStatement>();

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            // Expected is 2 years, 9 months and 1 day
            var expectedResult = new List<ContractDurationBeforeContractEnds>{
                new ContractDurationBeforeContractEnds {
                    DurationValue =  2,
                    DurationUnitText = DurationEnum.Year.ToString()
                },
                new ContractDurationBeforeContractEnds {
                    DurationValue = 9,
                    DurationUnitText = DurationEnum.Month.ToString()
                },
                new ContractDurationBeforeContractEnds {
                    DurationValue = 1,
                    DurationUnitText = DurationEnum.Day.ToString()
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-20")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsMoreThanOneYearButLessThanTwoYears(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 2;
            contract.DurationUnit = DurationEnum.Year;
            contract.AccountStatements = new List<AccountStatement>();

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            var expectedResult = new List<ContractDurationBeforeContractEnds>{
                new ContractDurationBeforeContractEnds {
                    DurationValue =  1,
                    DurationUnitText = DurationEnum.Year.ToString()
                },
                new ContractDurationBeforeContractEnds {
                    DurationValue = 7,
                    DurationUnitText = DurationEnum.Month.ToString()
                },
                new ContractDurationBeforeContractEnds {
                    DurationValue = 26,
                    DurationUnitText = DurationEnum.Day.ToString()
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-09-15")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsEqualToOneYear(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 2;
            contract.DurationUnit = DurationEnum.Year;
            contract.AccountStatements = new List<AccountStatement>();

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            var expectedResult = new List<ContractDurationBeforeContractEnds>{
                new ContractDurationBeforeContractEnds {
                    DurationValue =  1,
                    DurationUnitText = DurationEnum.Year.ToString()
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-03-15")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsMoreThanOneMonthButLessThanOneYear(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 1;
            contract.DurationUnit = DurationEnum.Year;
            contract.AccountStatements = new List<AccountStatement>();

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            var expectedResult = new List<ContractDurationBeforeContractEnds>{
                new ContractDurationBeforeContractEnds {
                    DurationValue = 6,
                    DurationUnitText = DurationEnum.Month.ToString()
                },
                new ContractDurationBeforeContractEnds {
                    DurationValue = 1,
                    DurationUnitText = DurationEnum.Day.ToString()
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2019-10-29")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsLessThanAMonth(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 2;
            contract.DurationUnit = DurationEnum.Month;
            contract.AccountStatements = new List<AccountStatement>();

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            var expectedResult = new List<ContractDurationBeforeContractEnds>{
                new ContractDurationBeforeContractEnds {
                    DurationValue = 17,
                    DurationUnitText = DurationEnum.Day.ToString()
                }
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-09-15")]
        public void Test_ContractDurationValueBeforeContractEnds_WhenRemainingDaysBeforeContractEndsIsEqualToExpirationDate(DateTime currentDateTime)
        {
            // Arrange
            var contract = GetTestContractObject();
            contract.SetCurrentDateTime(currentDateTime);
            contract.DurationValue = 1;
            contract.DurationUnit = DurationEnum.Year;

            var comparer = new ContractDurationBeforeContractEndsCollectionComparer();

            // Act
            var actualResult = contract.ContractDurationBeforeContractEnds;

            // Assert
            var expectedResult = new List<ContractDurationBeforeContractEnds>();

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
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

            contract.Term = new Term
            {
                DurationUnit = DurationEnum.Month
            };

            return contract;
        }
    }
}