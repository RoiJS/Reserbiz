using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITermMiscellaneousRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TermMiscellaneous>> GetAllTermMiscellaneousPerTerm(int termId);
        Task<bool> DeleteMultipleTermMiscelleneousAsync(List<int> termMiscellaneousIds);
    }
}