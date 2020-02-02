using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReserbizAPP.LIB.Interfaces;

namespace ReserbizAPP.API.Helpers
{
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dataSeedRepository = services.GetService<IDataSeedRepository<IEntity>>();
                dataSeedRepository.SeedData();
            }

            return host;
        }
    }
}