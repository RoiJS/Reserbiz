using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPaymentBreakdownRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<PaymentBreakdown>> GetAllPaymentsAsync(int accountStatementId, SortOrderEnum sortOrder);
    }
}