using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TermRepository
        : BaseRepository<Term>, ITermRepository<Term>
    {
        public TermRepository(IReserbizRepository<Term> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public TermRepository() : base()
        {

        }

        public async Task<Term> GetTermAsync(int id)
        {
            var term = await _reserbizRepository.ClientDbContext.Terms
                .Include(t => t.TermMiscellaneous)
                .Include(t => t.SpaceType)
                .FirstOrDefaultAsync(t => t.Id == id);

            return term;
        }

        public async Task<IEnumerable<Term>> GetTermsAsync()
        {
            var termsFromRepo = await _reserbizRepository.ClientDbContext.Terms
                                    .AsQueryable()
                                    .Includes(t => t.Contracts)
                                    .Where(t => t.IsDelete == false)
                                    .ToListAsync();

            return termsFromRepo;
        }

        public async Task<bool> DeleteMultipleTermsAsync(List<int> termIds)
        {
            var selectedTerms = await _reserbizRepository
                .ClientDbContext
                .Terms
                .Where(t => termIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedTerms);
            return await SaveChanges();
        }

        public bool CheckTermCodeIfExists(IList<Term> termList, int termId, string termCode)
        {
            var termWithTheSameCode = termList
                                .Where(t => (termId != 0 && (t.Id != termId && t.Code == termCode)) || (termId == 0 && t.Code == termCode))
                                .Count();

            return termWithTheSameCode > 0;
        }
    }
}