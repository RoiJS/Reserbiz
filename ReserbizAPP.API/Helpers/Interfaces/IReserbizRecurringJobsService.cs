namespace ReserbizAPP.API.Helpers.Interfaces
{
    public interface IReserbizRecurringJobsService
    {
        void RegisterAutoGenerateAccountStatements();

        void RegisterAutoRemoveExpiredRefreshTokens();
    }
}