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

        public async Task<IEnumerable<PaymentBreakdown>> GetAllPaymentsAsync(int accountStatementId, SortOrderEnum sortOrder)
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
                                               IsAmountFromDeposit = p.IsAmountFromDeposit
                                           }).ToListAsync();


            var user = await (from u in _reserbizRepository.ClientDbContext.Accounts where u.Id == 1 select u).FirstOrDefaultAsync();

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