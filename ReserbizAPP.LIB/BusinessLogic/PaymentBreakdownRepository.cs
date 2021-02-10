using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
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

        public async Task<IEnumerable<PaymentBreakdown>> GetAllPaymentsAsync(int accountStatementId, SortOrderEnum sortOrder)
        {
            var paymentBreakdowns = await _reserbizRepository.ClientDbContext.PaymentBreakdowns
                                                    .Where(a => a.AccountStatementId == accountStatementId)
                                                    .Includes(a => a.ReceivedBy)
                                                    .ToListAsync();

            if (sortOrder == SortOrderEnum.Ascending)
            {
                return paymentBreakdowns
                        .OrderBy(p => p.DateTimeReceived)
                        .ToList();
            }
            else
            {
                return paymentBreakdowns
                        .OrderByDescending(p => p.DateTimeReceived)
                        .ToList();
            }
        }

    }
}