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
        public int? CurrentUserId { get; set; }

        public ReserbizClientDataContext(DbContextOptions<ReserbizClientDataContext> options, IDataContextHelper dcHelper) : base(options)
        {
            _dcHelper = dcHelper;
        }

        #region "DB models Definitions"

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<SpaceType> SpaceTypes { get; set; }
        public DbSet<Space> Spaces { get; set; }
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
        /// Overrides onModelCreating to add custom model creation criteria
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedAccounts)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_Accounts_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedAccounts)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_Accounts_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedAccounts)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_Accounts_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedAccounts)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_Accounts_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<Tenant>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedTenants)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_Tenants_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedTenants)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_Tenants_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedTenants)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_Tenants_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedTenants)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_Tenants_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<ContactPerson>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedContactPersons)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_ContactPersons_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedContactPersons)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_ContactPersons_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedContactPersons)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_ContactPersons_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedContactPersons)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_ContactPersons_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<SpaceType>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedSpaceTypes)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_SpaceTypes_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedSpaceTypes)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_SpaceTypes_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedSpaceTypes)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_SpaceTypes_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedSpaceTypes)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_SpaceTypes_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<Space>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedSpaces)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_Spaces_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedSpaces)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_Spaces_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedSpaces)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_Spaces_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedSpaces)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_Spaces_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<Term>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedTerms)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_Terms_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedTerms)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_Terms_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedTerms)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_Terms_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedTerms)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_Terms_DeactivatedById_Accounts_AccountId");

                a.HasOne(field => field.TermParent)
                .WithMany(fk => fk.TermChildren)
                .HasForeignKey(fk => fk.TermParentId)
                .HasConstraintName("FK_Terms_Terms_TermParentId");
            });

            modelBuilder.Entity<TermMiscellaneous>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedTermMiscellaneous)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_TermMiscellaneous_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedTermMiscellaneous)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_TermMiscellaneous_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedTermMiscellaneous)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_TermMiscellaneous_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedTermMiscellaneous)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_TermMiscellaneous_DeactivatedById_Accounts_AccountId");
            });
            
            modelBuilder.Entity<Contract>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedContracts)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_Contracts_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedContracts)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_Contracts_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedContracts)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_Contracts_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedContracts)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_Contracts_DeactivatedById_Accounts_AccountId");
            });
            
            modelBuilder.Entity<AccountStatement>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedAccountStatements)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_AccountStatements_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedAccountStatements)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_AccountStatements_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedAccountStatements)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_AccountStatements_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedAccountStatements)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_AccountStatements_DeactivatedById_Accounts_AccountId");
            });
            
            modelBuilder.Entity<ClientSettings>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedClientSettings)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_ClientSettings_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedClientSettings)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_ClientSettings_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedClientSettings)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_ClientSettings_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedClientSettings)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_ClientSettings_DeactivatedById_Accounts_AccountId");
            });
            
            modelBuilder.Entity<PaymentBreakdown>(a =>
            {
                a.HasOne(field => field.CreatedBy)
                .WithMany(fk => fk.CreatedPaymentBreakdowns)
                .HasForeignKey(fk => fk.CreatedById)
                .HasConstraintName("FK_PaymentBreakdown_CreatedById_Accounts_AccountId");

                a.HasOne(field => field.DeletedBy)
                .WithMany(fk => fk.DeletedPaymentBreakdowns)
                .HasForeignKey(fk => fk.DeletedById)
                .HasConstraintName("FK_PaymentBreakdown_DeletedById_Accounts_AccountId");

                a.HasOne(field => field.UpdatedBy)
                .WithMany(fk => fk.UpdatedPaymentBreakdowns)
                .HasForeignKey(fk => fk.UpdatedById)
                .HasConstraintName("FK_PaymentBreakdowns_UpdatedById_Accounts_AccountId");

                a.HasOne(field => field.DeactivatedBy)
                .WithMany(fk => fk.DeactivatedPaymentBreakdowns)
                .HasForeignKey(fk => fk.DeactivatedById)
                .HasConstraintName("FK_PaymentBreakdowns_DeactivatedById_Accounts_AccountId");
            });

            modelBuilder.Entity<ErrorLog>(a =>
            {
                a.HasOne(field => field.User)
                .WithMany(fk => fk.ErrorLogs)
                .HasForeignKey(fk => fk.UserId)
                .HasConstraintName("FK_ErrorLogs_Accounts_UserId");
            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.HasOne(field => field.User)
                .WithMany(fk => fk.RefreshTokens)
                .HasForeignKey(fk => fk.AccountId)
                .HasConstraintName("FK_RefreshToken_Accounts_AccountId");
            });
        }

        /// <summary>
        /// Overriden SaveChangesAsync to add custom logic before actual saving of entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker.Entries().ToList();

            _dcHelper.GenerateEntityCreatedDateAndCreatedById(entries, CurrentUserId);
            _dcHelper.GenerateEntityUpdateDateAndUpdatedById(entries, CurrentUserId);

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

            _dcHelper.GenerateEntityCreatedDateAndCreatedById(entries, CurrentUserId);
            _dcHelper.GenerateEntityUpdateDateAndUpdatedById(entries, CurrentUserId);

            return base.SaveChanges();
        }

        #endregion
    }
}