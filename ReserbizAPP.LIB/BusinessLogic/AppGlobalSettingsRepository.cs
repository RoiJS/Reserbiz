using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AppGlobalSettingsRepository
        : BaseRepository<AppGlobalSettings>, IAppGlobalSettingsRepository<AppGlobalSettings>
    {

        public AppGlobalSettingsRepository(IReserbizRepository<AppGlobalSettings> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {

        }

        public async Task<AppGlobalSettings> GetAppGlobalSettings()
        {
            var appGlobalSettings = await GetAllEntities().ToListObjectAsync();
            return appGlobalSettings.Count > 0 ? appGlobalSettings[0] : null;
        }
    }
}