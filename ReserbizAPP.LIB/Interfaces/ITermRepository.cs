using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITermRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<Term> GetTermAsync(int id);
        Task<IEnumerable<Term>> GetTermsAsync();
        Task<bool> DeleteMultipleTermsAsync(List<int> termIds);
        bool CheckTermCodeIfExists(IList<Term> termList, int termId, string termCode);
    }
}