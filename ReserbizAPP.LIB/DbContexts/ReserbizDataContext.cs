using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.DbContexts {
    public class ReserbizDataContext : DbContext {

        private readonly IDataContextHelper _dcHelper;

        public ReserbizDataContext (DbContextOptions<ReserbizDataContext> options, IDataContextHelper dcHelper) : base (options) {
            _dcHelper = dcHelper;
        }

        public DbSet<Client> Clients { get; set; }

        #region "Override functions"

        /// <summary>
        /// Overriden SaveChangesAsync to add custom logic before actual saving of entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {

            var entries = ChangeTracker.Entries().ToList();

            _dcHelper.GenerateEntityCreatedDate(entries);
            _dcHelper.GenerateEntityUpdateDate(entries);

            return await base.SaveChangesAsync();
        }

        #endregion
    }
}