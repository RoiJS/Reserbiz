using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<Contract>> GetAllContractsAsync();
        Task<IEnumerable<Contract>> GetContractsPerTenantAsync(int tenantId);
        Task<IEnumerable<Contract>> GetActiveDueContractsPerTenantAsync(int tenantId);
        Task<IEnumerable<Contract>> GetActiveContractsPerTenantAsync(int tenantId);
        List<Contract> GetFilteredContracts(IList<Contract> unfilteredContracts, IContractFilter contractFilter);
    }
}