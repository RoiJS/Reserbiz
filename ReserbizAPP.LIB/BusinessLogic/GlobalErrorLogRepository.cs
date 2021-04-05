using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class GlobalErrorLogRepository
        : BaseRepository<GlobalErrorLog>, IGlobalErrorLogRepository<GlobalErrorLog>
    {

        public GlobalErrorLogRepository(IReserbizRepository<GlobalErrorLog> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {

        }

        public async Task RegisterGlobalError(string source, string message, string stackTrace, int clientId)
        {
            var newError = new GlobalErrorLog()
            {
                Source = source,
                Message = message,
                Stacktrace = stackTrace,
                ClientId = clientId
            };

            await AddEntity(newError);
        }
    }
}