using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Helpers.Services;
using ReserbizAPP.LIB.Helpers.Services.StatementOfAccounts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AccountStatementRepository
        : BaseRepository<AccountStatement>, IAccountStatementRepository<AccountStatement>
    {
        private readonly IContractRepository<Contract> _contractRepository;

        private readonly IClientSettingsRepository<ClientSettings> _clientSettingsRepository;
        private readonly IPaymentBreakdownRepository<PaymentBreakdown> _paymentBreakdownRepository;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IOptions<EmailServerSettings> _emailServerSettings;
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly INotificationRepository<Notification> _notificationRepository;
        private readonly IUserNotificationRepository<UserNotification> _userNotificationRepository;
        private readonly IGenerateAccountStatementNotificationRepository<GeneratedAccountStatementNotification> _generateAccountStatementNotificationRepository;
        private readonly IAccountRepository<Account> _accountRepository;
        private readonly IOptions<SMSAPISettings> _smsApiSettings;

        public AccountStatementRepository(
            IReserbizRepository<AccountStatement> reserbizRepository,
            IContractRepository<Contract> contractRepository,
            IPaymentBreakdownRepository<PaymentBreakdown> paymentBreakdownRepository,
            IClientSettingsRepository<ClientSettings> clientSettingsRepository,
            IOptions<ApplicationSettings> appSettings,
            IOptions<EmailServerSettings> emailServerSettings,
            IOptions<SMSAPISettings> smsApiSettings,
            ITenantRepository<Tenant> tenantRepository,
            INotificationRepository<Notification> notificationRepository,
            IUserNotificationRepository<UserNotification> userNotificationRepository,
            IGenerateAccountStatementNotificationRepository<GeneratedAccountStatementNotification> generateAccountStatementNotificationRepository,
            IAccountRepository<Account> accountRepository) : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _appSettings = appSettings;
            _accountRepository = accountRepository;
            _emailServerSettings = emailServerSettings;
            _smsApiSettings = smsApiSettings;
            _paymentBreakdownRepository = paymentBreakdownRepository;
            _contractRepository = contractRepository;
            _clientSettingsRepository = clientSettingsRepository;
            _tenantRepository = tenantRepository;
            _notificationRepository = notificationRepository;
            _userNotificationRepository = userNotificationRepository;
            _generateAccountStatementNotificationRepository = generateAccountStatementNotificationRepository;
        }

        public AccountStatementRepository() : base()
        {

        }

        public async Task<AccountStatement> GetSuggestedNewAccountStatement(int contractId)
        {
            var contract = await _contractRepository
                            .GetEntity(contractId)
                            .Includes(
                                c => c.Term,
                                c => c.Term.TermMiscellaneous,
                                c => c.AccountStatements
                            )
                            .ToObjectAsync();

            var proposedAccountStatement = GenerateSuggesteAccountStatement(contract);

            proposedAccountStatement.Contract = contract;
            proposedAccountStatement.Contract.AccountStatements = contract.AccountStatements.Where(a => a.IsDelete == false).ToList();

            return proposedAccountStatement;
        }

        public PenaltyBreakdown RegisterNewPenaltyItem(AccountStatement accountStatement)
        {
            var newPenaltyItem = new PenaltyBreakdown
            {
                DueDate = accountStatement.PenaltyNextDueDate,
                Amount = accountStatement.PenaltyAmountValue
            };
            return newPenaltyItem;
        }

        public async Task<AccountStatement> GetAccountStatementAsync(int id)
        {
            var accountStatementFromRepo = await GetEntity(id)
                .Includes(
                    c => c.Contract,
                    c => c.Contract.AccountStatements,
                    c => c.AccountStatementMiscellaneous,
                    c => c.PaymentBreakdowns,
                    c => c.PenaltyBreakdowns
                )
                .ToObjectAsync();

            accountStatementFromRepo.LastDateSent = accountStatementFromRepo.LastDateSent.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone);

            return accountStatementFromRepo;
        }

        public async Task<IEnumerable<AccountStatement>> GetActiveAccountStatementsPerContractAsync(int contractId)
        {
            var activeAccountStatementsPerContractFromRepo = await _reserbizRepository.ClientDbContext.AccountStatements
                .AsQueryable()
                .Includes(
                    c => c.Contract,
                    c => c.Contract.AccountStatements,
                    c => c.AccountStatementMiscellaneous,
                    c => c.PaymentBreakdowns,
                    c => c.PenaltyBreakdowns
                )
                .Where(c =>
                   c.IsActive &&
                   c.IsDelete == false &&
                   c.ContractId == contractId
                )
                .OrderBy(c => c.DueDate)
                .ToListAsync();

            return activeAccountStatementsPerContractFromRepo;
        }

        public async Task<IEnumerable<AccountStatement>> GetUnpaidAccountStatementsAsync()
        {
            var accountStatements = await _reserbizRepository.ClientDbContext.AccountStatements
                .AsQueryable()
                .Includes(
                    c => c.Contract,
                    c => c.Contract.AccountStatements,
                    c => c.Contract.Tenant,
                    c => c.AccountStatementMiscellaneous,
                    c => c.PaymentBreakdowns,
                    c => c.PenaltyBreakdowns
                )
                .OrderBy(c => c.DueDate)
                .ToListAsync();

            var unpaidAccountStatements = accountStatements
                .Where(c =>
                   c.IsActive &&
                   c.IsDelete == false &&
                   c.IsFullyPaid == false
                ).ToList();

            return unpaidAccountStatements;
        }

        public async Task<IEnumerable<AccountStatement>> GetActiveDueAccountStatementsPerContractAsync(int contractId)
        {
            var activeAccountStatementsPerContractFromRepo = await _reserbizRepository.ClientDbContext.AccountStatements
                .AsQueryable()
                .Includes(
                    c => c.Contract,
                    c => c.Contract.AccountStatements,
                    c => c.AccountStatementMiscellaneous,
                    c => c.PaymentBreakdowns,
                    c => c.PenaltyBreakdowns
                )
                .Where(c =>
                   c.IsActive &&
                   c.ContractId == contractId
                )
                .ToListAsync();

            var activeDueAccountStatements = activeAccountStatementsPerContractFromRepo.Where(a => a.IsDueToGeneratePenalty).ToList();

            return activeDueAccountStatements;
        }

        public List<AccountStatement> GetFilteredAccountStatements(IList<AccountStatement> unfilteredAccountStatements, IAccountStatementFilter accountStatementFilter)
        {
            var filteredAccountStatements = unfilteredAccountStatements;

            // Filter account statements where the filter FromDate should 
            // be less than or equal to account statement Due Date
            if (accountStatementFilter.FromDate != DateTime.MinValue)
            {
                filteredAccountStatements = filteredAccountStatements.Where(c => !(accountStatementFilter.FromDate > c.DueDate)).ToList();
            }

            // Filter account statements where the filter ToDate should 
            // be greater than or equal to account statement Due Date
            if (accountStatementFilter.ToDate != DateTime.MinValue)
            {
                filteredAccountStatements = filteredAccountStatements.Where(c => !(accountStatementFilter.ToDate < c.DueDate)).ToList();
            }

            // Filter open contracts 
            if (accountStatementFilter.PaymentStatus != PaymentStatusEnum.All)
            {
                var isPaid = (accountStatementFilter.PaymentStatus == PaymentStatusEnum.Paid);
                filteredAccountStatements = filteredAccountStatements.Where(c => c.IsFullyPaid == isPaid).ToList();
            }

            // Filter based on account statement type
            if (accountStatementFilter.AccountStatementType != AccountStatementTypeEnum.All)
            {
                filteredAccountStatements = filteredAccountStatements.Where(c => c.AccountStatementType == accountStatementFilter.AccountStatementType).ToList();
            }

            // Set sort order based on due date
            // Sort order is ascending by default
            if (accountStatementFilter.SortOrder == SortOrderEnum.Ascending)
            {
                return filteredAccountStatements
                    .OrderBy(c => c.DueDate)
                    .ToList();
            }
            else
            {
                return filteredAccountStatements
                    .OrderByDescending(c => c.DueDate)
                    .ToList();
            }
        }

        public async Task GenerateContractAccountStatementsForRentalBill(string dbHashName, int contractId, SendAccountStatementModeEnum sendAccountStatementMode)
        {
            var listOfNewAccountStatements = new List<AccountStatement>();
            var contract = await _contractRepository
                .GetEntity(contractId)
                .Includes(
                    c => c.Tenant,
                    c => c.Term,
                    c => c.Term.TermMiscellaneous,
                    c => c.AccountStatements
                )
                .ToObjectAsync();

            if (contract == null) return;

            while (contract.IsDueForGeneratingAccountStatement)
            {
                // Make sure to only checks any statement of account that is set to Rental Bills.
                var isAccountStatementExists = (await _reserbizRepository.ClientDbContext.AccountStatements
                    .Where(
                        a => a.ContractId == contract.Id &&
                        a.AccountStatementType == AccountStatementTypeEnum.RentalBill &&
                        a.DueDate == contract.NextDueDate &&
                        a.IsDelete == false &&
                        a.IsActive
                    )
                    .FirstOrDefaultAsync() != null);

                // Its important to check if statement of account date already
                // exists on list of statement of accounts to avoid any duplication
                if (isAccountStatementExists == false)
                {
                    // (1) If the contract is about to generate its first account statement,
                    // it should generate account statements based on the number of AdvancedPaymentDurationValue.
                    // Eg. If the AdvancedPaymentDurationValue = 3, then we should generate 3 account statements for the contract.
                    if (contract.AccountStatements.Count == 0)
                    {
                        for (var idx = 0; idx < contract.Term.AdvancedPaymentDurationValue; idx++)
                        {
                            var newContractAccountStatement = RegisterNewAccountStatementForRentalBill(contract);
                            contract.AccountStatements.Add(newContractAccountStatement);
                            listOfNewAccountStatements.Add(newContractAccountStatement);
                        }
                    }

                    // (2) If the contract is about to generate account statement that is not the first, 
                    // then generate only single account statement.
                    else
                    {
                        var newContractAccountStatement = RegisterNewAccountStatementForRentalBill(contract);
                        contract.AccountStatements.Add(newContractAccountStatement);
                        listOfNewAccountStatements.Add(newContractAccountStatement);
                    }
                }
            }

            try
            {
                await _contractRepository.SaveChanges();

                // Send push notifications on the device subscribing to db hash name topic
                await SendPushNotifications(dbHashName, contract, listOfNewAccountStatements, sendAccountStatementMode);
            }
            catch (Exception ex)
            {
                throw new Exception($"Registering new statement of accounts for contract with id={contract.Id} failed on save. Error message: {ex.Message}.", ex);
            }
        }

        public async Task GenerateContractAccountStatementsRentalBillForNewDatabase(int contractId, int currentUserId)
        {
            var contract = await _contractRepository
                .GetEntity(contractId)
                .Includes(
                    c => c.Term,
                    c => c.Term.TermMiscellaneous,
                    c => c.AccountStatements
                )
                .ToObjectAsync();

            if (contract == null) return;

            try
            {
                while (contract.IsDueForGeneratingAccountStatement)
                {
                    var isAccountStatementExists = (await _reserbizRepository.ClientDbContext.AccountStatements
                        .Where(
                            a => a.ContractId == contract.Id &&
                            a.DueDate == contract.NextDueDate &&
                            a.AccountStatementType == AccountStatementTypeEnum.RentalBill &&
                            a.IsDelete == false &&
                            a.IsActive
                        )
                        .FirstOrDefaultAsync() != null);

                    // Its important to check if statement of account date already
                    // exists on list of statement of accounts to avoid any duplication
                    if (isAccountStatementExists == false)
                    {
                        // (1) If the contract is about to generate its first account statement,
                        // it should generate account statements based on the number of AdvancedPaymentDurationValue.
                        // Eg. If the AdvancedPaymentDurationValue = 3, then we should generate 3 account statements for the contract.
                        if (contract.AccountStatements.Count == 0)
                        {
                            for (var idx = 0; idx < contract.Term.AdvancedPaymentDurationValue; idx++)
                            {
                                // Insert new statement of account for rental bill
                                var newContractAccountStatementForRentalBill = RegisterNewAccountStatementForRentalBill(contract);
                                contract.AccountStatements.Add(newContractAccountStatementForRentalBill);
                                newContractAccountStatementForRentalBill.Contract = contract;
                                RegisterPayments(newContractAccountStatementForRentalBill, currentUserId);

                                await _contractRepository.SaveChanges();

                                // Insert new statement of account for utility bill
                                var newContractAccountStatementForUtilityBill = await RegisterNewAccountStatementForUtilityBill(contract, newContractAccountStatementForRentalBill.DueDate);
                                if (newContractAccountStatementForUtilityBill != null)
                                {
                                    contract.AccountStatements.Add(newContractAccountStatementForUtilityBill);
                                    newContractAccountStatementForUtilityBill.Contract = contract;
                                    RegisterPayments(newContractAccountStatementForUtilityBill, currentUserId);
                                }

                                await _contractRepository.SaveChanges();
                            }
                        }

                        // (2) If the contract is about to generate account statement that is not the first, 
                        // then generate only single account statement.
                        else
                        {
                            // Insert new statement of account for rental bill
                            var newContractAccountStatementForRentalBill = RegisterNewAccountStatementForRentalBill(contract);
                            contract.AccountStatements.Add(newContractAccountStatementForRentalBill);
                            newContractAccountStatementForRentalBill.Contract = contract;
                            RegisterPayments(newContractAccountStatementForRentalBill, currentUserId);

                            await _contractRepository.SaveChanges();

                            // Insert new statement of account for utility bill
                            var newContractAccountStatementForUtilityBill = await RegisterNewAccountStatementForUtilityBill(contract, newContractAccountStatementForRentalBill.DueDate);
                            if (newContractAccountStatementForUtilityBill != null)
                            {
                                contract.AccountStatements.Add(newContractAccountStatementForUtilityBill);
                                newContractAccountStatementForUtilityBill.Contract = contract;
                                RegisterPayments(newContractAccountStatementForUtilityBill, currentUserId);
                            }

                            await _contractRepository.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Registering new statement of accounts for contract with id={contract.Id} failed on save. Error message: {ex.Message}.", ex);
            }
        }

        public async Task GenerateNewContractAccountStatement(AccountStatement accountStatement, bool markAsPaid, int currentUserId)
        {
            var contract = await _contractRepository
                    .GetEntity(accountStatement.ContractId)
                    .Includes(
                        c => c.Term,
                        c => c.Term.TermMiscellaneous,
                        c => c.AccountStatements
                    )
                    .ToObjectAsync();

            if (contract == null) return;

            _contractRepository.SetCurrentUserId(currentUserId);

            try
            {
                var statementOfAccountServiceTypeName = Enum.GetName(typeof(AccountStatementTypeEnum), accountStatement.AccountStatementType);
                var className = $"ReserbizAPP.LIB.Helpers.Services.StatementOfAccounts.{statementOfAccountServiceTypeName}StatementOfAccountService";
                var constructedType = Type.GetType(className);

                var statementOfAccountTypeInstance = (IStatementOfAccountService)Activator.CreateInstance(constructedType);

                var newContractAccountStatement = statementOfAccountTypeInstance.GenerateStatementOfAccount(contract, accountStatement);

                // Mark as Paid, Add entry on payment breakdown 
                // with the same amount of statement of account
                // total amount
                if (markAsPaid)
                {
                    newContractAccountStatement.Contract = contract;

                    RegisterPayments(newContractAccountStatement, currentUserId);
                }

                contract.AccountStatements.Add(newContractAccountStatement);

                await _contractRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Registering new statement of accounts for contract with id={contract.Id} failed on save. Error message: {ex.Message}.", ex);
            }
        }

        public async Task GenerateAccountStatementPenalties(int contractId)
        {
            var activeContractAccountStatements = await GetActiveAccountStatementsPerContractAsync(contractId);

            foreach (var accountStatement in activeContractAccountStatements)
            {
                // Check of the penalty setting is active.
                if (!accountStatement.IsPenaltySettingActive) continue;

                // Only generate penalties if it is valid to do so.
                while (accountStatement.IsValidForGeneratingPenalty)
                {
                    var newPenaltyItem = RegisterNewPenaltyItem(accountStatement);
                    accountStatement.PenaltyBreakdowns.Add(newPenaltyItem);
                }

                try
                {
                    await SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Registering new penalty details for statement of account with id={accountStatement.Id} failed on save. Error messsage: {ex.Message}.", ex);
                }
            }
        }

        public async Task<AccountStatement> GetFirstAccountStatement(int contractId)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId)
                .Includes(c => c.AccountStatements)
                .ToObjectAsync();

            var accountStatements = contractFromRepo.AccountStatements
                .OrderBy(a => a.DueDate)
                .ToList();

            var accountStatementToReturn = accountStatements.Count > 0 ? accountStatements[0] : null;

            if (accountStatementToReturn != null)
            {
                accountStatementToReturn = await GetEntity(accountStatementToReturn.Id)
                    .Includes(
                        a => a.PaymentBreakdowns,
                        a => a.AccountStatementMiscellaneous,
                        a => a.PenaltyBreakdowns
                    )
                    .ToObjectAsync();
            }

            return accountStatementToReturn;
        }

        public float CalculateTotalAmountPaid(IEnumerable<PaymentBreakdown> paymentBreakdowns)
        {
            return paymentBreakdowns.Where(m => m.IsAmountFromDeposit == false)
                .Select(m => m.Amount)
                .Sum();
        }

        public float CalculateTotalAmountPaidUsingDeposit(IEnumerable<PaymentBreakdown> paymentBreakdowns)
        {
            return paymentBreakdowns.Where(m => m.IsAmountFromDeposit)
                .Select(m => m.Amount)
                .Sum();
        }

        public async Task<float> CalculateOverAllPaymentUsedFromDepositedAmount(int contractId)
        {
            var accountStatementJoinedPayments = await (from accountStatement in _reserbizRepository.ClientDbContext.AccountStatements
                                                        join paymentBreakdown in _reserbizRepository.ClientDbContext.PaymentBreakdowns on accountStatement.Id equals paymentBreakdown.AccountStatementId

                                                        where accountStatement.ContractId == contractId &&
                                                        paymentBreakdown.IsAmountFromDeposit == true

                                                        select new
                                                        {
                                                            paymentBreakdown
                                                        }).ToListAsync();

            var overAllPaymentsUsedFromDepositedAmount = accountStatementJoinedPayments.Sum(a => a.paymentBreakdown.Amount);

            return overAllPaymentsUsedFromDepositedAmount;
        }

        public async Task<double> CalculatedDepositedAmountBalance(int contractId, AccountStatement firstAccountStatement)
        {
            var overAllPaymentsUsedFromDepositedAmount = await CalculateOverAllPaymentUsedFromDepositedAmount(contractId);

            return firstAccountStatement.CalculatedDepositAmount - overAllPaymentsUsedFromDepositedAmount;
        }

        public double CalculatedSuggestedAmountForRentalPayment(AccountStatement currentAccountStatement, double depositedAmountBalance)
        {
            var paidRentalAmount = currentAccountStatement.TotalPaidRentalAmount;

            var remainingUnpaidRentalAmount = currentAccountStatement.Rate - paidRentalAmount;

            return remainingUnpaidRentalAmount <= depositedAmountBalance ? remainingUnpaidRentalAmount : depositedAmountBalance;
        }

        public double CalculateSuggestedAmountForElectricBill(AccountStatement currentAccountStatement, double depositedAmountBalance)
        {
            var paidElectricBillAmount = currentAccountStatement.TotalPaidElectricBills;

            var remainingUnpaidElectricBillAmount = currentAccountStatement.ElectricBill - paidElectricBillAmount;

            return remainingUnpaidElectricBillAmount <= depositedAmountBalance ? remainingUnpaidElectricBillAmount : depositedAmountBalance;
        }

        public double CalculateSuggestedAmountForWaterBill(AccountStatement currentAccountStatement, double depositedAmountBalance)
        {
            var paidWaterBillAmount = currentAccountStatement.TotalPaidWaterBills;

            var remainingUnpaidWaterBillAmount = currentAccountStatement.WaterBill - paidWaterBillAmount;

            return remainingUnpaidWaterBillAmount <= depositedAmountBalance ? remainingUnpaidWaterBillAmount : depositedAmountBalance;
        }

        public double CalculateSuggestedAmountForMiscellaneousFees(AccountStatement currentAccountStatement, double depositedAmountBalance)
        {
            var paidMiscellaneousFeesAmount = currentAccountStatement.TotalPaidMiscellaneousFees;

            var remainingUnpaidMiscellaneousFees = currentAccountStatement.MiscellaneousTotalAmount - paidMiscellaneousFeesAmount;

            return remainingUnpaidMiscellaneousFees <= depositedAmountBalance ? remainingUnpaidMiscellaneousFees : depositedAmountBalance;
        }

        public double CalculateSuggestedAmountForPenaltyAmount(AccountStatement currentAccountStatement, double depositedAmountBalance)
        {
            var paidPenaltyAmount = currentAccountStatement.TotalPaidPenaltyAmount;

            var remainingUnpaidPenaltyAmount = currentAccountStatement.PenaltyTotalAmount - paidPenaltyAmount;

            return remainingUnpaidPenaltyAmount <= depositedAmountBalance ? remainingUnpaidPenaltyAmount : depositedAmountBalance;
        }

        public async Task<AccountStatementsAmountSummary> GetAccountStatementsAmountSummary()
        {
            // Get all account statements
            var accountStatements = await _reserbizRepository.ClientDbContext.AccountStatements
                .AsQueryable()
                .Includes(
                    c => c.Contract,
                    c => c.Contract.AccountStatements,
                    c => c.AccountStatementMiscellaneous,
                    c => c.PaymentBreakdowns,
                    c => c.PenaltyBreakdowns
                )
                .OrderByDescending(c => c.DueDate)
                .ToListAsync();

            // Total Paid Amount 
            var totalAmountPaid = accountStatements
                .Where(c =>
                   c.IsActive &&
                   c.IsDelete == false
                ).Sum(a => a.CurrentAmountPaid);

            // Total Unpaid Amount 
            var totalExpectedAmount = accountStatements
                .Where(c =>
                   c.IsActive &&
                   c.IsDelete == false
                ).Sum(a => a.AccountStatementTotalAmount);

            var totalUnpaidAmount = Math.Abs(totalAmountPaid - totalExpectedAmount);

            var amountSummary = new AccountStatementsAmountSummary
            {
                TotalAmountPaid = totalAmountPaid,
                TotalUnpaidAmount = totalUnpaidAmount
            };

            return amountSummary;
        }

        public async Task SendAccountStatement(int id)
        {
            var accountStatement = await GetAccountStatementAsync(id);
            var tenant = await _tenantRepository.GetTenantAsync(accountStatement.Contract.TenantId);

            try
            {
                // Send account statement details via email
                await SendAccountStatementAsEmail(accountStatement, tenant);

                // Send account statement details via SMS
                await SendAccountStatementAsSMS(accountStatement, tenant);

                accountStatement.LastDateSent = DateTime.Now;

                await _accountRepository.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private AccountStatement RegisterNewAccountStatementForRentalBill(Contract contract)
        {
            var rentalBillStatementOfAccountService = new RentalBillStatementOfAccountService();
            var newAccountStatement = rentalBillStatementOfAccountService.GenerateStatementOfAccount(contract, new AccountStatement());
            return newAccountStatement;
        }

        private async Task<AccountStatement> RegisterNewAccountStatementForUtilityBill(Contract contract, DateTime dueDate)
        {
            var accountStatementTemplate = await GetSuggestedNewAccountStatement(contract.Id);

            if (accountStatementTemplate.ExcludeElectricBill && accountStatementTemplate.ElectricBill > 0 &&
                accountStatementTemplate.ExcludeWaterBill && accountStatementTemplate.WaterBill > 0)
            {
                var utilityBillStatementOfAccountService = new UtilityBillStatementOfAccountService();
                var newUtilityBillAccountStatement = new AccountStatement();

                newUtilityBillAccountStatement.DueDate = dueDate.GetLastDayOfMonth();
                newUtilityBillAccountStatement.WaterBill = accountStatementTemplate.WaterBill;
                newUtilityBillAccountStatement.ElectricBill = accountStatementTemplate.ElectricBill;
                newUtilityBillAccountStatement.ExcludeElectricBill = accountStatementTemplate.ExcludeElectricBill;
                newUtilityBillAccountStatement.ExcludeWaterBill = accountStatementTemplate.ExcludeWaterBill;

                var newAccountStatement = utilityBillStatementOfAccountService.GenerateStatementOfAccount(contract, newUtilityBillAccountStatement);
                return newAccountStatement;
            }

            return null;
        }

        private List<AccountStatementMiscellaneous> GenerateAccountStatementMiscellaneous(List<TermMiscellaneous> termMiscellaneous)
        {
            var accountTermMiscellaneous = new List<AccountStatementMiscellaneous>();

            // Get only active term miscellaneous
            foreach (var item in termMiscellaneous.Where(t => t.IsActive == true && t.IsDelete == false))
            {
                var newTermMiscellaneous = new AccountStatementMiscellaneous();
                newTermMiscellaneous.Name = item.Name;
                newTermMiscellaneous.Description = item.Description;
                newTermMiscellaneous.Amount = item.Amount;
                accountTermMiscellaneous.Add(newTermMiscellaneous);
            }
            return accountTermMiscellaneous;
        }

        private AccountStatement GenerateSuggesteAccountStatement(Contract contract)
        {
            var contractTerm = contract.Term;

            // Value for electric and water bill will be 0 for the 
            // first account statement and will be based on term
            // for the succeeding generation of account statements.
            var waterBill = contract.AccountStatements.Count > 0 ? contractTerm.WaterBillAmount : 0;
            var electricBill = contract.AccountStatements.Count > 0 ? contractTerm.ElectricBillAmount : 0;

            var newAccountStatement = new AccountStatement
            {
                DueDate = contract.NextDueDate,
                Rate = contractTerm.Rate,
                DurationUnit = contractTerm.DurationUnit,
                ElectricBill = electricBill,
                WaterBill = waterBill,
                ExcludeElectricBill = contractTerm.ExcludeElectricBill,
                ExcludeWaterBill = contractTerm.ExcludeWaterBill,
                PenaltyValue = contractTerm.PenaltyValue,
                AdvancedPaymentDurationValue = contractTerm.AdvancedPaymentDurationValue,
                DepositPaymentDurationValue = contractTerm.DepositPaymentDurationValue,
                PenaltyValueType = contractTerm.PenaltyValueType,
                PenaltyAmountPerDurationUnit = contractTerm.PenaltyAmountPerDurationUnit,
                PenaltyEffectiveAfterDurationValue = contractTerm.PenaltyEffectiveAfterDurationValue,
                PenaltyEffectiveAfterDurationUnit = contractTerm.PenaltyEffectiveAfterDurationUnit,
                MiscellaneousDueDate = contractTerm.MiscellaneousDueDate
            };
            newAccountStatement.AccountStatementMiscellaneous.AddRange(GenerateAccountStatementMiscellaneous(contractTerm.TermMiscellaneous));
            return newAccountStatement;
        }

        private async Task SendAccountStatementAsSMS(AccountStatement accountStatement, Tenant tenant)
        {
            // Make sure to only send sms once in a day.
            if (accountStatement.AllowSentSMSNotificationForTheDay)
            {
                var contentSection = GenerateAccountStatementSMSContentSection(accountStatement);
                var smsContent = await GenerateAccountStatementNotificationContent(accountStatement, tenant, contentSection, _appSettings.Value.AccountStatementNotificationSettings.SMSNotificationTemplate);

                // Need to append "63" on the contact number in order to send the message
                var contactNumber = String.Format("63{0}", tenant.ContactNumber.Remove(0, 1));

                try
                {
                    var smsService = new SMSService(_smsApiSettings.Value.API_KEY, _smsApiSettings.Value.API_SECRET);
                    await smsService.SendMessage(_smsApiSettings.Value.SMS_BRAND_NAME, contactNumber, smsContent);

                    accountStatement.SMSNotificationLastDateSent = DateTime.Now.AddDays(1);

                    await SaveChanges();

                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
        }

        private async Task SendAccountStatementAsEmail(AccountStatement accountStatement, Tenant tenant)
        {
            var contentSection = GenerateAccountStatementEmailContentSection(accountStatement);
            var emailContent = await GenerateAccountStatementNotificationContent(accountStatement, tenant, contentSection, _appSettings.Value.AccountStatementNotificationSettings.EmailNotificationTemplate);
            var subject = "Statement of Account";

            try
            {
                var emailService = new EmailService(
                                _emailServerSettings.Value.SmtpServer,
                                _appSettings.Value.AccountStatementNotificationSettings.SenderEmailAddress,
                                _emailServerSettings.Value.SmtpPassword,
                                _emailServerSettings.Value.SmtpPort
                            );

                emailService.Send(
                    _appSettings.Value.AccountStatementNotificationSettings.SenderEmailAddress,
                    tenant.EmailAddress,
                    subject,
                    emailContent,
                    _appSettings.Value.AccountStatementNotificationSettings.BCCEmailAddress
                );
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private async Task<string> GenerateAccountStatementNotificationContent(AccountStatement accountStatement, Tenant tenant, string content, string templatePath)
        {
            var template = "";
            var clientSettings = await _clientSettingsRepository.GetClientSettings();

            using (var rdFile = new StreamReader(String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, templatePath)))
            {
                template = rdFile.ReadToEnd();
            }

            template = template.Replace("#tenantName", tenant.PersonFullName);
            template = template.Replace("#statementofaccounts", content);
            template = template.Replace("#organizationname", clientSettings.BusinessName);

            return template;
        }

        private string GenerateAccountStatementEmailContentSection(AccountStatement accountStatement)
        {
            var content = new StringBuilder();

            #region Rental fee section
            // Check if the statement of account is for rental bill
            if (accountStatement.AccountStatementType == AccountStatementTypeEnum.RentalBill)
            {
                content.AppendLine(String.Format("<b>Due Date:</b> {0}<br>", accountStatement.DueDate.ToString("MM/dd/yyyy")));

                var totalAmount = 0.0f;
                var rentalFee = accountStatement.Rate;
                if (accountStatement.IsFirstAccountStatement)
                {
                    rentalFee += accountStatement.Rate * accountStatement.DepositPaymentDurationValue;
                }
                content.AppendLine(String.Format("<b>Rental Fee:</b> {0}<br>", rentalFee.ToCurrencyFormat()));
                totalAmount += rentalFee;

                if (accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
                {
                    // Include miscellaneous if Miscellaneous Due Date is Same with rental due date
                    if (accountStatement.AccountStatementMiscellaneous.Count > 0)
                    {
                        content.AppendLine("<b>Miscellaneous Fees:</b><br>");
                        foreach (AccountStatementMiscellaneous item in accountStatement.AccountStatementMiscellaneous)
                        {
                            content.AppendLine(String.Format("{0}: {1}<br>", item.Name, item.Amount.ToCurrencyFormat()));
                            totalAmount += item.Amount;
                        }
                    }
                }

                // Append Penalty amount
                if (accountStatement.PenaltyTotalAmount > 0)
                {
                    content.AppendLine(String.Format("<b>Penalties Amount:</b> {0}<br>", accountStatement.PenaltyTotalAmount.ToCurrencyFormat()));
                    totalAmount += accountStatement.PenaltyTotalAmount;
                }

                if ((accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate && accountStatement.AccountStatementMiscellaneous.Count > 0) || accountStatement.PenaltyTotalAmount > 0)
                {
                    content.AppendLine(String.Format("<b>Total Amount:</b> {0}<br>", totalAmount.ToCurrencyFormat()));
                }

                content.AppendLine("<br>");
            }
            #endregion

            #region Utility bills section
            // Check if the statement of account is for utility bill
            if (accountStatement.AccountStatementType == AccountStatementTypeEnum.UtilityBill)
            {
                content.AppendLine(String.Format("<b>Due Date:</b> {0}<br>", accountStatement.DueDate.ToString("MM/dd/yyyy")));
                var totalAmount = 0.0f;

                if (accountStatement.ExcludeWaterBill && accountStatement.WaterBill > 0)
                {
                    content.AppendLine(String.Format("<b>Electric Bill Amount:</b> {0}<br>", accountStatement.ElectricBill.ToCurrencyFormat()));
                    totalAmount += accountStatement.ElectricBill;
                }

                if (accountStatement.ExcludeElectricBill && accountStatement.ElectricBill > 0)
                {
                    content.AppendLine(String.Format("<b>Water Bill Amount:</b> {0}<br>", accountStatement.WaterBill.ToCurrencyFormat()));
                    totalAmount += accountStatement.WaterBill;
                }

                if (accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithUtilityBillDueDate)
                {
                    // Include miscellaneous if Miscellaneous Due Date is Same with utility bills due date
                    if (accountStatement.AccountStatementMiscellaneous.Count > 0)
                    {
                        content.AppendLine("<b>Miscellaneous Fees:</b><br>");
                        foreach (AccountStatementMiscellaneous item in accountStatement.AccountStatementMiscellaneous)
                        {
                            content.AppendLine(String.Format("{0}: {1}<br>", item.Name, item.Amount.ToCurrencyFormat()));
                            totalAmount += item.Amount;
                        }
                    }
                }

                content.AppendLine(String.Format("<b>Total Amount:</b> {0}<br>", totalAmount.ToCurrencyFormat()));
                content.AppendLine("<br>");
            }
            #endregion

            return content.ToString();
        }

        private string GenerateAccountStatementSMSContentSection(AccountStatement accountStatement)
        {
            var content = new StringBuilder();

            #region  Rental fee section
            // Check if the statement of account is for rental bill
            if (accountStatement.AccountStatementType == AccountStatementTypeEnum.RentalBill)
            {
                content.Append("\n");
                content.Append(String.Format("Due Date: {0} \n", accountStatement.DueDate.ToString("MM/dd/yyyy")));

                var totalAmount = 0.0f;
                var rentalFee = accountStatement.Rate;
                if (accountStatement.IsFirstAccountStatement)
                {
                    rentalFee += accountStatement.Rate * accountStatement.DepositPaymentDurationValue;
                }
                content.Append(String.Format("Rental Fee: {0} \n", rentalFee.ToCurrencyFormat()));
                totalAmount += rentalFee;

                if (accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate)
                {
                    // Include miscellaneous if Miscellaneous Due Date is Same with rental due date
                    if (accountStatement.AccountStatementMiscellaneous.Count > 0)
                    {
                        content.Append("Miscellaneous Fees: \n");
                        foreach (AccountStatementMiscellaneous item in accountStatement.AccountStatementMiscellaneous)
                        {
                            content.Append(String.Format("{0}: {1} \n", item.Name, item.Amount.ToCurrencyFormat()));
                            totalAmount += item.Amount;
                        }
                    }
                }

                // Append Penalty amount
                if (accountStatement.PenaltyTotalAmount > 0)
                {
                    content.Append(String.Format("Penalties Amount: {0} \n", accountStatement.PenaltyTotalAmount.ToCurrencyFormat()));
                    totalAmount += accountStatement.PenaltyTotalAmount;
                }

                if ((accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithRentalDueDate && accountStatement.AccountStatementMiscellaneous.Count > 0) || accountStatement.PenaltyTotalAmount > 0)
                {
                    content.Append(String.Format("Total Amount: {0} \n", totalAmount.ToCurrencyFormat()));
                }
            }
            #endregion


            #region Utility bills section
            // Check if the statement of account is for utility bill
            if (accountStatement.AccountStatementType == AccountStatementTypeEnum.UtilityBill)
            {
                content.Append("\n");
                content.Append(String.Format("Due Date: {0} \n", accountStatement.DueDate.ToString("MM/dd/yyyy")));
                var totalAmount = 0.0f;

                if (accountStatement.ExcludeWaterBill && accountStatement.WaterBill > 0)
                {
                    content.Append(String.Format("Electric Bill Amount: {0} \n", accountStatement.ElectricBill.ToCurrencyFormat()));
                    totalAmount += accountStatement.ElectricBill;
                }

                if (accountStatement.ExcludeElectricBill && accountStatement.ElectricBill > 0)
                {
                    content.Append(String.Format("Water Bill Amount: {0} \n", accountStatement.WaterBill.ToCurrencyFormat()));
                    totalAmount += accountStatement.WaterBill;
                }

                if (accountStatement.MiscellaneousDueDate == MiscellaneousDueDateEnum.SameWithUtilityBillDueDate)
                {
                    // Include miscellaneous if Miscellaneous Due Date is Same with utility bills due date
                    if (accountStatement.AccountStatementMiscellaneous.Count > 0)
                    {
                        content.Append("Miscellaneous Fees: \n");
                        foreach (AccountStatementMiscellaneous item in accountStatement.AccountStatementMiscellaneous)
                        {
                            content.Append(String.Format("{0}: {1} \n", item.Name, item.Amount.ToCurrencyFormat()));
                            totalAmount += item.Amount;
                        }
                    }
                }

                content.Append(String.Format("Total Amount: {0} \n", totalAmount.ToCurrencyFormat()));
                content.Append("\n");
            }
            #endregion

            return content.ToString();
        }

        private async Task SendPushNotifications(string dbHashName, Contract contract, List<AccountStatement> newAccountStatements, SendAccountStatementModeEnum sendAccountStatementMode)
        {

            var notificationTitle = "New Statement of Account";
            var fireBasePushNotificationService = new FireBasePushNotificationService(dbHashName);
            foreach (var accountStatement in newAccountStatements)
            {
                try
                {
                    var accountStatementDate = accountStatement.DueDate.ToString("MM/dd/yyyy");
                    var notificationBody = $"New statement of account generated for customer {contract.Tenant.PersonFullName} for {accountStatementDate} ({contract.Code})";
                    var data = new Dictionary<string, string>();

                    data.Add("url", $"contracts/{contract.Id}/account-statement/{accountStatement.Id}");

                    // Broacast notification
                    await fireBasePushNotificationService.Send(notificationTitle, notificationBody, data);

                    // Register notification on database
                    await RegisterNotification(accountStatementDate, accountStatement.Id, contract.Id, contract.TenantId);

                    // Auto-send statement of account details
                    if (sendAccountStatementMode == SendAccountStatementModeEnum.Automatic && accountStatement.AutoSendNewAccountStatement)
                    {
                        await SendAccountStatement(accountStatement.Id);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error when sending push notification for client {dbHashName}. Error Message {ex.Message}");
                }
            }
        }

        private void RegisterPayments(AccountStatement newContractAccountStatement, int currentUserId)
        {
            if (newContractAccountStatement.RentalTotalAmount > 0)
            {
                var paymentForRental = new PaymentBreakdown()
                {
                    Amount = newContractAccountStatement.RentalTotalAmount,
                    ReceivedById = currentUserId,
                    DateTimeReceived = DateTime.Now,
                    Notes = String.Empty,
                    IsAmountFromDeposit = false,
                    PaymentForType = PaymentForTypeEnum.Rental
                };

                newContractAccountStatement.PaymentBreakdowns.Add(paymentForRental);
            }

            if (newContractAccountStatement.ElectricBill > 0)
            {
                var paymentForElectricBill = new PaymentBreakdown()
                {
                    Amount = newContractAccountStatement.ElectricBill,
                    ReceivedById = currentUserId,
                    DateTimeReceived = DateTime.Now,
                    Notes = String.Empty,
                    IsAmountFromDeposit = false,
                    PaymentForType = PaymentForTypeEnum.ElectricBill
                };

                newContractAccountStatement.PaymentBreakdowns.Add(paymentForElectricBill);
            }

            if (newContractAccountStatement.WaterBill > 0)
            {
                var paymentForWaterBill = new PaymentBreakdown()
                {
                    Amount = newContractAccountStatement.WaterBill,
                    ReceivedById = currentUserId,
                    DateTimeReceived = DateTime.Now,
                    Notes = String.Empty,
                    IsAmountFromDeposit = false,
                    PaymentForType = PaymentForTypeEnum.WaterBill
                };

                newContractAccountStatement.PaymentBreakdowns.Add(paymentForWaterBill);
            }

            if (newContractAccountStatement.MiscellaneousTotalAmount > 0)
            {
                var paymentForMiscellaneousFees = new PaymentBreakdown()
                {
                    Amount = newContractAccountStatement.MiscellaneousTotalAmount,
                    ReceivedById = currentUserId,
                    DateTimeReceived = DateTime.Now,
                    Notes = String.Empty,
                    IsAmountFromDeposit = false,
                    PaymentForType = PaymentForTypeEnum.MiscellaneousFee
                };

                newContractAccountStatement.PaymentBreakdowns.Add(paymentForMiscellaneousFees);
            }
        }

        private async Task RegisterNotification(string accountStatementDate, int accountStatementId, int contractId, int tenantId)
        {
            var accounts = await _accountRepository.GetAllActiveUsers();
            var accountStatementNotification = new GeneratedAccountStatementNotification
            {
                AccountStatementDateTime = Convert.ToDateTime(accountStatementDate),
                AccountStatementId = accountStatementId,
                ContractId = contractId,
                TenantId = tenantId
            };

            var accountStatementNotificationService = new AccountStatementNotificationService(_generateAccountStatementNotificationRepository, accountStatementNotification);
            var notificationId = await _notificationRepository.Register(accountStatementNotificationService, 0, NotificationFromTypeEnum.ReserbizSystem);

            // We will only send the notification to all active administrator accounts
            foreach (var account in accounts)
            {
                try
                {
                    var userNotification = new UserNotification
                    {
                        NotificationId = notificationId,
                        UserId = account.Id,
                        UserType = UserTypeEnum.Administrator
                    };

                    await _userNotificationRepository.Register(userNotification);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error registering notification for user {account.Id}. Error Message: {ex.Message}");
                }
            }
        }
    }
}