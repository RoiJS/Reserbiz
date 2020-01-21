
namespace ReserbizAPP.LIB.Interfaces
{
    public interface IDataSeedRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        void SeedAccounts();
        void SeedTenants();
        void SeedContactPersonPerTenant();
        void SeedSpaceTypes();
        void SeedTerms();
        void SeedMiscellaneousPerTerm();
        void SeedContracts();
        void SeedAccountStatementsPerContract();
        void SeedPenaltyBreakdownsPerAccountStatement();
        void SeedPaymentBreakdownsPerAccountStatement();
        void SeedSettings();
    }
}