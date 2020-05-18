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
                .Include(t => t.Contracts)
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

        public async Task<bool> DeleteMultipleTenantsAsync(List<int> tenantIds)
        {
            var selectedTenants = await _reserbizRepository
                .ClientDbContext
                .Tenants
                .Where(t => tenantIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedTenants);
            return await SaveChanges();
        }

        public async Task<IEnumerable<Tenant>> GetTenantsBasedOnNameAsync(string tenantName)
        {
            var activeTenantsFromRepo = _reserbizRepository.ClientDbContext.Tenants.AsQueryable()
                                        .Includes(t => t.Contracts)
                                        .Where(t => !t.IsDelete);

            if (!string.IsNullOrEmpty(tenantName))
            {
                activeTenantsFromRepo = activeTenantsFromRepo.Where(t => t.FirstName.Contains(tenantName) || t.MiddleName.Contains(tenantName) || t.LastName.Contains(tenantName));
            }

            return await activeTenantsFromRepo.ToListAsync();
        }
    }
}