using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITermVersionRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {

        Task AddTermVersion(int contractId, int termId);
    }
}