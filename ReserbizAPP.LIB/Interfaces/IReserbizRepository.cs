using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IReserbizRepository<TEntity> where TEntity : class, IEntity
    {
        ReserbizDataContext SystemDbContext { get; }
        ReserbizClientDataContext ClientDbContext { get; }
        
        void SetDbContext(DbContext dbContext);
        Task AddEntity(TEntity entity);
        void DeleteEntity(TEntity entity, bool forceDelete);
        void SetEntityStatus(TEntity entity, bool status);
        Task<TEntity> GetEntityById(int id);
        Task<IEnumerable<TEntity>> GetAllEntities(bool includeDeleted = false);
        Task<bool> SaveChangesAsync();
    }
}