using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace ReserbizAPP.Hangfire.Helpers.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}