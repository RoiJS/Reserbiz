using System.Threading.Tasks;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAuthRepository<TEntity> 
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<Account> Register(Account account, string password);
        Task<Account> Login(string username, string password);
        Task<bool> UserExists(string username, int userId = 0);
        EncryptedPassword GenerateNewPassword(string password);
        RefreshToken GenerateNewRefreshToken();
        Task RemoveExpiredRefreshTokens();
    }
}