using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AccountStatementMiscellaneousRepository
        : BaseRepository<AccountStatementMiscellaneous>, IAccountStatementMiscellaneousRepository<AccountStatementMiscellaneous>
    {
        public AccountStatementMiscellaneousRepository(IReserbizRepository<AccountStatementMiscellaneous> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
    }
}