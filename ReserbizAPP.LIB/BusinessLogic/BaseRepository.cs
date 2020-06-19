using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class BaseRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IReserbizRepository<TEntity> _reserbizRepository;
        private readonly DbContext _repoDbContext;

        public BaseRepository(IReserbizRepository<TEntity> reserbizRepository, DbContext repoDbContext)
        {
            _reserbizRepository = reserbizRepository;
            _repoDbContext = repoDbContext;

            // This will set the current database context to which TEntity 
            // will be coming from to perform CRUD and other
            // custom functions.
            _reserbizRepository.SetDbContext(_repoDbContext);
        }

        public BaseRepository()
        {

        }

        public IBaseRepository<TEntity> SetCurrentUserId(int currentUserId)
        {
            _reserbizRepository.SetCurrentUser(currentUserId);

            return this;
        }

        public async Task AddEntity(TEntity entity)
        {
            await _reserbizRepository.AddEntity(entity);
        }

        public void DeleteEntity(TEntity entity, bool forceDelete = false)
        {
            _reserbizRepository.DeleteEntity(entity, forceDelete);
        }

        public void DeleteMultipleEntities(List<TEntity> entities, bool forceDelete = false)
        {
            _reserbizRepository.DeleteMultipleEntities(entities, forceDelete);
        }

        public void SetEntityStatus(TEntity entity, bool status)
        {
            _reserbizRepository.SetEntityStatus(entity, status);
        }

        public bool HasChanged()
        {
            return _repoDbContext.ChangeTracker.HasChanges();
        }

        public async Task<bool> IsExists(int id)
        {
            var entity = await GetEntity(id).ToObjectAsync();
            return (entity != null);
        }

        public IBaseRepository<TEntity> GetEntity(int id)
        {
            _reserbizRepository.GetEntity(id);
            return this;
        }

        public IBaseRepository<TEntity> GetAllEntities(bool includeDeleted = false)
        {
            _reserbizRepository.GetAllEntities(includeDeleted);
            return this;
        }

        public IBaseRepository<TEntity> Includes(params Expression<Func<TEntity, object>>[] includes)
        {
            _reserbizRepository.Includes(includes);
            return this;
        }

        public async Task<TEntity> ToObjectAsync()
        {
            var entityObject = await _reserbizRepository.ToObjectAsync();
            return entityObject;
        }

        public async Task<IList<TEntity>> ToListObjectAsync()
        {
            var entitiesObject = await _reserbizRepository.ToListObjectAsync();
            return entitiesObject;
        }

        public async Task<bool> SaveChanges()
        {
            return await _reserbizRepository.SaveChangesAsync();
        }

        public async virtual Task Reset()
        {
            await _reserbizRepository.Reset();
        }
    }
}