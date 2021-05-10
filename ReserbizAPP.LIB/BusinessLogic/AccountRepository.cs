using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AccountRepository
        : BaseRepository<Account>, IAccountRepository<Account>
    {

        public AccountRepository(IReserbizRepository<Account> reserbizRepository)
                    : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<Account> VerifyUsernameOrEmailAddress(string usernameOrEmailAddress)
        {
            var account = await _reserbizRepository.ClientDbContext.Accounts
                .Where(a => a.Username == usernameOrEmailAddress || a.EmailAddress == usernameOrEmailAddress)
                .FirstOrDefaultAsync();

            return account;
        }
    }
}