using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AuthRepository : ReserbizDataContextRepository, IAuthRepository
    {

        public AuthRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext) : base(systemDbContext, clientDbContext)
        {
           
        }

        public async Task<Account> Login(string username, string password)
        {
            var account = await _clientDbContext.Accounts.FirstOrDefaultAsync(x => x.Username == username);

            if (account == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, account.PasswordHash))
            {
                return null;
            }

            return account;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
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
            if (await _clientDbContext.Accounts.AnyAsync(x => x.Username == username))
                return true;

            return false;

        }
        public async Task<Account> Register(Account user, string password)
        {
            byte[] passwordHash;
            CreatePasswordHash(password, out passwordHash);

            user.PasswordHash = passwordHash;

            await _clientDbContext.Accounts.AddAsync(user);
            await _clientDbContext.SaveChangesAsync();

            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}