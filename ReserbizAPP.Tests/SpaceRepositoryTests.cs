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
    public class SpaceRepositoryTests
    {

        [Test]
        public void Test_GetFilteredSpaces_WhenFilterIsDefault()
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter();

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 1,
               },
               new Space {
                   Id = 2,
               },
               new Space {
                   Id = 3,
               },
               new Space {
                   Id = 4,
               },
               new Space {
                   Id = 5,
               },
               new Space {
                   Id = 6,
               },
               new Space {
                   Id = 7,
               },
               new Space {
                   Id = 8,
               },
               new Space {
                   Id = 9,
               },
               new Space {
                   Id = 10,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("103")]
        public void Test_GetFilteredSpaces_WhenInfixKeywordDescriptionIsAvailable(string description)
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Description = description
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 3,
               },
               new Space {
                   Id = 4,
               },
               new Space {
                   Id = 5,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("Room")]
        public void Test_GetFilteredSpaces_WhenPrefixKeywordDescriptionIsAvailable(string description)
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Description = description
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 1,
               },
               new Space {
                   Id = 2,
               },
               new Space {
                   Id = 3,
               },
               new Space {
                   Id = 4,
               },
               new Space {
                   Id = 5,
               },
               new Space {
                   Id = 6,
               },
               new Space {
                   Id = 7,
               },
               new Space {
                   Id = 8,
               },
               new Space {
                   Id = 9,
               },
               new Space {
                   Id = 10,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [TestCase("08")]
        public void Test_GetFilteredSpaces_WhenSuffixKeywordDescriptionIsAvailable(string description)
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Description = description
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 10,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredSpaces_WhenUnitTypeHasValue()
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var roomStudioType = GetUnitTypes()[0];

            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                UnitTypeId = roomStudioType.Id
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 1,
               },
               new Space {
                   Id = 2,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredSpaces_WhenUnitStatusIsSetToAll()
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Status = UnitStatusEnum.All
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 1,
               },
               new Space {
                   Id = 2,
               },
               new Space {
                   Id = 3,
               },
               new Space {
                   Id = 4,
               },
               new Space {
                   Id = 5,
               },
               new Space {
                   Id = 6,
               },
               new Space {
                   Id = 7,
               },
               new Space {
                   Id = 8,
               },
               new Space {
                   Id = 9,
               },
               new Space {
                   Id = 10,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredSpaces_WhenUnitStatusIsSetToOccupied()
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Status = UnitStatusEnum.Occupied
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 1,
               },
               new Space {
                   Id = 3,
               },
               new Space {
                   Id = 6,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        [Test]
        public void Test_GetFilteredSpaces_WhenUnitStatusIsSetToAvailable()
        {
            // Arrange
            var spaceRepository = new SpaceRepository();
            var spaceList = GetUnits();
            var comparer = new SpaceComparer();
            var spaceFilter = new SpaceFilter
            {
                Status = UnitStatusEnum.Available
            };

            // Act
            var actualResult = spaceRepository.GetFilteredSpaces(spaceList, spaceFilter);

            // Assert
            var expectedResult = new List<Space> {
               new Space {
                   Id = 2,
               },
               new Space {
                   Id = 4,
               },
               new Space {
                   Id = 5,
               },
               new Space {
                   Id = 7,
               },
               new Space {
                   Id = 8,
               },
               new Space {
                   Id = 9,
               },
               new Space {
                   Id = 10,
               },
            };

            CollectionAssert.AreEqual(actualResult, expectedResult, comparer);
        }

        private List<Space> GetUnits()
        {
            var units = new List<Space>();
            var unitTypes = GetUnitTypes();
            var contracts = GetContracts();

            var roomStudioType = unitTypes[0];
            var bedSpace = unitTypes[1];
            var condo = unitTypes[2];
            var roomNonStudioType = unitTypes[3];

            var contract1 = contracts[0];
            var contract2 = contracts[1];
            var contract3 = contracts[2];

            // Studio Type and Occupied
            units.Add(new Space
            {
                Id = 1,
                Description = "Room 101",
                SpaceTypeId = roomStudioType.Id,
                Contracts = new List<Contract>() { contract1 }
            });

            // Studio Type and Available
            units.Add(new Space
            {
                Id = 2,
                Description = "Room 102",
                SpaceTypeId = roomStudioType.Id
            });

            // Bed Space and Occupied
            units.Add(new Space
            {
                Id = 3,
                Description = "Room 103 - Bed Unit 1",
                SpaceTypeId = bedSpace.Id,
                Contracts = new List<Contract>() { contract2 }
            });

            // Bed Space and Available
            units.Add(new Space
            {
                Id = 4,
                Description = "Room 103 - Bed Unit 2",
                SpaceTypeId = bedSpace.Id
            });

            // Bed Space and Available
            units.Add(new Space
            {
                Id = 5,
                Description = "Room 103 - Bed Unit 3",
                SpaceTypeId = bedSpace.Id
            });

            // Condo and Occupied
            units.Add(new Space
            {
                Id = 6,
                Description = "Room 104",
                SpaceTypeId = condo.Id,
                Contracts = new List<Contract>() { contract3 }
            });

            // Condo and Available
            units.Add(new Space
            {
                Id = 7,
                Description = "Room 105",
                SpaceTypeId = condo.Id
            });

            // Non-studio type and Available
            units.Add(new Space
            {
                Id = 8,
                Description = "Room 106",
                SpaceTypeId = roomNonStudioType.Id
            });

            // Non-studio type and Available
            units.Add(new Space
            {
                Id = 9,
                Description = "Room 107",
                SpaceTypeId = roomNonStudioType.Id
            });

            // Non-studio type and Available
            units.Add(new Space
            {
                Id = 10,
                Description = "Room 108",
                SpaceTypeId = roomNonStudioType.Id
            });

            return units;
        }

        private List<SpaceType> GetUnitTypes()
        {
            var unitTypes = new List<SpaceType>();

            unitTypes.Add(
                    new SpaceType
                    {
                        Id = 1,
                        Name = "Room (Studio Type)",
                        Description = "Id labore deserunt non mollit enim sit excepteur fugiat sit reprehenderit exercitation anim cupidatat elit.",
                        Rate = 9000
                    }
                );

            unitTypes.Add(
                new SpaceType
                {
                    Id = 2,
                    Name = "Bed Space",
                    Description = "Dolor consectetur dolor nisi eu proident.",
                    Rate = 2500
                }
            );

            unitTypes.Add(
                new SpaceType
                {
                    Id = 3,
                    Name = "Condo",
                    Description = "Aliqua do excepteur cillum occaecat.",
                    Rate = 13000
                }
            );

            unitTypes.Add(
                new SpaceType
                {
                    Id = 4,
                    Name = "Room (Non-Studio Type)",
                    Description = "Minim veniam sint sunt laboris ex aute ea esse in id cupidatat nisi duis.",
                    Rate = 6000
                }
            );

            return unitTypes;
        }

        private List<Contract> GetContracts()
        {
            var contracts = new List<Contract>();
            var currentDate = new DateTime(2019, 12, 03);

            var contract1 = new Contract
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
            };
            var contract2 = new Contract
            {
                Id = 2,
                Code = "G1-Code-1002",
                TenantId = 2,
                TermId = 2,
                EffectiveDate = new DateTime(2019, 07, 02),
                IsOpenContract = false,
                DurationValue = 8,
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
            };
            var contract3 = new Contract
            {
                Id = 3,
                Code = "G1-Code-1003",
                TenantId = 3,
                TermId = 3,
                EffectiveDate = new DateTime(2019, 05, 10),
                IsOpenContract = false,
                DurationValue = 9,
                DurationUnit = DurationEnum.Month,
                Term = new Term
                {
                    DurationUnit = DurationEnum.Month
                },
                AccountStatements = new List<AccountStatement>()
            };

            contract1.SetCurrentDateTime(currentDate);
            contract2.SetCurrentDateTime(currentDate);
            contract3.SetCurrentDateTime(currentDate);

            contracts.Add(contract1);
            contracts.Add(contract2);
            contracts.Add(contract3);

            return contracts;
        }
    }
}