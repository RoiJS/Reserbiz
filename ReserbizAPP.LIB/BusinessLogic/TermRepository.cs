using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TermRepository
        : BaseRepository<Term>, ITermRepository<Term>
    {
        public TermRepository(IReserbizRepository<Term> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<Term> GetTermAsync(int id)
        {
            var term = await _reserbizRepository.ClientDbContext.Terms
                .Include(t => t.TermMiscellaneous)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            return term;
        }
    }
}