using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class GeneralInformationRepository
        : BaseRepository<GeneralInformation>, IGeneralInformationRepository<GeneralInformation>
    {
        public GeneralInformationRepository(IReserbizRepository<GeneralInformation> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {

        }

        public async Task<GeneralInformation> GetGeneralInformation()
        {
            var generalInformationFromRepo = await GetAllEntities().ToListObjectAsync();
            return generalInformationFromRepo.Count > 0 ? generalInformationFromRepo[0] : null;
        }
    }
}