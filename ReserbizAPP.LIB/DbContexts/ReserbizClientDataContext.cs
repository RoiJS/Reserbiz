using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.DbContexts
{
    public class ReserbizClientDataContext : DbContext
    {
        private readonly IDataContextHelper _dcHelper;
        public ReserbizClientDataContext(DbContextOptions<ReserbizClientDataContext> options, IDataContextHelper dcHelper) : base(options)
        {
            _dcHelper = dcHelper;
        }

        #region "DB models Definitions"

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<SpaceType> SpaceTypes { get; set; }
        public DbSet<TermMiscellaneous> TermMiscellaneous { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<AccountStatement> AccountStatements { get; set; }
        public DbSet<AccountStatementMiscellaneous> AccountStatementMiscellaneous { get; set; }
        public DbSet<PaymentBreakdown> PaymentBreakdowns { get; set; }
        public DbSet<PenaltyBreakdown> PenaltyBreakdowns { get; set; }
        public DbSet<ClientSettings> ClientSettings { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        #endregion

        #region "Override functions"

        /// <summary>
        /// Overriden SaveChangesAsync to add custom logic before actual saving of entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries().ToList();

            _dcHelper.GenerateEntityCreatedDate(entries);
            _dcHelper.GenerateEntityUpdateDate(entries);

            return await base.SaveChangesAsync();
        }
        
        /// <summary>
        /// Overriden SaveChanges to add custom logic before actual saving of entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().ToList();

            _dcHelper.GenerateEntityCreatedDate(entries);
            _dcHelper.GenerateEntityUpdateDate(entries);

            return base.SaveChanges();
        }

        #endregion
    }
}