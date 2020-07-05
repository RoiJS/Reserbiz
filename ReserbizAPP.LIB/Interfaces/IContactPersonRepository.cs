using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContactPersonRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<bool> DeleteMultipleContactPersons(List<int> contactPersonIds);
        Task<IEnumerable<ContactPerson>> GetContactPersonsPerTenant(int tenantId);
    }
}