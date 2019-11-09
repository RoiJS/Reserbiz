using System.Collections.Generic;
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
        protected readonly DbContext _repoDbContext;

        public BaseRepository(IReserbizRepository<TEntity> reserbizRepository, DbContext repoDbContext)
        {
            _reserbizRepository = reserbizRepository;
            _repoDbContext = repoDbContext;

            // This will set the current database context to which TEntity 
            // will be coming from to perform CRUD and other
            // custom functions.
            _reserbizRepository.SetDbContext(_repoDbContext);
        }


        public async Task AddEntity(TEntity entity)
        {
            await _reserbizRepository.AddEntity(entity);
        }

        public void DeleteEntity(TEntity entity, bool forceDelete = false)
        {
            _reserbizRepository.DeleteEntity(entity, forceDelete);
        }

        public void SetEntityStatus(TEntity entity, bool status)
        {
            _reserbizRepository.SetEntityStatus(entity, status);
        }

        public bool HasChanged()
        {
            return _repoDbContext.ChangeTracker.HasChanges();
        }

        public async Task<TEntity> GetEntityById(int id)
        {
            var entity = await _reserbizRepository.GetEntityById(id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllEntities(bool includeDeleted = false)
        {
            var entities = await _reserbizRepository.GetAllEntities(includeDeleted);
            return entities;
        }

        public async Task<bool> SaveChanges()
        {
            return await _reserbizRepository.SaveChangesAsync();
        }

    }
}