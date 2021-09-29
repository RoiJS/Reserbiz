using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAccountStatementRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        PenaltyBreakdown RegisterNewPenaltyItem(AccountStatement accountStatement);
        Task<AccountStatement> GetSuggestedNewAccountStatement(int contractId);
        Task<AccountStatement> GetAccountStatementAsync(int id);
        Task<IEnumerable<AccountStatement>> GetActiveAccountStatementsPerContractAsync(int contractId);
        Task<IEnumerable<AccountStatement>> GetUnpaidAccountStatementsAsync();
        List<AccountStatement> GetFilteredAccountStatements(IList<AccountStatement> unfilteredAccountStatements, IAccountStatementFilter accountStatementFilter);
        Task GenerateContractAccountStatementsForRentalBill(string dbHashName, int contractId);
        Task GenerateContractAccountStatementsRentalBillForNewDatabase(int contractId, int currentUserId);
        Task GenerateNewContractAccountStatement(AccountStatement accountStatement, bool markAsPaid, int currentUserId);
        Task GenerateAccountStatementPenalties(int tenantId);
        Task<AccountStatement> GetFirstAccountStatement(int contractId);
        float CalculateTotalAmountPaid(IEnumerable<PaymentBreakdown> paymentBreakdowns);
        float CalculateTotalAmountPaidUsingDeposit(IEnumerable<PaymentBreakdown> paymentBreakdowns);
        Task<double> CalculatedDepositedAmountBalance(int contractId, AccountStatement firstAccountStatement);
        double CalculatedSuggestedAmountForRentalPayment(AccountStatement firstAccountStatement, double depositedAmountBalance);
        double CalculateSuggestedAmountForElectricBill(AccountStatement currentAccountStatement, double depositedAmountBalance);
        double CalculateSuggestedAmountForWaterBill(AccountStatement currentAccountStatement, double depositedAmountBalance);
        double CalculateSuggestedAmountForMiscellaneousFees(AccountStatement currentAccountStatement, double depositedAmountBalance);
        double CalculateSuggestedAmountForPenaltyAmount(AccountStatement currentAccountStatement, double depositedAmountBalance);
        Task<float> CalculateOverAllPaymentUsedFromDepositedAmount(int contractId);
        Task<AccountStatementsAmountSummary> GetAccountStatementsAmountSummary();
        Task SendAccountStatement(int id);
    }
}