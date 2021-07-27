using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TermVersionRepository
        : BaseRepository<TermVersion>, ITermVersionRepository<TermVersion>
    {
        public TermVersionRepository(IReserbizRepository<TermVersion> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
        }

        public TermVersionRepository() : base()
        {

        }

        public async Task AddTermVersion(int contractId, int termId)
        {
            var termVersion = new TermVersion();
            termVersion.ContractId = contractId;
            termVersion.TermId = termId;
            await AddEntity(termVersion);
        }
    }
}