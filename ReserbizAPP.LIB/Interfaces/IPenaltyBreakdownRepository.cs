using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IPenaltyBreakdownRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<PenaltyBreakdown>> GetAllPenaltiesAsync(int accountStatement, SortOrderEnum sortOrder);
    }
}