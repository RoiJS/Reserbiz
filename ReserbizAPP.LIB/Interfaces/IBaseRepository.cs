using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task AddEntity(TEntity entity);
        void DeleteEntity(TEntity entity, bool forceDelete = false);
        void SetEntityStatus(TEntity entity, bool status);
        bool HasChanged();
        Task<bool> IsExists(int id);
        IBaseRepository<TEntity> GetEntity(int id);
        IBaseRepository<TEntity> GetAllEntities(bool includeDeleted = false);
        IBaseRepository<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> ToObjectAsync();
        Task<IEnumerable<TEntity>> ToListObjectAsync();
        Task<bool> SaveChanges();
    }
}