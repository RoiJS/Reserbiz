using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IClientDbManagerRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task SyncAllClientDatabases();
    }
}