using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    }
}