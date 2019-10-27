using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.DbContexts
{
    public class DataContextHelper : IDataContextHelper
    {
        /// <summary>
        /// Auto generate data updated for each updated entities
        /// </summary>
        public void GenerateEntityUpdateDate(List<EntityEntry> entries)
        {
            var currentDateTime = DateTime.Now;
            // get a list of all Modified entries which implement the IUpdatable interface
            var updatedEntries = GetEntityEntries(EntityState.Modified, entries);

            updatedEntries.ForEach(e =>
            {
                ((Entity)e.Entity).DateUpdated = currentDateTime;
            });
        }

        /// <summary>
        /// Auto generate created date for each added entities
        /// </summary>
        public void GenerateEntityCreatedDate(List<EntityEntry> entries)
        {
            var currentDateTime = DateTime.Now;
            // get a list of all Added entries which implement the IUpdatable interface
            var addedEntries = GetEntityEntries(EntityState.Added, entries);

            addedEntries.ForEach(e =>
            {
                ((Entity)e.Entity).DateCreated = currentDateTime;
            });
        }

        /// <summary>
        /// Get entities based on the entity state supplied on parameter entityState
        /// </summary>
        /// <param name="entityState"></param>
        /// <param name="entries"></param>
        /// <returns>List of entity entries</returns>
        private List<EntityEntry> GetEntityEntries(EntityState entityState, List<EntityEntry> entries)
        {
            var entityEntries = entries.Where(e => e.Entity is Entity)
                    .Where(e => e.State == entityState)
                    .ToList();

            return entityEntries;
        }
    }
}