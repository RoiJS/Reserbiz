using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.Tests.Comparers;

namespace ReserbizAPP.Tests
{
    [TestFixture]
    public class ContractRepositoryTests
    {
        [TestCase("G1-")]
        public void Test_GetFilteredContracts_WhenFilterCodeIsAvailable(string code)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                Code = code
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase(3)]
        public void Test_GetFilteredContracts_WhenFilterTenantIdIsAvailable(int tenantId)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                TenantId = tenantId
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 4
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-01")]
        public void Test_GetFilteredContracts_WhenFilterActiveFromIsInBetweenTheContractEffectiveAndExpirationDate(DateTime activeFrom)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveFrom = activeFrom
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 9
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2019-12-02")]
        public void Test_GetFilteredContracts_WhenFilterActiveFromIsEqualtoTheContractExpirationDate(DateTime activeFrom)
        {
            /**
            * In this test case, We're trying to test if the current "active from" 
            * filter is equal to the expiration date of some of the test contract 
            * list items which in this case Contract # 2 is the target test item to compare
            * against the current active from filter value.
            **/

            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveFrom = activeFrom
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 9
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-15")]
        public void Test_GetFilteredContracts_WhenFilterActiveFromIsEqualtoTheContractEffectiveDate(DateTime activeFrom)
        {
            /**
            * In this test case, We're trying to test if the current "active from" 
            * filter is equal to the effective date of some of the test contract 
            * list items which in this case Contract # 6 is the target test item to compare
            * against the current active from filter value.
            **/

            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveFrom = activeFrom
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 9
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-01")]
        public void Test_GetFilteredContracts_WhenFilterActiveToIsInBetweenTheContractEffectiveAndExpirationDate(DateTime activeTo)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveTo = activeTo
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-15")]
        public void Test_GetFilteredContracts_WhenFilterActiveToIsEqualtoTheContractEffectiveDate(DateTime activeTo)
        {
            /**
            * In this test case, We're trying to test if the current "active to" 
            * filter is equal to the effective date of some of the test contract 
            * list items which in this case Contract # 6 is the target test item to compare
            * against the current active to filter value.
            **/

            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveTo = activeTo
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2019-12-02")]
        public void Test_GetFilteredContracts_WhenFilterActiveToIsEqualtoTheContractExpirationDate(DateTime activeTo)
        {
            /**
            * In this test case, We're trying to test if the current "active to" 
            * filter is equal to the expiration date of some of the test contract 
            * list items which in this case Contract # 2 is the target test item to compare
            * against the current active to filter value.
            **/

            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveTo = activeTo
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-01", "2020-03-20")]
        public void Test_GetFilteredContracts_WhenFilterActiveFromAndActiveToAreAvailable(DateTime activeFrom, DateTime activeTo)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                ActiveFrom = activeFrom,
                ActiveTo = activeTo
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-01-01")]
        public void Test_GetFilteredContracts_WhenFilterNextDueDateFromIsAvailable(DateTime nextDueDateFrom)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                NextDueDateFrom = nextDueDateFrom,
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 9
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2020-04-11")]
        public void Test_GetFilteredContracts_WhenFilterNextDueDateFromIsEqualToContractNextDueDate(DateTime nextDueDateFrom)
        {
            /**
            * The intention of this test case is to test the "NextDueDateFrom"
            * filter against the contract next due date that are equal with
            * each other, in this case we pick Contract # 4 to test
            * against the next "NextDueDateFrom" filter
            **/
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                NextDueDateFrom = nextDueDateFrom,
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 9
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2019-12-25")]
        public void Test_GetFilteredContracts_WhenFilterNextDueDateToIsAvailable(DateTime nextDueDateTo)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                NextDueDateTo = nextDueDateTo,
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("2019-12-01", "2020-06-15")]
        public void Test_GetFilteredContracts_WhenFilterNextDueDateFromAndNextDueDateToAreAvailable(DateTime nextDueDateFrom, DateTime nextDueDateTo)
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                NextDueDateFrom = nextDueDateFrom,
                NextDueDateTo = nextDueDateTo,
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 1
                },
                new Contract {
                    Id = 6
                },
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 7
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredContracts_WhenFilterOpenContractIsAvailable()
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                OpenContract = true
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 4
                },
                new Contract {
                    Id = 10
                },
                new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredContracts_WhenFilterSortOrderAscendingIsAvailable()
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
                 new Contract {
                    Id = 6
                },
                 new Contract {
                    Id = 4
                },
                 new Contract {
                    Id = 7
                },
                 new Contract {
                    Id = 10
                },
                 new Contract {
                    Id = 9
                },
                 new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredContracts_WhenFilterSortOrderIsNotAvailable()
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                SortOrder = SortOrderEnum.Ascending
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                new Contract {
                    Id = 3
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 1
                },
                 new Contract {
                    Id = 6
                },
                 new Contract {
                    Id = 4
                },
                 new Contract {
                    Id = 7
                },
                 new Contract {
                    Id = 10
                },
                 new Contract {
                    Id = 9
                },
                 new Contract {
                    Id = 5
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredContracts_WhenFilterSortOrderDescendingAvailable()
        {
            // Arrange
            var contractRepository = new ContractRepository();
            var contractList = GetContractList();
            var comparer = new ContractComparer();
            var contractFilter = new ContractFilter
            {
                SortOrder = SortOrderEnum.Descending
            };

            // Act
            var actualResult = contractRepository.GetFilteredContracts(contractList, contractFilter);

            // Assert
            var expectedResult = new List<Contract> {
                 new Contract {
                    Id = 5
                },
                 new Contract {
                    Id = 9
                },
                 new Contract {
                    Id = 10
                },
                 new Contract {
                    Id = 7
                },
                new Contract {
                    Id = 4
                },
                 new Contract {
                    Id = 6
                },
                 new Contract {
                    Id = 1
                },
                 new Contract {
                    Id = 8
                },
                new Contract {
                    Id = 2
                },
                new Contract {
                    Id = 3
                },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        private List<Contract> GetContractList()
        {
            var contracts = new List<Contract>();

            contracts.Add(
                new Contract
                {
                    Id = 1,
                    Code = "G1-Code-1001",
                    TenantId = 1,
                    TermId = 1,
                    EffectiveDate = new DateTime(2019, 09, 15),
                    IsOpenContract = false,
                    DurationValue = 1,
                    DurationUnit = DurationEnum.Year,
                    Term = new Term
                    {
                        DurationUnit = DurationEnum.Month
                    },
                    AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2019, 10, 15)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2019, 11, 15)
                        },
                    }
                }
            );

            contracts.Add(
               new Contract
               {
                   Id = 2,
                   Code = "G1-Code-1002",
                   TenantId = 2,
                   TermId = 2,
                   EffectiveDate = new DateTime(2019, 07, 02),
                   IsOpenContract = false,
                   DurationValue = 5,
                   DurationUnit = DurationEnum.Month,
                   Term = new Term
                   {
                       DurationUnit = DurationEnum.Week
                   },
                   AccountStatements = new List<AccountStatement>
                   {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2019, 07, 09)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2019, 07, 16)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2019, 07, 23)
                        },
                   }
               }
           );

            contracts.Add(
                new Contract
                {
                    Id = 3,
                    Code = "G1-Code-1003",
                    TenantId = 3,
                    TermId = 3,
                    EffectiveDate = new DateTime(2019, 05, 10),
                    IsOpenContract = false,
                    DurationValue = 6,
                    DurationUnit = DurationEnum.Month,
                    Term = new Term
                    {
                        DurationUnit = DurationEnum.Month
                    },
                    AccountStatements = new List<AccountStatement>()
                }
            );

            contracts.Add(
                new Contract
                {
                    Id = 4,
                    Code = "G1-Code-1004",
                    TenantId = 3,
                    TermId = 1,
                    EffectiveDate = new DateTime(2020, 02, 11),
                    IsOpenContract = true,
                    Term = new Term
                    {
                        DurationUnit = DurationEnum.Month
                    },
                    AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 03, 11)
                        },
                   }
                }
            );

            contracts.Add(
               new Contract
               {
                   Id = 5,
                   Code = "G1-Code-1005",
                   TenantId = 4,
                   TermId = 3,
                   EffectiveDate = new DateTime(2020, 01, 23),
                   IsOpenContract = true,
                   Term = new Term
                   {
                       DurationUnit = DurationEnum.Quarter
                   },
                   AccountStatements = new List<AccountStatement>
                   {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Quarter,
                            DueDate = new DateTime(2020, 04, 23)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Quarter,
                            DueDate = new DateTime(2020, 07, 23)
                        },
                  }
               }
           );

            contracts.Add(
               new Contract
               {
                   Id = 6,
                   Code = "G2-Code-1006",
                   TenantId = 5,
                   TermId = 5,
                   EffectiveDate = new DateTime(2020, 01, 15),
                   IsOpenContract = false,
                   DurationValue = 1,
                   DurationUnit = DurationEnum.Month,
                   Term = new Term
                   {
                       DurationUnit = DurationEnum.Week
                   },
                   AccountStatements = new List<AccountStatement>
                   {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2020, 01, 22)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2020, 01, 29)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Week,
                            DueDate = new DateTime(2020, 02, 05)
                        },
                  }
               }
           );

            contracts.Add(
               new Contract
               {
                   Id = 7,
                   Code = "G2-Code-1007",
                   TenantId = 6,
                   TermId = 1,
                   EffectiveDate = new DateTime(2020, 02, 02),
                   IsOpenContract = false,
                   DurationValue = 1,
                   DurationUnit = DurationEnum.Year,
                   Term = new Term
                   {
                       DurationUnit = DurationEnum.Month
                   },
                   AccountStatements = new List<AccountStatement>
                   {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 03, 02)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 04, 02)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 05, 02)
                        },
                  }
               }
           );

            contracts.Add(
               new Contract
               {
                   Id = 8,
                   Code = "G2-Code-1008",
                   TenantId = 7,
                   TermId = 2,
                   EffectiveDate = new DateTime(2019, 08, 18),
                   IsOpenContract = false,
                   DurationValue = 3,
                   DurationUnit = DurationEnum.Month,
                   Term = new Term
                   {
                       DurationUnit = DurationEnum.Month
                   },
                   AccountStatements = new List<AccountStatement>
                   {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2019, 09, 18)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2019, 10, 18)
                        },
                  }
               }
           );

            contracts.Add(
                new Contract
                {
                    Id = 9,
                    Code = "G2-Code-1009",
                    TenantId = 8,
                    TermId = 8,
                    EffectiveDate = new DateTime(2020, 04, 12),
                    IsOpenContract = false,
                    DurationValue = 4,
                    DurationUnit = DurationEnum.Quarter,
                    Term = new Term
                    {
                        DurationUnit = DurationEnum.Quarter
                    },
                    AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Quarter,
                            DueDate = new DateTime(2020, 07, 12)
                        },
                    }
                }
            );

            contracts.Add(
                new Contract
                {
                    Id = 10,
                    Code = "G2-Code-1010",
                    TenantId = 9,
                    TermId = 1,
                    EffectiveDate = new DateTime(2020, 04, 12),
                    IsOpenContract = true,
                    Term = new Term
                    {
                        DurationUnit = DurationEnum.Month
                    },
                    AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 05, 12)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 06, 12)
                        },
                        new AccountStatement
                        {
                            DurationUnit = DurationEnum.Month,
                            DueDate = new DateTime(2020, 07, 12)
                        },
                    }
                }
            );

            return contracts;
        }
    }
}