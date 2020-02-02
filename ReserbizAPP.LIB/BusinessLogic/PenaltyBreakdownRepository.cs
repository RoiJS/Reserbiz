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
    }
}