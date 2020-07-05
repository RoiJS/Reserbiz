using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TermMiscellaneousRepository
        : BaseRepository<TermMiscellaneous>, ITermMiscellaneousRepository<TermMiscellaneous>
    {
        public TermMiscellaneousRepository(IReserbizRepository<TermMiscellaneous> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<bool> DeleteMultipleTermMiscelleneousAsync(List<int> termMiscellaneousIds)
        {
            var selectedTermMiscellaneous = await _reserbizRepository
                .ClientDbContext
                .TermMiscellaneous
                .Where(t => termMiscellaneousIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedTermMiscellaneous);
            return await SaveChanges();
        }


        public async Task<IEnumerable<TermMiscellaneous>> GetAllTermMiscellaneousPerTerm(int termId)
        {
            var termMiscellaneousFromRepo = await _reserbizRepository
                .ClientDbContext
                .TermMiscellaneous
                .Where(t => t.TermId == termId && t.IsDelete == false)
                .OrderBy(t => t.Name)
                .ToListAsync();

            return termMiscellaneousFromRepo;
        }
    }
}