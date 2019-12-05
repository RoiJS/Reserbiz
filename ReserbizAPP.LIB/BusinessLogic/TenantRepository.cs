using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class TenantRepository
        : BaseRepository<Tenant>, ITenantRepository<Tenant>
    {
        public TenantRepository(IReserbizRepository<Tenant> reserbizRepository)
        : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task CreateTenant(Tenant tenant)
        {
            await AddEntity(tenant);
        }

        public async Task<Tenant> GetTenantAsync(int id)
        {
            var tenant = await _reserbizRepository.ClientDbContext.Tenants
                .Include(t => t.ContactPersons)
                .FirstOrDefaultAsync(t => t.Id == id);

            return tenant;
        }

        public async Task<IEnumerable<Tenant>> GetActiveTenantsAsync()
        {
            var activeTenantsFromRepo = await _reserbizRepository.ClientDbContext.Tenants.AsQueryable()
                .Includes(t => t.ContactPersons)
                .Where(t => t.IsActive)
                .ToListAsync();

            return activeTenantsFromRepo;
        }
    }
}