using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class PaymentBreakdownRepository
        : BaseRepository<PaymentBreakdown>, IPaymentBreakdownRepository<PaymentBreakdown>
    {
        private readonly IOptions<ApplicationSettings> _appSettings;
        public PaymentBreakdownRepository(IReserbizRepository<PaymentBreakdown> reserbizRepository, IOptions<ApplicationSettings> appSettings)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _appSettings = appSettings;
        }

        public PaymentBreakdownRepository()
        {
            
        }

        public async Task<IEnumerable<PaymentBreakdown>> GetAllPaymentsPerAccountStatmentAsync(int accountStatementId)
        {
            var paymentBreakdowns = await (from p in _reserbizRepository.ClientDbContext.PaymentBreakdowns
                                           where p.AccountStatementId == accountStatementId

                                           join u in _reserbizRepository.ClientDbContext.Accounts
                                           on p.ReceivedById equals u.Id

                                           select new PaymentBreakdown
                                           {
                                               AccountStatementId = p.AccountStatementId,
                                               Amount = p.Amount,
                                               DateTimeReceived = p.DateTimeReceived.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone),
                                               ReceivedById = p.ReceivedById,
                                               ReceivedBy = u,
                                               Notes = p.Notes,
                                               IsAmountFromDeposit = p.IsAmountFromDeposit,
                                               PaymentForType = p.PaymentForType
                                           }).ToListAsync();

            return paymentBreakdowns;
        }

        public List<PaymentBreakdown> GetFilteredPayments(IList<PaymentBreakdown> unfilteredPayments, IPaymentFilter paymentFilter)
        {
            var filteredPayments = unfilteredPayments;

            // Filter payment for type 
            if (paymentFilter.PaymentForType != PaymentForTypeEnum.All)
            {
                filteredPayments = filteredPayments.Where(c => c.PaymentForType == paymentFilter.PaymentForType).ToList();
            }

            // Set sort order based on next due date
            // Sort order is ascending by default
            if (paymentFilter.SortOrder == SortOrderEnum.Ascending)
            {
                return filteredPayments
                    .OrderBy(c => c.DateTimeReceived)
                    .ToList();
            }
            else
            {
                return filteredPayments
                    .OrderByDescending(c => c.DateTimeReceived)
                    .ToList();
            }
        }

    }
}