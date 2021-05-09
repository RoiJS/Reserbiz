using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IAccountRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {

        Task<Account> VerifyUsernameOrEmailAddress(string usernameOrEmailAddress);
    }
}