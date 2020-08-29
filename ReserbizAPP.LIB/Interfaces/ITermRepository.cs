using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITermRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<Term> GetTermAsync(int id);
        Task<IEnumerable<Term>> GetTermsAsync(string termKeywords);
        Task<IEnumerable<Term>> GetTermsAsOptions();
        Task<Term> DuplicateTerm(int termId);
        Task<bool> DeleteMultipleTermsAsync(List<int> termIds);
        bool CheckTermCodeIfExists(IList<Term> termList, int termId, string termCode);
        Task<bool> CheckTermSpaceTypeAvailability(int termId);
    }
}