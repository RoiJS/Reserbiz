using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.Interfaces
{
    public interface IContractRepository<TEntity>
        : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<Contract>> GetAllContractsAsync(bool onlyArchivedContracts);
        Task<IEnumerable<Contract>> GetAllUpcomingDueDateContractsPerMonthAsync(int month);
        Task<IEnumerable<Contract>> GetContractsPerTenantAsync(int tenantId);
        Task<IEnumerable<Contract>> GetActiveDueContractsPerTenantAsync(int tenantId);
        Task<IEnumerable<Contract>> GetActiveContractsPerTenantAsync(int tenantId);
        List<Contract> GetFilteredContracts(IList<Contract> unfilteredContracts, IContractFilter contractFilter);
        Task<bool> DeleteMultipleContractsAsync(List<int> contractIds);
        Task<bool> SetMultipleContractsStatus(List<int> contractIds, bool status);
        bool CheckContractCodeIfExists(IList<Contract> contractList, int contractId, string contractCode);
        bool ValidateExpirationDate(Contract contract, DateTime effectiveDate, DurationEnum durationUnit, int durationValue);
    }
}