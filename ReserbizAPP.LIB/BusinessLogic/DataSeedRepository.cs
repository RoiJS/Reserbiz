using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using RestSharp;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class DataSeedRepository
        : BaseRepository<IEntity>, IDataSeedRepository<IEntity>
    {
        public ReserbizClientDataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IOptions<AppHostInfo> _appHostInfo;
        private readonly IOptions<AppSettingsURL> _appSettingsUrl;

        public DataSeedRepository(IReserbizRepository<IEntity> reserbizRepository,
            IConfiguration configuration,
            IOptions<ApplicationSettings> appSettings,
            IOptions<AppHostInfo> appHostInfo,
            IOptions<AppSettingsURL> appSettingsUrl)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _appSettings = appSettings;
            _appHostInfo = appHostInfo;
            _appSettingsUrl = appSettingsUrl;
            _configuration = configuration;
            _context = reserbizRepository.ClientDbContext;
        }

        public async Task SeedData(UserAccount userAccount, Client client)
        {
            switch (client.Type)
            {
                case ClientTypeEnum.Demo:
                    SeedAccounts(userAccount);
                    SeedTenants();
                    SeedUnitTypes();
                    SeedUnits();
                    SeedTerms();
                    await SeedContracts(client);
                    SeedSettings(client);
                    break;
                case ClientTypeEnum.Regular:
                    SeedAccounts(userAccount);
                    SeedSettings(client);
                    break;
            }
        }
        private void SeedAccounts(UserAccount userAccount)
        {
            var defaultUsername = GenerateUsername(userAccount);
            var defaultPassword = GeneratePassword();

            userAccount.Username = defaultUsername;
            userAccount.Password = defaultPassword;

            byte[] passwordHash, passwordSalt;

            SystemUtility.EncryptionUtility.CreatePasswordHash(defaultPassword, out passwordHash, out passwordSalt);

            if (!_context.Accounts.Any())
            {
                _context.Accounts.Add(
                     new Account
                     {
                         FirstName = userAccount.FirstName,
                         MiddleName = userAccount.MiddleName,
                         LastName = userAccount.LastName,
                         Gender = GenderEnum.Male,
                         PhotoUrl = "",
                         Username = defaultUsername,
                         PasswordHash = passwordHash,
                         PasswordSalt = passwordSalt,
                         EmailAddress = userAccount.EmailAddress
                     }
                 );

                _context.SaveChanges();
            }
        }
        private void SeedTenants()
        {
            if (!_context.Tenants.Any())
            {
                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Nicole",
                        MiddleName = "Bardwell",
                        LastName = "Bardwell",
                        Address = "Schenck Place Sheatown",
                        ContactNumber = "09944412827",
                        Gender = GenderEnum.Female,
                        EmailAddress = "nicolebardwell@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Willis",
                                MiddleName = "Hood",
                                LastName = "Arnold",
                                Gender = GenderEnum.Male,
                                ContactNumber = "9834133245"
                            },
                            new ContactPerson {
                                FirstName = "Wolfe",
                                MiddleName = "Mccarthy",
                                LastName = "Clarke",
                                Gender = GenderEnum.Male,
                                ContactNumber = "9144002148"
                            }
                        }
                    }
                );

                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Snyder",
                        MiddleName = "Malone",
                        LastName = "Fox",
                        Address = "Bond Street Wilmington",
                        ContactNumber = "09125443882",
                        Gender = GenderEnum.Male,
                        EmailAddress = "snyderfox@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Lana",
                                MiddleName = "Mcpherson",
                                LastName = "Randolph",
                                Gender = GenderEnum.Female,
                                ContactNumber = "9574473000"
                            },
                            new ContactPerson {
                                FirstName = "Alford",
                                MiddleName = "Stuart",
                                LastName = "Stafford",
                                Gender = GenderEnum.Male,
                                ContactNumber = "8654783806"
                            }
                        }
                    }
                );

                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Susie",
                        MiddleName = "Copeland",
                        LastName = "Myers",
                        Address = "Waldane Court Buxton",
                        ContactNumber = "09075332056",
                        Gender = GenderEnum.Female,
                        EmailAddress = "susiemyers@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Lorena",
                                MiddleName = "Lawson",
                                LastName = "Kirkland",
                                Gender = GenderEnum.Female,
                                ContactNumber = "9954722522"
                            },
                            new ContactPerson {
                                FirstName = "Nannie",
                                MiddleName = "Frost",
                                LastName = "Martin",
                                Gender = GenderEnum.Female,
                                ContactNumber = "8934453926"
                            }
                        }
                    }
                );

                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Snow",
                        MiddleName = "Solis",
                        LastName = "Spencer",
                        Address = "Debevoise Avenue Bascom",
                        ContactNumber = "09265212646",
                        Gender = GenderEnum.Male,
                        EmailAddress = "snowspencer@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Morton",
                                MiddleName = "Murphy",
                                LastName = "Petty",
                                Gender = GenderEnum.Male,
                                ContactNumber = "9464043593"
                            },
                            new ContactPerson {
                                FirstName = "Diaz",
                                MiddleName = "Paul",
                                LastName = "Cash",
                                Gender = GenderEnum.Male,
                                ContactNumber = "8055022916"
                            }
                        }
                    }
                );

                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Fitzpatrick",
                        MiddleName = "Gilbert",
                        LastName = "Gamble",
                        Address = "Foster Avenue Carrsville",
                        ContactNumber = "09875643910",
                        Gender = GenderEnum.Male,
                        EmailAddress = "fitzpatrickgamble@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Randall",
                                MiddleName = "Powers",
                                LastName = "Rivers",
                                Gender = GenderEnum.Male,
                                ContactNumber = "9855883811"
                            },
                            new ContactPerson {
                                FirstName = "Manning",
                                MiddleName = "Hall",
                                LastName = "Mcfadden",
                                Gender = GenderEnum.Male,
                                ContactNumber = "8535603651"
                            }
                        }
                    }
                );

                _context.Tenants.Add(
                    new Tenant
                    {
                        FirstName = "Patrick",
                        MiddleName = "James",
                        LastName = "Young",
                        Address = "Foster Avenue Carrsville",
                        ContactNumber = "09875642345",
                        Gender = GenderEnum.Male,
                        EmailAddress = "patrickyoung@radiantix.com",
                        ContactPersons = new List<ContactPerson> {
                            new ContactPerson {
                                FirstName = "Randall",
                                MiddleName = "Powers",
                                LastName = "Rivers",
                                Gender = GenderEnum.Male,
                                ContactNumber = "9855883811"
                            },
                            new ContactPerson {
                                FirstName = "Manning",
                                MiddleName = "Hall",
                                LastName = "Mcfadden",
                                Gender = GenderEnum.Male,
                                ContactNumber = "8535603651"
                            }
                        }
                    }
                );

                _context.SaveChanges();
            }
        }
        private void SeedUnitTypes()
        {
            if (!_context.SpaceTypes.Any())
            {
                _context.SpaceTypes.Add(
                    new SpaceType
                    {
                        Name = "Room (Studio Type)",
                        Description = "Id labore deserunt non mollit enim sit excepteur fugiat sit reprehenderit exercitation anim cupidatat elit.",
                        Rate = 9000
                    }
                );

                _context.SpaceTypes.Add(
                    new SpaceType
                    {
                        Name = "Bed Space",
                        Description = "Dolor consectetur dolor nisi eu proident.",
                        Rate = 2500
                    }
                );

                _context.SpaceTypes.Add(
                    new SpaceType
                    {
                        Name = "Condo",
                        Description = "Aliqua do excepteur cillum occaecat.",
                        Rate = 13000
                    }
                );

                _context.SpaceTypes.Add(
                    new SpaceType
                    {
                        Name = "Room (Non-Studio Type)",
                        Description = "Minim veniam sint sunt laboris ex aute ea esse in id cupidatat nisi duis.",
                        Rate = 6000
                    }
                );

                _context.SaveChanges();
            }
        }
        private void SeedUnits()
        {
            if (!_context.Spaces.Any())
            {
                var roomStudioType = GetUnitType("Room (Studio Type)");
                var bedSpace = GetUnitType("Bed Space");
                var condo = GetUnitType("Condo");
                var roomNonStudioType = GetUnitType("Room (Non-Studio Type)");

                _context.Spaces.Add(new Space
                {
                    Description = "Room 101",
                    SpaceTypeId = roomStudioType.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 102",
                    SpaceTypeId = roomStudioType.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 103 - Bed Unit 1",
                    SpaceTypeId = bedSpace.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 103 - Bed Unit 2",
                    SpaceTypeId = bedSpace.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 103 - Bed Unit 3",
                    SpaceTypeId = bedSpace.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 104",
                    SpaceTypeId = condo.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 105",
                    SpaceTypeId = condo.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 106",
                    SpaceTypeId = roomNonStudioType.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 107",
                    SpaceTypeId = roomNonStudioType.Id
                });

                _context.Spaces.Add(new Space
                {
                    Description = "Room 108",
                    SpaceTypeId = roomNonStudioType.Id
                });

                _context.SaveChanges();
            }
        }
        private SpaceType GetUnitType(string unitTypeName)
        {
            return _context.SpaceTypes.Where(s => s.Name == unitTypeName).FirstOrDefault();
        }
        private void SeedTerms()
        {
            if (!_context.Terms.Any())
            {
                var unitTypeIdForRoomStudioType = GetUnitType("Room (Studio Type)");
                var unitTypeIdForBedSpace = GetUnitType("Bed Space");
                var unitTypeIdForCondo = GetUnitType("Condo");
                var unitTypeIdForRoomNonStudioType = GetUnitType("Room (Non-Studio Type)");

                var unitTypeForRoomStudioTypeTermTemplate = new Term
                {
                    Code = "0001",
                    Name = "Term-0001",
                    SpaceTypeId = unitTypeIdForRoomStudioType.Id,
                    Rate = 9000,
                    MaximumNumberOfOccupants = 2,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ExcludeElectricBill = true,
                    ElectricBillAmount = 550,
                    ExcludeWaterBill = true,
                    WaterBillAmount = 350,
                    TermMiscellaneous = new List<TermMiscellaneous> {
                        new TermMiscellaneous
                        {
                            Name = "Utility Fee",
                            Description = "Esse voluptate ullamco nostrud quis velit excepteur.",
                            Amount = 500
                        },
                        new TermMiscellaneous
                        {
                            Name = "Parking Fee",
                            Description = "Duis eiusmod officia fugiat labore duis enim eiusmod laborum magn",
                            Amount = 1000
                        },
                    },

                    PenaltyValue = 50,
                    PenaltyValueType = ValueTypeEnum.Fixed,
                    PenaltyAmountPerDurationUnit = DurationEnum.Day,
                    PenaltyEffectiveAfterDurationValue = 3,
                    PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                    GenerateAccountStatementDaysBeforeValue = 5,
                    MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate
                };
                var unitTypeForBedSpaceTermTemplate = new Term
                {
                    Code = "0002",
                    Name = "Term-0002",
                    SpaceTypeId = unitTypeIdForBedSpace.Id,
                    Rate = 2800,
                    MaximumNumberOfOccupants = 1,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 0,
                    ExcludeElectricBill = false,
                    ElectricBillAmount = 0,
                    ExcludeWaterBill = false,
                    WaterBillAmount = 0,
                    TermMiscellaneous = new List<TermMiscellaneous> {
                        new TermMiscellaneous
                        {
                            Name = "Gasoline Fee",
                            Description = "Esse voluptate ullamco nostrud quis velit excepteur.",
                            Amount = 100
                        },
                        new TermMiscellaneous
                        {
                            Name = "Utility Fee",
                            Description = "Duis eiusmod officia fugiat labore duis enim eiusmod laborum magn",
                            Amount = 250
                        },
                    },

                    PenaltyValue = 3,
                    PenaltyValueType = ValueTypeEnum.Percentage,
                    PenaltyAmountPerDurationUnit = DurationEnum.Day,
                    PenaltyEffectiveAfterDurationValue = 5,
                    PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                    GenerateAccountStatementDaysBeforeValue = 5,
                    MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate
                };
                var unitTypeIdForCondoTermTemplate = new Term
                {
                    Code = "0003",
                    Name = "Term-0003",
                    SpaceTypeId = unitTypeIdForCondo.Id,
                    Rate = 13000,
                    MaximumNumberOfOccupants = 5,
                    DurationUnit = DurationEnum.Month,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 3,
                    ExcludeElectricBill = true,
                    ElectricBillAmount = 1200,
                    ExcludeWaterBill = true,
                    WaterBillAmount = 650,
                    TermMiscellaneous = new List<TermMiscellaneous> {
                        new TermMiscellaneous
                        {
                            Name = "Parking Fee",
                            Description = "Esse voluptate ullamco nostrud quis velit excepteur.",
                            Amount = 1000
                        },
                        new TermMiscellaneous
                        {
                            Name = "Utility Fee",
                            Description = "Duis eiusmod officia fugiat labore duis enim eiusmod laborum magn",
                            Amount = 550
                        },
                    },

                    PenaltyValue = 5,
                    PenaltyValueType = ValueTypeEnum.Percentage,
                    PenaltyAmountPerDurationUnit = DurationEnum.Day,
                    PenaltyEffectiveAfterDurationValue = 3,
                    PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                    GenerateAccountStatementDaysBeforeValue = 5,
                    MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate
                };
                var unitTypeIdForRoomNonStudioTypeTermTemplate = new Term
                {
                    Code = "0004",
                    Name = "Term-0004",
                    SpaceTypeId = unitTypeIdForRoomNonStudioType.Id,
                    Rate = 6500,
                    MaximumNumberOfOccupants = 2,
                    DurationUnit = DurationEnum.Week,
                    AdvancedPaymentDurationValue = 1,
                    DepositPaymentDurationValue = 2,
                    ExcludeElectricBill = true,
                    ElectricBillAmount = 0,
                    ExcludeWaterBill = true,
                    WaterBillAmount = 0,
                    TermMiscellaneous = new List<TermMiscellaneous>(),
                    PenaltyValue = 0,
                    GenerateAccountStatementDaysBeforeValue = 5,
                    MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate
                };

                _context.Terms.Add(unitTypeForRoomStudioTypeTermTemplate);
                _context.Terms.Add(unitTypeForBedSpaceTermTemplate);
                _context.Terms.Add(unitTypeIdForCondoTermTemplate);
                _context.Terms.Add(unitTypeIdForRoomNonStudioTypeTermTemplate);

                _context.SaveChanges();
            }
        }

        private Term GetTerm(string code)
        {
            return _context.Terms.Where(t => t.Code == code).FirstOrDefault();
        }

        private void ResetTermMiscellaneousId(List<TermMiscellaneous> termMiscellaneous)
        {
            termMiscellaneous.ForEach((TermMiscellaneous t) =>
            {
                t.Id = 0;
                t.TermId = 0;
            });
        }

        private Term CloneTermDetails(Term originalTerm)
        {
            var newTerm = new Term();
            newTerm.Id = 0;
            newTerm.TermParentId = originalTerm.TermParentId;
            newTerm.Name = originalTerm.Name;
            newTerm.Code = originalTerm.Code;
            newTerm.SpaceTypeId = originalTerm.SpaceTypeId;
            newTerm.Rate = originalTerm.Rate;
            newTerm.MaximumNumberOfOccupants = originalTerm.MaximumNumberOfOccupants;
            newTerm.DurationUnit = originalTerm.DurationUnit;
            newTerm.AdvancedPaymentDurationValue = originalTerm.AdvancedPaymentDurationValue;
            newTerm.DepositPaymentDurationValue = originalTerm.DepositPaymentDurationValue;
            newTerm.ExcludeElectricBill = originalTerm.ExcludeElectricBill;
            newTerm.ElectricBillAmount = originalTerm.ElectricBillAmount;
            newTerm.ExcludeWaterBill = originalTerm.ExcludeWaterBill;
            newTerm.WaterBillAmount = originalTerm.WaterBillAmount;
            newTerm.PenaltyValue = originalTerm.PenaltyValue;
            newTerm.PenaltyValueType = originalTerm.PenaltyValueType;
            newTerm.PenaltyAmountPerDurationUnit = originalTerm.PenaltyAmountPerDurationUnit;
            newTerm.PenaltyEffectiveAfterDurationValue = originalTerm.PenaltyEffectiveAfterDurationValue;
            newTerm.PenaltyEffectiveAfterDurationUnit = originalTerm.PenaltyEffectiveAfterDurationUnit;
            newTerm.GenerateAccountStatementDaysBeforeValue = originalTerm.GenerateAccountStatementDaysBeforeValue;
            newTerm.MiscellaneousDueDate = originalTerm.MiscellaneousDueDate;
            newTerm.IncludeMiscellaneousCheckAndCalculateForPenalty = originalTerm.IncludeMiscellaneousCheckAndCalculateForPenalty;

            newTerm.TermMiscellaneous = new List<TermMiscellaneous>();

            originalTerm.TermMiscellaneous.ForEach(t =>
            {
                newTerm.TermMiscellaneous.Add(t);
            });

            return newTerm;
        }

        private async Task SeedContracts(Client client)
        {
            if (!_context.Contracts.Any())
            {
                var tenants = _context.Tenants.ToList();
                var terms = _context.Terms.Includes(t => t.TermMiscellaneous).ToList();
                var account = _context.Accounts.FirstOrDefault();

                // Setup terms to be used
                var unitTypeForRoomStudioTypeTerm = GetTerm("0001");
                var unitTypeForBedSpaceTerm = GetTerm("0002");
                var unitTypeIdForCondoTerm = GetTerm("0003");
                var unitTypeIdForRoomNonStudioTypeTerm = GetTerm("0004");

                _context.Entry(unitTypeForRoomStudioTypeTerm).State = EntityState.Detached;
                _context.Entry(unitTypeForBedSpaceTerm).State = EntityState.Detached;
                _context.Entry(unitTypeIdForCondoTerm).State = EntityState.Detached;
                _context.Entry(unitTypeIdForRoomNonStudioTypeTerm).State = EntityState.Detached;

                unitTypeForRoomStudioTypeTerm.TermParentId = unitTypeForRoomStudioTypeTerm.Id;
                unitTypeForBedSpaceTerm.TermParentId = unitTypeForBedSpaceTerm.Id;
                unitTypeIdForCondoTerm.TermParentId = unitTypeIdForCondoTerm.Id;
                unitTypeIdForRoomNonStudioTypeTerm.TermParentId = unitTypeIdForRoomNonStudioTypeTerm.Id;

                unitTypeForRoomStudioTypeTerm.Id = 0;
                unitTypeForBedSpaceTerm.Id = 0;
                unitTypeIdForCondoTerm.Id = 0;
                unitTypeIdForRoomNonStudioTypeTerm.Id = 0;

                ResetTermMiscellaneousId(unitTypeForRoomStudioTypeTerm.TermMiscellaneous);
                ResetTermMiscellaneousId(unitTypeForBedSpaceTerm.TermMiscellaneous);
                ResetTermMiscellaneousId(unitTypeIdForCondoTerm.TermMiscellaneous);
                ResetTermMiscellaneousId(unitTypeIdForRoomNonStudioTypeTerm.TermMiscellaneous);

                var termForContract1 = CloneTermDetails(unitTypeForRoomStudioTypeTerm);
                var termForContract2 = CloneTermDetails(unitTypeForRoomStudioTypeTerm);
                var termForContract3 = CloneTermDetails(unitTypeForBedSpaceTerm);
                var termForContract4 = CloneTermDetails(unitTypeForBedSpaceTerm);
                var termForContract5 = CloneTermDetails(unitTypeIdForCondoTerm);
                var termForContract6 = CloneTermDetails(unitTypeIdForRoomNonStudioTypeTerm);
                var termForContract7 = CloneTermDetails(unitTypeIdForRoomNonStudioTypeTerm);

                // Set up units to be used
                var roomStudioType1 = GetUnit("Room 101");
                var roomStudioType2 = GetUnit("Room 102");
                var bedSpace1 = GetUnit("Room 103 - Bed Unit 1");
                var bedSpace2 = GetUnit("Room 103 - Bed Unit 2");
                var roomNonStudioType1 = GetUnit("Room 106");
                var roomNonStudioType2 = GetUnit("Room 107");
                var condo1 = GetUnit("Room 104");

                // Setup sample dynamic dates for contracts

                // Contract Effective date will be dynamic based on the current date minus a number of months and additional few days
                // Example: current date minus 8 months plus 15 days
                // Current date = 2021-04-15
                // Sample Effective date = 2020-08-30
                _context.Contracts.Add(new Contract
                {
                    Code = "C-0001",
                    TenantId = tenants[0].Id,
                    Term = termForContract1,
                    EffectiveDate = DateTime.Now.AddMonths(-8).AddDays(15),
                    DurationValue = 12,
                    DurationUnit = DurationEnum.Month,
                    IsOpenContract = false,
                    SpaceId = roomStudioType1.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0002",
                    TenantId = tenants[1].Id,
                    Term = termForContract2,
                    EffectiveDate = DateTime.Now.AddMonths(-5).AddDays(3),
                    DurationValue = 0,
                    DurationUnit = DurationEnum.None,
                    IsOpenContract = true,
                    SpaceId = roomStudioType2.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0003",
                    TenantId = tenants[2].Id,
                    Term = termForContract3,
                    EffectiveDate = DateTime.Now.AddMonths(-3).AddDays(10),
                    DurationValue = 6,
                    DurationUnit = DurationEnum.Month,
                    IsOpenContract = false,
                    SpaceId = bedSpace1.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0004",
                    TenantId = tenants[3].Id,
                    Term = termForContract4,
                    EffectiveDate = DateTime.Now.AddMonths(-1).AddDays(2),
                    DurationValue = 4,
                    DurationUnit = DurationEnum.Week,
                    IsOpenContract = false,
                    SpaceId = bedSpace2.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0005",
                    TenantId = tenants[4].Id,
                    Term = termForContract5,
                    EffectiveDate = DateTime.Now.AddMonths(-6).AddDays(9),
                    DurationValue = 12,
                    DurationUnit = DurationEnum.Month,
                    IsOpenContract = false,
                    SpaceId = condo1.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0006",
                    TenantId = tenants[2].Id,
                    Term = termForContract6,
                    EffectiveDate = DateTime.Now.AddMonths(-3).AddDays(13),
                    DurationValue = 0,
                    DurationUnit = DurationEnum.None,
                    IsOpenContract = true,
                    SpaceId = roomNonStudioType2.Id,
                });

                _context.Contracts.Add(new Contract
                {
                    Code = "C-0007",
                    TenantId = tenants[1].Id,
                    Term = termForContract7,
                    EffectiveDate = DateTime.Now.AddMonths(-4).AddDays(6),
                    DurationValue = 0,
                    DurationUnit = DurationEnum.None,
                    IsOpenContract = true,
                    SpaceId = roomNonStudioType1.Id,
                });

                _context.SaveChanges();

                await SendRequestToGenerateAccountStatements(client, account);
            }
        }
        private Space GetUnit(string description)
        {
            return _context.Spaces.Where(s => s.Description == description).FirstOrDefault();
        }
        private void SeedSettings(Client client)
        {
            if (!_context.ClientSettings.Any())
            {
                _context.ClientSettings.Add(new ClientSettings
                {
                    BusinessName = client.Name
                });

                _context.SaveChanges();
            }
        }
        private async Task SendRequestToGenerateAccountStatements(Client client, Account account)
        {
            try
            {
                var url = String.Format("{0}{1}/{2}", _appHostInfo.Value.Domain, _appSettings.Value.AppSettingsURL.AutoGenerateAccountStatementsForNewDatabaseURL, account.Id);
                var httpClient = new RestClient(url);
                httpClient.Timeout = -1;
                var httpRequest = new RestRequest(Method.POST);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                await httpClient.ExecuteAsync(httpRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to auto generate statement of accounts. Error Message: {ex.InnerException.Message}");
            }
        }
        private string GenerateUsername(UserAccount userAccount)
        {
            var username = String.Format("{0}{1}", userAccount.FirstName.ToLower().Substring(0, 2), userAccount.LastName.ToLower().Substring(0, 2));
            return username;
        }

        private string GeneratePassword()
        {
            // For now this will be the default password
            return "Starta123";
        }
    }
}