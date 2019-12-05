using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}