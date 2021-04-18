
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IDataSeedRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task SeedData(UserAccount userAccount, Client client);
    }
}