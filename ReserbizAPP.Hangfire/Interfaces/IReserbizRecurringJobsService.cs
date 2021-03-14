using System.Threading.Tasks;

namespace ReserbizAPP.Hangfire.Interfaces
{
    public interface IReserbizRecurringJobsService
    {
        void RegisterAutoGenerateAccountStatements();
    }
}