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
        Task<IEnumerable<AccountStatement>> GetAccountStatementsPerContractAsync(int contractId);
        Task<IEnumerable<AccountStatement>> GetActiveDueAccountStatementsPerContractAsync(int contractId);
    }
}