using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ContractRepository
        : BaseRepository<Contract>, IContractRepository<Contract>
    {
        private readonly IClientSettingsRepository<ClientSettings> _clientSettingsRepository;

        public ContractRepository(IReserbizRepository<Contract> reserbizRepository,
            IClientSettingsRepository<ClientSettings> clientSettingsRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {
            _clientSettingsRepository = clientSettingsRepository;
        }

        public ContractRepository() : base()
        {

        }

        public async Task<IEnumerable<Contract>> GetAllContractsAsync(bool onlyArchivedContracts)
        {
            var allActiveContractsFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                            .AsQueryable()
                                            .Includes(
                                                c => c.Term,
                                                c => c.Tenant,
                                                c => c.AccountStatements
                                            )
                                            .ToListAsync();

            if (onlyArchivedContracts == true)
            {
                allActiveContractsFromRepo = allActiveContractsFromRepo
                    .Where(c => c.IsArchived == true && c.IsDelete == false)
                    .ToList();
            }
            else
            {
                allActiveContractsFromRepo = allActiveContractsFromRepo
                    .Where(c => c.IsArchived == false && c.IsDelete == false)
                    .ToList();
            }

            return allActiveContractsFromRepo
                .OrderBy(c => c.NextDueDate)
                .ToList();
        }

        public async Task<IEnumerable<Contract>> GetAllUpcomingDueDateContractsPerMonthAsync(int month)
        {
            var allActiveContractsFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                            .AsQueryable()
                                            .Includes(
                                                c => c.Term,
                                                c => c.Tenant,
                                                c => c.AccountStatements
                                            )
                                            .ToListAsync();

            allActiveContractsFromRepo = allActiveContractsFromRepo
                .Where(
                    c => c.IsArchived == false &&
                    c.IsDelete == false &&
                    c.NextDueDate.Month == month &&
                    c.NextDueDate.Year == DateTime.Now.Year &&
                    c.NextDueDate > DateTime.Now
                )
                .OrderBy(c => c.NextDueDate)
                .ToList();

            return allActiveContractsFromRepo;
        }

        public async Task<IEnumerable<Contract>> GetContractsPerTenantAsync(int tenantId)
        {
            var contractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                                .Where(c => c.TenantId == tenantId && c.IsDelete == false)
                                                .ToListAsync();

            return contractsPerTenantFromRepo;
        }

        public async Task<IEnumerable<Contract>> GetActiveDueContractsPerTenantAsync(int tenantId)
        {
            var activeContractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                                .AsQueryable()
                                                .Includes(
                                                    c => c.Term,
                                                    c => c.Term.TermMiscellaneous,
                                                    c => c.AccountStatements
                                                )
                                                .Where(c =>
                                                    c.IsActive
                                                    && c.TenantId == tenantId
                                                    && c.IsDelete == false
                                                )
                                                .ToListAsync();

            //  activeContractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.AccountStatements
            //     .Include(a => a.PaymentBreakdowns).ThenInclude(p => p.ReceivedBy).ThenInclude(p => p.PersonFullName).Include(c => c.)

            var activeDueContracts = activeContractsPerTenantFromRepo.Where(c => c.IsDueForGeneratingAccountStatement);

            return activeContractsPerTenantFromRepo;
        }

        public async Task<IEnumerable<Contract>> GetActiveContractsPerTenantAsync(int tenantId)
        {
            var clientSettingsFromRepo = await _clientSettingsRepository.GetClientSettings();
            var activeContractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                                .AsQueryable()
                                                .Includes(
                                                    c => c.Term,
                                                    c => c.Term.TermMiscellaneous,
                                                    c => c.AccountStatements
                                                )
                                                .Where(c =>
                                                    c.IsActive
                                                    && c.TenantId == tenantId
                                                    && c.IsDelete == false
                                                )
                                                .ToListAsync();

            return activeContractsPerTenantFromRepo;
        }

        public List<Contract> GetFilteredContracts(IList<Contract> unfilteredContracts, IContractFilter contractFilter)
        {
            var filteredContracts = unfilteredContracts;

            // Filter by contract code
            if (!String.IsNullOrEmpty(contractFilter.Code))
            {
                filteredContracts = filteredContracts.Where(c => c.Code.Contains(contractFilter.Code)).ToList();
            }

            // Filter by tenant Id
            if (contractFilter.TenantId != 0)
            {
                filteredContracts = filteredContracts.Where(c => c.TenantId == contractFilter.TenantId).ToList();
            }

            // Filter contracts where the filter ActiveFrom should 
            // be less than or equal to contract Expiration Date
            if (contractFilter.ActiveFrom != DateTime.MinValue)
            {
                filteredContracts = filteredContracts.Where(c => !(contractFilter.ActiveFrom > c.ExpirationDate)).ToList();
            }

            // Filter contracts where the filter ActiveTo should 
            // be greater than or equal to contract Expiration Date
            if (contractFilter.ActiveTo != DateTime.MinValue)
            {
                filteredContracts = filteredContracts.Where(c => !(contractFilter.ActiveTo < c.EffectiveDate)).ToList();
            }

            // Filter contracts where next due date is 
            // greater than or equal to filter NextDueDateFrom
            if (contractFilter.NextDueDateFrom != DateTime.MinValue)
            {
                filteredContracts = filteredContracts.Where(c => c.NextDueDate >= contractFilter.NextDueDateFrom).ToList();
            }

            // Filter contracts where next due date is 
            // less than or equal to filter NextDueDateTo
            if (contractFilter.NextDueDateTo != DateTime.MinValue)
            {
                filteredContracts = filteredContracts.Where(c => c.NextDueDate <= contractFilter.NextDueDateTo).ToList();
            }

            // Filter open contracts 
            if (contractFilter.OpenContract)
            {
                filteredContracts = filteredContracts.Where(c => c.IsOpenContract).ToList();
            }

            // Set sort order based on next due date
            // Sort order is ascending by default
            if (contractFilter.SortOrder == SortOrderEnum.Ascending)
            {
                return filteredContracts
                    .OrderBy(c => c.NextDueDate)
                    .ToList();
            }
            else
            {
                return filteredContracts
                    .OrderByDescending(c => c.NextDueDate)
                    .ToList();
            }
        }

        public async Task<bool> DeleteMultipleContractsAsync(List<int> contractIds)
        {
            var selectedContracts = await _reserbizRepository
                .ClientDbContext
                .Contracts
                .Where(c => contractIds.Contains(c.Id)).ToListAsync();

            DeleteMultipleEntities(selectedContracts);
            return await SaveChanges();
        }

        public async Task<bool> SetMultipleContractsStatus(List<int> contractIds, bool status)
        {
            var selectedContracts = await _reserbizRepository
                .ClientDbContext
                .Contracts
                .Where(c => contractIds.Contains(c.Id)).ToListAsync();

            SetMultipleEntitiesStatus(selectedContracts, status);

            return await SaveChanges();
        }

        public bool CheckContractCodeIfExists(IList<Contract> contractList, int contractId, string contractCode)
        {
            var termWithTheSameCode = contractList
                                .Where(t => (contractId != 0 && (t.Id != contractId && t.Code == contractCode)) || (contractId == 0 && t.Code == contractCode))
                                .Count();

            return termWithTheSameCode > 0;
        }

        public bool ValidateExpirationDate(Contract contract, DateTime effectiveDate, DurationEnum durationUnit, int durationValue)
        {
            // Contract has no statement of accounts 
            // at the moment is valid
            if (contract.AccountStatements.Count == 0)
            {
                return true;
            }

            var contractNextDueDate = contract.NextDueDate;
            var durationDays = effectiveDate.CalculateDaysBasedOnDuration(durationValue, durationUnit);
            var expirationDate = effectiveDate.AddDays(durationDays);

            // The new expiration date is greater than or
            // equal with contract next due date
            // is valid
            if (expirationDate >= contractNextDueDate)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SetEncashDepositAmountStatus(Contract contract, bool status, int currentUserId)
        {
            contract.EncashDepositAmount = status;
            contract.EncashedDepositAmountByAccountId = currentUserId;
            contract.EncashedDepositAmountDateTime = DateTime.Now;

            return await SaveChanges();
        }
    }
}