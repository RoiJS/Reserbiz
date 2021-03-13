using Hangfire;
using Microsoft.AspNetCore.Builder;
using ReserbizAPP.Hangfire.Helpers.Services;

namespace ReserbizAPP.Hangfire.Helpers.Extensions
{
    public static class ReserbizHangfireTaskSchedulers
    {

        public static void RegisterReserbizRecurringJobs(this IApplicationBuilder app)
        {
            // Register Recurring Job for auto generating account statement
            // RecurringJob.AddOrUpdate("job-auto-generate-account-statements", () => new ReserbizRecurringJobsService(appSettings, clientRepository).RegisterAutoGenerateAccountStatements(), Cron.Minutely);
            RecurringJob.AddOrUpdate<ReserbizRecurringJobsService>("job-auto-generate-account-statements", (r) => r.RegisterAutoGenerateAccountStatements(), Cron.Hourly);
        }
    }
}