using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class DataSeedRepository 
        : BaseRepository<IEntity>, IDataSeedRepository<IEntity>
    {
        public ReserbizClientDataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IOptions<IApplicationSettings> _appSettings;
        public DataSeedRepository(IReserbizRepository<IEntity> reserbizRepository, IConfiguration configuration, IOptions<IApplicationSettings> appSettings)
        : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _appSettings = appSettings;
            _configuration = configuration;
            _context = reserbizRepository.ClientDbContext;
        }

    public void SeedData()
    {
        if (_appSettings.Value.ActivateDataSeed)
        {
            SeedAccounts();
            SeedTenants();
            SeedSpaceTypes();
            SeedTerms();
            SeedContracts();
            SeedSettings();
        }
    }
    private void SeedAccounts()
    {
        var password = "Starta123";
        byte[] passwordHash, passwordSalt;
        var accountsCount = _context.Accounts.ToList().Count;

        SystemUtility.EncryptionUtility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        if (!_context.Accounts.Any())
        {
            var account = new Account();
            _context.Accounts.Add(
                 new Account
                 {
                     FirstName = "James",
                     MiddleName = "Harden",
                     LastName = "Harden",
                     Gender = GenderEnum.Male,
                     PhotoUrl = "",
                     Username = "jaha",
                     PasswordHash = passwordHash,
                     PasswordSalt = passwordSalt
                 }
             );

            _context.Accounts.Add(
                new Account
                {
                    FirstName = "Russell",
                    MiddleName = "Westbrook",
                    LastName = "Westbrook",
                    Gender = GenderEnum.Male,
                    PhotoUrl = "",
                    Username = "ruwe",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            _context.Accounts.Add(
                new Account
                {
                    FirstName = "Russell",
                    MiddleName = "Westbrook",
                    LastName = "Westbrook",
                    Gender = GenderEnum.Male,
                    PhotoUrl = "",
                    Username = "ruwe",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            _context.Accounts.Add(
                new Account
                {
                    FirstName = "Eric",
                    MiddleName = "Gordon",
                    LastName = "Gordon",
                    Gender = GenderEnum.Male,
                    PhotoUrl = "",
                    Username = "ergo",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            _context.Accounts.Add(
                new Account
                {
                    FirstName = "PJ",
                    MiddleName = "Tucker",
                    LastName = "Tucker",
                    Gender = GenderEnum.Male,
                    PhotoUrl = "",
                    Username = "pjtu",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            _context.Accounts.Add(
                new Account
                {
                    FirstName = "Clint",
                    MiddleName = "Capella",
                    LastName = "Capella",
                    Gender = GenderEnum.Male,
                    PhotoUrl = "",
                    Username = "clca",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                }
            );

            _context.SaveChanges();
        }
    }
    private void SeedTenants()
    {
        var tenantCount = _context.Tenants.ToList().Count;

        if (!_context.Tenants.Any())
        {
            _context.Tenants.Add(
                new Tenant
                {
                    FirstName = "Nicole",
                    MiddleName = "Bardwell",
                    LastName = "Bardwell",
                    Address = "Schenck Place Sheatown",
                    ContactNumber = "9944412827",
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
                    ContactNumber = "9125443882",
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
                    ContactNumber = "9075332056",
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
                    ContactNumber = "8265212646",
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
                    ContactNumber = "9875643910",
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
                    FirstName = "Fitzpatrick",
                    MiddleName = "Gilbert",
                    LastName = "Gamble",
                    Address = "Foster Avenue Carrsville",
                    ContactNumber = "9875643910",
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

            _context.SaveChanges();
        }
    }
    private void SeedSpaceTypes()
    {
        if (!_context.SpaceTypes.Any())
        {
            _context.SpaceTypes.Add(
                new SpaceType
                {
                    Name = "Room (Studio Type)",
                    Description = "Id labore deserunt non mollit enim sit excepteur fugiat sit reprehenderit exercitation anim cupidatat elit.",
                    AvailableSlot = 5,
                }
            );

            _context.SpaceTypes.Add(
                new SpaceType
                {
                    Name = "Bed Space",
                    Description = "Dolor consectetur dolor nisi eu proident.",
                    AvailableSlot = 2,
                }
            );

            _context.SpaceTypes.Add(
                new SpaceType
                {
                    Name = "Condo",
                    Description = "Aliqua do excepteur cillum occaecat.",
                    AvailableSlot = 3,
                }
            );

            _context.SpaceTypes.Add(
                new SpaceType
                {
                    Name = "Room (Non-Studio Type)",
                    Description = "Minim veniam sint sunt laboris ex aute ea esse in id cupidatat nisi duis.",
                    AvailableSlot = 5,
                }
            );

            _context.SaveChanges();
        }
    }
    private void SeedTerms()
    {
        if (!_context.Terms.Any())
        {
            var spaceTypeIdForRoomStudioType = _context.SpaceTypes.Where(s => s.Name == "Room (Studio Type)").FirstOrDefault();
            var spaceTypeIdForBedSpace = _context.SpaceTypes.Where(s => s.Name == "Bed Space").FirstOrDefault();
            var spaceTypeIdForCondo = _context.SpaceTypes.Where(s => s.Name == "Condo").FirstOrDefault();
            var spaceTypeIdForRoomNonStudioType = _context.SpaceTypes.Where(s => s.Name == "Room (Non-Studio Type)").FirstOrDefault();

            _context.Terms.Add(new Term
            {
                Code = "0001",
                Name = "Term-0001",
                SpaceTypeId = spaceTypeIdForRoomStudioType.Id,
                Rate = 9000,
                MaximumNumberOfOccupants = 2,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ExcludeElectricBill = true,
                ElectricBillAmount = 0,
                ExcludeWaterBill = true,
                WaterBillAmount = 0,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            _context.Terms.Add(new Term
            {
                Code = "0002",
                Name = "Term-0002",
                SpaceTypeId = spaceTypeIdForBedSpace.Id,
                Rate = 2800,
                MaximumNumberOfOccupants = 1,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 0,
                ExcludeElectricBill = true,
                ElectricBillAmount = 250,
                ExcludeWaterBill = true,
                WaterBillAmount = 150,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            _context.Terms.Add(new Term
            {
                Code = "0003",
                Name = "Term-0003",
                SpaceTypeId = spaceTypeIdForCondo.Id,
                Rate = 13000,
                MaximumNumberOfOccupants = 5,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 3,
                ExcludeElectricBill = false,
                ElectricBillAmount = 0,
                ExcludeWaterBill = false,
                WaterBillAmount = 0,
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
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day
            });

            _context.Terms.Add(new Term
            {
                Code = "0004",
                Name = "Term-0004",
                SpaceTypeId = spaceTypeIdForRoomNonStudioType.Id,
                Rate = 6000,
                MaximumNumberOfOccupants = 2,
                DurationUnit = DurationEnum.Week,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ExcludeElectricBill = true,
                ElectricBillAmount = 500,
                ExcludeWaterBill = true,
                WaterBillAmount = 150,
                TermMiscellaneous = new List<TermMiscellaneous>(),
                PenaltyValue = 0
            });

            _context.SaveChanges();
        }
    }
    private void SeedContracts()
    {
        if (!_context.Contracts.Any())
        {
            var tenants = _context.Tenants.ToList();
            var terms = _context.Terms.Includes(t => t.TermMiscellaneous).ToList();
            var accounts = _context.Accounts.ToList();

            // Setup terms to be used
            var term1 = terms[0];
            var term2 = terms[1];
            var term3 = terms[2];
            var term4 = terms[3];

            // Setup sample dynamic dates for contracts

            // Contract Effective date will be dynamic based on the current date minus a number of months and additional few days
            // Example: current date minus 8 months plus 15 days
            // Current date = 2020-01-20
            // Sample Effective date = 2019-06-14
            var contract1_effectiveDate = DateTime.Now.AddMonths(-8).AddDays(15);
            var contract1_duration = term1.DurationUnit;
            var contract1_accountStatement1 = contract1_effectiveDate;
            var contract1_accountStatement2 = contract1_accountStatement1.AddDays(contract1_accountStatement1.CalculateDaysBasedOnDuration(1, contract1_duration));
            var contract1_accountStatement3 = contract1_accountStatement2.AddDays(contract1_accountStatement2.CalculateDaysBasedOnDuration(1, contract1_duration));

            var contract2_effectiveDate = DateTime.Now.AddMonths(-5).AddDays(3);
            var contract2_duration = term2.DurationUnit;
            var contract2_accountStatement1 = contract2_effectiveDate;
            var contract2_accountStatement2 = contract2_accountStatement1.AddDays(contract2_accountStatement1.CalculateDaysBasedOnDuration(1, contract2_duration));
            var contract2_accountStatement3 = contract2_accountStatement2.AddDays(contract2_accountStatement2.CalculateDaysBasedOnDuration(1, contract2_duration));

            var contract3_effectiveDate = DateTime.Now.AddMonths(-3).AddDays(10);
            var contract3_duration = term3.DurationUnit;
            var contract3_accountStatement1 = contract3_effectiveDate;
            var contract3_accountStatement2 = contract3_accountStatement1.AddDays(contract3_accountStatement1.CalculateDaysBasedOnDuration(1, contract3_duration));

            var contract4_effectiveDate = DateTime.Now.AddMonths(-1).AddDays(2);
            var contract4_duration = term4.DurationUnit;
            var contract4_accountStatement1 = contract4_effectiveDate;
            var contract4_accountStatement2 = contract4_accountStatement1.AddDays(contract4_accountStatement1.CalculateDaysBasedOnDuration(1, contract4_duration));
            var contract4_accountStatement3 = contract4_accountStatement2.AddDays(contract4_accountStatement2.CalculateDaysBasedOnDuration(1, contract4_duration));

            var contract5_effectiveDate = DateTime.Now.AddMonths(-6).AddDays(9);
            var contract5_duration = term2.DurationUnit;
            var contract5_accountStatement1 = contract5_effectiveDate;
            var contract5_accountStatement2 = contract5_accountStatement1.AddDays(contract5_accountStatement1.CalculateDaysBasedOnDuration(1, contract5_duration));
            var contract5_accountStatement3 = contract5_accountStatement2.AddDays(contract5_accountStatement2.CalculateDaysBasedOnDuration(1, contract5_duration));

            var contract6_effectiveDate = DateTime.Now.AddMonths(-3).AddDays(13);
            var contract6_duration = term3.DurationUnit;
            var contract6_accountStatement1 = contract6_effectiveDate;
            var contract6_accountStatement2 = contract6_accountStatement1.AddDays(contract6_accountStatement1.CalculateDaysBasedOnDuration(1, contract6_duration));
            var contract6_accountStatement3 = contract6_accountStatement2.AddDays(contract6_accountStatement2.CalculateDaysBasedOnDuration(1, contract6_duration));

            var contract7_effectiveDate = DateTime.Now.AddMonths(-4).AddDays(6);
            var contract7_duration = term4.DurationUnit;
            var contract7_accountStatement1 = contract7_effectiveDate;
            var contract7_accountStatement2 = contract7_accountStatement1.AddDays(contract7_accountStatement1.CalculateDaysBasedOnDuration(1, contract7_duration));
            var contract7_accountStatement3 = contract7_accountStatement2.AddDays(contract7_accountStatement2.CalculateDaysBasedOnDuration(1, contract7_duration));

            _context.Contracts.Add(new Contract
            {
                Code = "C-0001",
                TenantId = tenants[0].Id,
                TermId = term1.Id,
                EffectiveDate = contract1_effectiveDate,
                DurationValue = 12,
                DurationUnit = DurationEnum.Month,
                IsOpenContract = false,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract1_accountStatement1,
                            Rate = term1.Rate,
                            DurationUnit = term1.DurationUnit,
                            AdvancedPaymentDurationValue = term1.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term1.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[0].Name,
                                    Description = term1.TermMiscellaneous[0].Description,
                                    Amount = term1.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[1].Name,
                                    Description = term1.TermMiscellaneous[1].Description,
                                    Amount = term1.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 14500,
                                    DateTimeReceived = contract1_accountStatement1.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 14000,
                                    DateTimeReceived = contract1_accountStatement1.AddDays(5),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term1.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term1.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term1.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term1.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract1_accountStatement2,
                            Rate = term1.Rate,
                            DurationUnit = term1.DurationUnit,
                            AdvancedPaymentDurationValue = term1.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term1.DepositPaymentDurationValue,
                            ElectricBill = 130,
                            WaterBill = 270,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[0].Name,
                                    Description = term1.TermMiscellaneous[0].Description,
                                    Amount = term1.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[1].Name,
                                    Description = term1.TermMiscellaneous[1].Description,
                                    Amount = term1.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 5450,
                                    DateTimeReceived = contract1_accountStatement2,
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 5450,
                                    DateTimeReceived = contract1_accountStatement2.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term1.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term1.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term1.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term1.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract1_accountStatement3,
                            Rate = term1.Rate,
                            DurationUnit = term1.DurationUnit,
                            AdvancedPaymentDurationValue = term1.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term1.DepositPaymentDurationValue,
                            ElectricBill = 135,
                            WaterBill = 265,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[0].Name,
                                    Description = term1.TermMiscellaneous[0].Description,
                                    Amount = term1.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term1.TermMiscellaneous[1].Name,
                                    Description = term1.TermMiscellaneous[1].Description,
                                    Amount = term1.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 10900,
                                    DateTimeReceived = contract1_accountStatement3,
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term1.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term1.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term1.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term1.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0002",
                TenantId = tenants[1].Id,
                TermId = term2.Id,
                EffectiveDate = contract2_effectiveDate,
                DurationValue = 0,
                DurationUnit = DurationEnum.None,
                IsOpenContract = true,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract2_accountStatement1,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3150,
                                    DateTimeReceived = contract2_accountStatement1,
                                    ReceivedById = accounts[1].Id
                                },
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract2_accountStatement2,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = term2.ElectricBillAmount,
                            WaterBill = term2.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract2_accountStatement2.AddDays(-1),
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract2_accountStatement2.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract2_accountStatement3,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = term2.ElectricBillAmount,
                            WaterBill = term2.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3550,
                                    DateTimeReceived = contract2_accountStatement3.AddDays(1),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0003",
                TenantId = tenants[2].Id,
                TermId = term3.Id,
                EffectiveDate = contract3_effectiveDate,
                DurationValue = 6,
                DurationUnit = DurationEnum.Month,
                IsOpenContract = false,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract3_accountStatement1,
                            Rate = term3.Rate,
                            DurationUnit = term3.DurationUnit,
                            AdvancedPaymentDurationValue = term3.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term3.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[0].Name,
                                    Description = term3.TermMiscellaneous[0].Description,
                                    Amount = term3.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[1].Name,
                                    Description = term3.TermMiscellaneous[1].Description,
                                    Amount = term3.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 13387.5f,
                                    DateTimeReceived = contract3_accountStatement1,
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 13387.5f,
                                    DateTimeReceived = contract3_accountStatement1.AddDays(4),
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 13387.5f,
                                    DateTimeReceived = contract3_accountStatement1.AddDays(7),
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 13387.5f,
                                    DateTimeReceived = contract3_accountStatement1.AddDays(5),
                                    ReceivedById = accounts[1].Id
                                },
                            },
                            PenaltyValue = term3.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term3.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term3.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term3.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract3_accountStatement2,
                            Rate = term3.Rate,
                            DurationUnit = term3.DurationUnit,
                            AdvancedPaymentDurationValue = term3.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term3.DepositPaymentDurationValue,
                            ElectricBill = 1200,
                            WaterBill = 1000,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[0].Name,
                                    Description = term3.TermMiscellaneous[0].Description,
                                    Amount = term3.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[1].Name,
                                    Description = term3.TermMiscellaneous[1].Description,
                                    Amount = term3.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 8375,
                                    DateTimeReceived = contract3_accountStatement2,
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 8375,
                                    DateTimeReceived = contract3_accountStatement2.AddDays(1),
                                    ReceivedById = accounts[1].Id
                                }
                            },
                            PenaltyValue = term3.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term3.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term3.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term3.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0004",
                TenantId = tenants[3].Id,
                TermId = term4.Id,
                EffectiveDate = contract4_effectiveDate,
                DurationValue = 4,
                DurationUnit = DurationEnum.Week,
                IsOpenContract = false,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract4_accountStatement1,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 9000,
                                    DateTimeReceived = contract4_accountStatement1,
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 9000,
                                    DateTimeReceived = contract4_accountStatement1.AddDays(5),
                                    ReceivedById = accounts[1].Id
                                }
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract4_accountStatement2,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = term4.ElectricBillAmount,
                            WaterBill = term4.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 6650,
                                    DateTimeReceived = contract4_accountStatement2,
                                    ReceivedById = accounts[1].Id
                                }
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract4_accountStatement3,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = term4.ElectricBillAmount,
                            WaterBill = term4.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 5000,
                                    DateTimeReceived = contract4_accountStatement3,
                                    ReceivedById = accounts[1].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 1550,
                                    DateTimeReceived = contract4_accountStatement3.AddDays(2),
                                    ReceivedById = accounts[1].Id
                                }
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0005",
                TenantId = tenants[4].Id,
                TermId = term2.Id,
                EffectiveDate = contract5_effectiveDate,
                DurationValue = 12,
                DurationUnit = DurationEnum.Month,
                IsOpenContract = false,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract5_accountStatement1,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 14500,
                                    DateTimeReceived = contract5_accountStatement1,
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 14000,
                                    DateTimeReceived = contract5_accountStatement1.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract5_accountStatement2,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = 130,
                            WaterBill = 270,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 5450,
                                    DateTimeReceived = contract5_accountStatement2,
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 5450,
                                    DateTimeReceived = contract5_accountStatement2.AddDays(3),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract5_accountStatement3,
                            Rate = term2.Rate,
                            DurationUnit = term2.DurationUnit,
                            AdvancedPaymentDurationValue = term2.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term2.DepositPaymentDurationValue,
                            ElectricBill = 135,
                            WaterBill = 265,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[0].Name,
                                    Description = term2.TermMiscellaneous[0].Description,
                                    Amount = term2.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term2.TermMiscellaneous[1].Name,
                                    Description = term2.TermMiscellaneous[1].Description,
                                    Amount = term2.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 10900,
                                    DateTimeReceived = contract5_accountStatement3.AddDays(1),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term2.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term2.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term2.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term2.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0006",
                TenantId = tenants[2].Id,
                TermId = term3.Id,
                EffectiveDate = contract6_effectiveDate,
                DurationValue = 0,
                DurationUnit = DurationEnum.None,
                IsOpenContract = true,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract6_accountStatement1,
                            Rate = term3.Rate,
                            DurationUnit = term3.DurationUnit,
                            AdvancedPaymentDurationValue = term3.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term3.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[0].Name,
                                    Description = term3.TermMiscellaneous[0].Description,
                                    Amount = term3.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[1].Name,
                                    Description = term3.TermMiscellaneous[1].Description,
                                    Amount = term3.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3150,
                                    DateTimeReceived = contract6_accountStatement1.AddDays(2),
                                    ReceivedById = accounts[1].Id
                                },
                            },
                            PenaltyValue = term3.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term3.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term3.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term3.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract6_accountStatement2,
                            Rate = term3.Rate,
                            DurationUnit = term3.DurationUnit,
                            AdvancedPaymentDurationValue = term3.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term3.DepositPaymentDurationValue,
                            ElectricBill = term3.ElectricBillAmount,
                            WaterBill = term3.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[0].Name,
                                    Description = term3.TermMiscellaneous[0].Description,
                                    Amount = term3.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[1].Name,
                                    Description = term3.TermMiscellaneous[1].Description,
                                    Amount = term3.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract6_accountStatement2.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract6_accountStatement2.AddDays(4),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term3.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term3.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term3.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term3.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract6_accountStatement3,
                            Rate = term3.Rate,
                            DurationUnit = term3.DurationUnit,
                            AdvancedPaymentDurationValue = term3.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term3.DepositPaymentDurationValue,
                            ElectricBill = term3.ElectricBillAmount,
                            WaterBill = term3.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous> {
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[0].Name,
                                    Description = term3.TermMiscellaneous[0].Description,
                                    Amount = term3.TermMiscellaneous[0].Amount
                                },
                                new AccountStatementMiscellaneous {
                                    Name = term3.TermMiscellaneous[1].Name,
                                    Description = term3.TermMiscellaneous[1].Description,
                                    Amount = term3.TermMiscellaneous[1].Amount
                                }
                            },
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3550,
                                    DateTimeReceived = contract6_accountStatement3.AddDays(1),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term3.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term3.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term3.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term3.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.Contracts.Add(new Contract
            {
                Code = "C-0007",
                TenantId = tenants[1].Id,
                TermId = term4.Id,
                EffectiveDate = contract7_effectiveDate,
                DurationValue = 0,
                DurationUnit = DurationEnum.None,
                IsOpenContract = true,
                AccountStatements = new List<AccountStatement>
                    {
                        new AccountStatement {
                            DueDate = contract7_accountStatement1,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = 0,
                            WaterBill = 0,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3150,
                                    DateTimeReceived = contract7_accountStatement1.AddDays(2),
                                    ReceivedById = accounts[1].Id
                                },
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract7_accountStatement2,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = term4.ElectricBillAmount,
                            WaterBill = term4.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract7_accountStatement2.AddDays(2),
                                    ReceivedById = accounts[0].Id
                                },
                                new PaymentBreakdown {
                                    Amount = 1775,
                                    DateTimeReceived = contract7_accountStatement2.AddDays(4),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        },

                        new AccountStatement {
                            DueDate = contract7_accountStatement3,
                            Rate = term4.Rate,
                            DurationUnit = term4.DurationUnit,
                            AdvancedPaymentDurationValue = term4.AdvancedPaymentDurationValue,
                            DepositPaymentDurationValue = term4.DepositPaymentDurationValue,
                            ElectricBill = term4.ElectricBillAmount,
                            WaterBill = term4.WaterBillAmount,
                            AccountStatementMiscellaneous = new List<AccountStatementMiscellaneous>(),
                            PaymentBreakdowns = new List<PaymentBreakdown>{
                                new PaymentBreakdown {
                                    Amount = 3550,
                                    DateTimeReceived = contract7_accountStatement3.AddDays(4),
                                    ReceivedById = accounts[0].Id
                                }
                            },
                            PenaltyValue = term4.PenaltyValue,
                            PenaltyAmountPerDurationUnit = term4.PenaltyAmountPerDurationUnit,
                            PenaltyEffectiveAfterDurationValue = term4.PenaltyEffectiveAfterDurationValue,
                            PenaltyEffectiveAfterDurationUnit = term4.PenaltyEffectiveAfterDurationUnit,
                            PenaltyBreakdowns = new List<PenaltyBreakdown>()
                        }
                    }
            });

            _context.SaveChanges();
        }
    }
    private void SeedSettings()
    {
        if (!_context.ClientSettings.Any())
        {
            _context.ClientSettings.Add(new ClientSettings
            {
                GenerateAccountStatementDaysBeforeValue = 3
            });

            _context.SaveChanges();
        }
    }

}
}