using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        void DeleteMultipleEntities(List<TEntity> entities, bool forceDelete);
        void SetEntityStatus(TEntity entity, bool status);
        void SetCurrentUser(int currentUserId);
        Task Reset();
        IReserbizRepository<TEntity> GetEntity(int id);
        IReserbizRepository<TEntity> GetAllEntities(bool includeDeleted = false);
        IReserbizRepository<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> ToObjectAsync();
        Task<IList<TEntity>> ToListObjectAsync();
        Task<bool> SaveChangesAsync();
    }
}