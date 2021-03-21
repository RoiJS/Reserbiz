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
        private readonly ISpaceTypeRepository<SpaceType> _spaceTypeRepository;

        public TermRepository(IReserbizRepository<Term> reserbizRepository, ISpaceTypeRepository<SpaceType> spaceTypeRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _spaceTypeRepository = spaceTypeRepository;
        }

        public TermRepository() : base()
        {

        }

        public async Task<Term> GetTermAsync(int id)
        {
            var term = await _reserbizRepository.ClientDbContext.Terms
                .Include(t => t.TermMiscellaneous)
                .Include(t => t.SpaceType)
                .Include(t => t.TermChildren)
                .FirstOrDefaultAsync(t => t.Id == id);

            return term;
        }

        public async Task<IEnumerable<Term>> GetTermsAsync(string termKeywords)
        {
            var termsFromRepo = _reserbizRepository.ClientDbContext.Terms
                                    .AsQueryable()
                                    .Includes(
                                        t => t.Contracts,
                                        t => t.TermChildren
                                    )
                                    .Where(t => t.IsDelete == false && t.TermParentId == null);

            if (!string.IsNullOrEmpty(termKeywords))
            {
                termsFromRepo = termsFromRepo.Where(t => t.Code.Contains(termKeywords) || t.Name.Contains(termKeywords));
            }

            return await termsFromRepo.ToListAsync();
        }

        public async Task<IEnumerable<Term>> GetTermsAsOptions()
        {
            return await _reserbizRepository.ClientDbContext.Terms
                                    .Where(t => t.TermParentId == null)
                                    .OrderBy(s => s.Name)
                                    .ToListAsync();
        }

        public async Task<Term> DuplicateTerm(int termId)
        {
            var termDetailsFromRepo = await GetTermAsync(termId);

            // Assigning the current term id as the 
            // parent term id of the term to be duplicated
            termDetailsFromRepo.TermParentId = termId;

            DetachEntity<Term>(termDetailsFromRepo);
            DetachEntities<TermMiscellaneous>(termDetailsFromRepo.TermMiscellaneous);

            await AddEntity(termDetailsFromRepo);

            return termDetailsFromRepo;
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
                                .Where(t => (t.TermParentId == null && termId != 0 && (t.Id != termId && t.Code == termCode)) || (termId == 0 && t.Code == termCode))
                                .Count();

            return termWithTheSameCode > 0;
        }
    }
}