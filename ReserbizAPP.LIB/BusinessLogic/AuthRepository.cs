using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class AuthRepository
        : BaseRepository<Account>, IAuthRepository<Account>
    {
        public AuthRepository(IReserbizRepository<Account> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<Account> Login(string username, string password)
        {
            var account = await _reserbizRepository.ClientDbContext.Accounts
                .Includes(a => a.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Username == username);

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

        public async Task<bool> UserExists(string username, int userId = 0)
        {
            var user = await _reserbizRepository.ClientDbContext.Accounts.Where(x => x.Username == username).FirstOrDefaultAsync();

            if (user != null && user.Id != userId)
                return true;

            return false;
        }

        public async Task<Account> Register(Account account, string password)
        {
            var encryptedPassword = GenerateNewPassword(password);

            account.PasswordHash = encryptedPassword.PasswordHash;
            account.PasswordSalt = encryptedPassword.PasswordSalt;

            await AddEntity(account);

            return account;
        }

        public EncryptedPassword GenerateNewPassword(string newPassword)
        {
            byte[] passwordHash, passwordSalt;

            SystemUtility.EncryptionUtility.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

            return new EncryptedPassword
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        public override async Task Reset()
        {
            var password = "Starta123";
            byte[] passwordHash, passwordSalt;

            SystemUtility.EncryptionUtility.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Reset account list
            await base.Reset();

            // Setup default account
            await AddEntity(new Account
            {
                FirstName = "James",
                MiddleName = "Harden",
                LastName = "Harden",
                Gender = GenderEnum.Male,
                PhotoUrl = "",
                Username = "jaha",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
        }

        public RefreshToken GenerateNewRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    ExpirationDate = DateTime.Now.AddDays(5)
                };
            }
        }

        public async Task RemoveExpiredRefreshTokens()
        {
            // Get all expired refresh tokens
            var expiredRefreshTokens = await _reserbizRepository
                                                .ClientDbContext.RefreshTokens
                                                .Where(r => DateTime.Now > r.ExpirationDate)
                                                .ToListAsync();

            // Delete expired refresh tokens
            _reserbizRepository.ClientDbContext.RefreshTokens.RemoveRange(expiredRefreshTokens);
        }
    }

    public class EncryptedPassword
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}