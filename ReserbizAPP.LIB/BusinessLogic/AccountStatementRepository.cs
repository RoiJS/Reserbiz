using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AccountStatementRepository
        : BaseRepository<AccountStatement>, IAccountStatementRepository<AccountStatement>
    {

        public AccountStatementRepository(IReserbizRepository<AccountStatement> reserbizRepository)
                : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public AccountStatement RegisterNewAccountStament(Contract contract)
        {
            var contractTerm = contract.Term;
            var newAccountStatement = new AccountStatement
            {
                DueDate = contract.NextDueDate,
                Rate = contractTerm.Rate,
                DurationUnit = contractTerm.DurationUnit,
                ElectricBill = contractTerm.ElectricBillAmount,
                WaterBill = contractTerm.WaterBillAmount,
                PenaltyValue = contractTerm.PenaltyValue,
                PenaltyValueType = contractTerm.PenaltyValueType,
                AdvancedPaymentDurationValue = contractTerm.AdvancedPaymentDurationValue,
                DepositPaymentDurationValue = contractTerm.DepositPaymentDurationValue,
                PenaltyAmountPerDurationUnit = contractTerm.PenaltyAmountPerDurationUnit,
                PenaltyEffectiveAfterDurationValue = contractTerm.PenaltyEffectiveAfterDurationValue,
                PenaltyEffectiveAfterDurationUnit = contractTerm.PenaltyEffectiveAfterDurationUnit
            };
            newAccountStatement.AccountStatementMiscellaneous.AddRange(GenerateAccountStatementMiscellaneous(contractTerm.TermMiscellaneous));
            return newAccountStatement;
        }

        private List<AccountStatementMiscellaneous> GenerateAccountStatementMiscellaneous(List<TermMiscellaneous> termMiscellaneous)
        {
            var accountTermMiscellaneous = new List<AccountStatementMiscellaneous>();
            foreach (var item in termMiscellaneous)
            {
                var newTermMiscellaneous = new AccountStatementMiscellaneous();
                newTermMiscellaneous.Name = item.Name;
                newTermMiscellaneous.Description = item.Description;
                newTermMiscellaneous.Amount = item.Amount;
                accountTermMiscellaneous.Add(newTermMiscellaneous);
            }
            return accountTermMiscellaneous;
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
                                                                c.IsActive
                                                                && c.ContractId == contractId
                                                            )
                                                            .ToListAsync();

            return activeAccountStatementsPerContractFromRepo;
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
                                                    c.IsActive
                                                    && c.ContractId == contractId
                                                )
                                                .ToListAsync();

            var activeDueAccountStatements = activeAccountStatementsPerContractFromRepo.Where(a => a.IsDueToGeneratePenalty).ToList();

            return activeDueAccountStatements;
        }
    }
}