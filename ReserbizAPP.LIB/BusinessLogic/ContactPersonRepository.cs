using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ContactPersonRepository
        : BaseRepository<ContactPerson>, IContactPersonRepository<ContactPerson>
    {

        public ContactPersonRepository(IReserbizRepository<ContactPerson> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }

        public async Task<IEnumerable<ContactPerson>> GetContactPersonsPerTenant(int tenantId)
        {
            var activeContactPersonsFromRepo = await _reserbizRepository
                .ClientDbContext
                .ContactPersons
                .Where(c => c.TenantId == tenantId && c.IsDelete == false).ToListAsync();

            return activeContactPersonsFromRepo;
        }

        public async Task<bool> DeleteMultipleContactPersons(List<int> contactPersonIds)
        {
            var selectedContactPersons = await _reserbizRepository
                .ClientDbContext
                .ContactPersons
                .Where(t => contactPersonIds.Contains(t.Id)).ToListAsync();

            DeleteMultipleEntities(selectedContactPersons);
            return await SaveChanges();
        }
    }
}