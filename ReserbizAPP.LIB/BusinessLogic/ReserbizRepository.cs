using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ReserbizRepository<TEntity>
        : IReserbizRepository<TEntity> where TEntity : class, IEntity
    {
        private ReserbizDataContext _systemDbContext;
        private ReserbizClientDataContext _clientDbContext;
        private DbContext _dbContext { get; set; }

        public ReserbizRepository(ReserbizDataContext systemDbContext, ReserbizClientDataContext clientDbContext)
        {
            _systemDbContext = systemDbContext;
            _clientDbContext = clientDbContext;
        }

        public void SetDbContext(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// This function inserts new entity.
        /// </summary>
        /// <param name="entity">Entity that is to be inserted</param>
        /// <typeparam name="TEntity">Class tyoe of the Entity</typeparam>
        public async Task AddEntity(TEntity entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This function will remove an entity.
        /// If parameter forceDelete is true, it will perform an actual deletion of the entity, 
        /// If not, it will only mark the entity as deleted and it will not remove it from the db.
        /// </summary>
        /// <param name="entity">Entity that is to be removed or just mark as remove.</param>
        /// <param name="forceDelete">This will determine if the entity will be removed from the db or just mark it as deleted</param>
        /// <typeparam name="TEntity">Class type of the entity.true Note: parameter T must a class that inherits IEntity interface</typeparam>
        public void DeleteEntity(TEntity entity, bool forceDelete)
        {
            if (forceDelete)
            {
                _dbContext.Remove(entity);
            }
            else
            {
                entity.DateDeleted = DateTime.Now;
                entity.IsDelete = true;
            }
        }

        public void SetEntityStatus(TEntity entity, bool status)
        {
            entity.IsActive = status;
            entity.DateDeactivated = status ? DateTime.MinValue : DateTime.Now;
        }

        /// <summary>
        /// Get Entity based on Id.
        /// </summary>
        /// <param name="id">Id of the entity that is to be retrieve</param>
        /// <returns>An entity from the current _dbContext</returns>
        public async Task<TEntity> GetEntityById(int id)
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        /// <summary>
        /// Get All entities from current _dbContext.await If includeDeleted parameter is set to true, 
        /// It will retrieve all items including those who are already marked as deleted, otherwise only those non-deleted items will
        /// be retrieved.
        /// </summary>
        /// <param name="includeDeleted">Parameter that will determin if deleted items will also be included on the result</param>
        /// <returns>Return all items on the current _dbContext</returns>
        public async Task<IEnumerable<TEntity>> GetAllEntities(bool includeDeleted = false)
        {
            var entities = new List<TEntity>();

            if (includeDeleted)
            {
                entities = await _dbContext.Set<TEntity>().ToListAsync();
            }
            else
            {
                entities = await _dbContext.Set<TEntity>().Where(e => e.IsDelete == false).ToListAsync();
            }

            return entities;
        }

        /// <summary>
        /// This method performs save asynchronous that will persist any changes applied to the entity.
        /// </summary>
        /// <returns>Return true if there were affected with the changes applied and false if none.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public ReserbizDataContext SystemDbContext => _systemDbContext;
        public ReserbizClientDataContext ClientDbContext => _clientDbContext;
    }
}