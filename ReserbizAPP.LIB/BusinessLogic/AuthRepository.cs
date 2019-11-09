using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AuthRepository : BaseRepository<Account>, IAuthRepository<Account>
    {
        public AuthRepository(IReserbizRepository<Account> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<Account> Login(string username, string password)
        {
            var account = await _reserbizRepository.ClientDbContext.Accounts.FirstOrDefaultAsync(x => x.Username == username);

            if (account == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return null;
            }

            return account;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _reserbizRepository.ClientDbContext.Accounts.AnyAsync(x => x.Username == username))
                return true;

            return false;

        }
        public async Task<Account> Register(Account account, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            await AddEntity(account);

            return account;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}