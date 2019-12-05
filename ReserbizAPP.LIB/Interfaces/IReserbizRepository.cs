using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        IReserbizRepository<TEntity> GetEntity(int id);
        IReserbizRepository<TEntity> GetAllEntities(bool includeDeleted = false);
        IReserbizRepository<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> ToObjectAsync();
        Task<IEnumerable<TEntity>> ToListObjectAsync();
        Task<bool> SaveChangesAsync();
    }
}