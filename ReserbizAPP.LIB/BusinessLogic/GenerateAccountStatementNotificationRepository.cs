using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class GenerateAccountStatementNotificationRepository
        : BaseRepository<GeneratedAccountStatementNotification>, IGenerateAccountStatementNotificationRepository<GeneratedAccountStatementNotification>
    {   
        
        public GenerateAccountStatementNotificationRepository(IReserbizRepository<GeneratedAccountStatementNotification> reserbizRepository)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
    }
}