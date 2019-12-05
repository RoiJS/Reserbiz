using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class PaymentBreakdownRepository
        : BaseRepository<PaymentBreakdown>, IPaymentBreakdownRepository<PaymentBreakdown>
    {
        public PaymentBreakdownRepository(IReserbizRepository<PaymentBreakdown> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
    }
}