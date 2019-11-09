using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task AddEntity(TEntity entity);
        void DeleteEntity(TEntity entity, bool forceDelete = false);
        void SetEntityStatus(TEntity entity, bool status);
        bool HasChanged();
        Task<TEntity> GetEntityById(int id);
        Task<IEnumerable<TEntity>> GetAllEntities(bool includeDeleted = false);
        Task<bool> SaveChanges();
    }
}