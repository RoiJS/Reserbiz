using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface ITenantRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task CreateTenant(Tenant tenant);

        Task<Tenant> GetTenantAsync(int id);
        Task<IEnumerable<Tenant>> GetActiveTenantsAsync();
        Task<IEnumerable<Tenant>> GetTenantsBasedOnNameAsync(string tenantName);
        Task<bool> DeleteMultipleTenants(List<int> tenantIds); 
    }
}