using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ClientSettingsRepository
        : BaseRepository<ClientSettings>, IClientSettingsRepository<ClientSettings>
    {
        public ClientSettingsRepository(IReserbizRepository<ClientSettings> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<ClientSettings> GetClientSettings()
        {
            var clientSettingsFromRepo = await GetAllEntities().ToListObjectAsync();
            return clientSettingsFromRepo.Count > 0 ? clientSettingsFromRepo[0] : null;
        }

        public override async Task Reset()
        {
            // Reset settings
            await base.Reset();
        }
    }
}