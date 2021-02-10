using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class PenaltyBreakdownRepository
        : BaseRepository<PenaltyBreakdown>, IPenaltyBreakdownRepository<PenaltyBreakdown>
    {
        public PenaltyBreakdownRepository(IReserbizRepository<PenaltyBreakdown> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<IEnumerable<PenaltyBreakdown>> GetAllPenaltiesAsync(int accountStatementId, SortOrderEnum sortOrder)
        {
            var penaltyBreakdowns = await _reserbizRepository.ClientDbContext.PenaltyBreakdowns
                                                    .Where(a => a.AccountStatementId == accountStatementId)
                                                    .ToListAsync();

            if (sortOrder == SortOrderEnum.Ascending)
            {
                return penaltyBreakdowns
                        .OrderBy(p => p.DueDate)
                        .ToList();
            }
            else
            {
                return penaltyBreakdowns
                        .OrderByDescending(p => p.DueDate)
                        .ToList();
            }
        }
    }
}