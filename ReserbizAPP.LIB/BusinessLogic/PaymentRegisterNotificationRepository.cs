using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class PaymentRegisterNotificationRepository
        : BaseRepository<PaymentRegisterNotification>, IPaymentRegisterNotificationRepository<PaymentRegisterNotification>
    {
        
        public PaymentRegisterNotificationRepository(IReserbizRepository<PaymentRegisterNotification> reserbizRepository)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
    }
}