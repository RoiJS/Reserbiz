using System.Diagnostics.CodeAnalysis;
using Hangfire.Dashboard;

namespace ReserbizAPP.API.Helpers.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}