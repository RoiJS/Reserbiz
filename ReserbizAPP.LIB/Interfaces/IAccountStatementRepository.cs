using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAccountStatementRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        AccountStatement RegisterNewAccountStament(Contract contract);
        PenaltyBreakdown RegisterNewPenaltyItem(AccountStatement accountStatement);
        Task<AccountStatement> GetAccountStatementAsync(int id);
        Task<IEnumerable<AccountStatement>> GetActiveAccountStatementsPerContractAsync(int contractId);
        List<AccountStatement> GetFilteredAccountStatements(IList<AccountStatement> unfilteredAccountStatements, IAccountStatementFilter accountStatementFilter);
        Task GenerateContractAccountStatements(int contractId);
        Task GenerateAccountStatementPenalties(int tenantId);
        Task<AccountStatement> GetFirstAccountStatement(int contractId);
        float CalculateTotalAmountPaid(IEnumerable<PaymentBreakdown> paymentBreakdowns);
        float CalculateTotalAmountPaidUsingDeposit(IEnumerable<PaymentBreakdown> paymentBreakdowns);
        Task<double> CalculatedDepositedAmountBalance(int contractId, AccountStatement firstAccountStatement);
        double CalculatedSuggestedAmountForPayment(AccountStatement firstAccountStatement, double depositedAmountBalance);
        Task<float> CalculateOverAllPaymentUsedFromDepositedAmount(int contractId);
    }
}