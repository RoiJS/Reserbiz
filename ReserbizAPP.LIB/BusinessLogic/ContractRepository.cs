using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateContract(Contract contract)
        {
            await AddEntity(contract);
        }

        public async Task<IEnumerable<Contract>> GetAllContractsAsync()
        {
            var contractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts.ToListAsync();
            return contractsPerTenantFromRepo;
        }
        
        public async Task<IEnumerable<Contract>> GetContractsPerTenantAsync(int tenantId)
        {
            var contractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                                .Where(c => c.TenantId == tenantId)
                                                .ToListAsync();

            return contractsPerTenantFromRepo;
        }

        public async Task<IEnumerable<Contract>> GetActiveDueContractsPerTenantAsync(int tenantId)
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
                                                )
                                                .ToListAsync();

            //  activeContractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.AccountStatements
            //     .Include(a => a.PaymentBreakdowns).ThenInclude(p => p.ReceivedBy).ThenInclude(p => p.PersonFullName).Include(c => c.)

            var activeDueContracts = activeContractsPerTenantFromRepo.Where(c => c.IsDueForGeneratingAccountStatement(clientSettingsFromRepo.GenerateAccountStatementDaysBeforeValue));

            return activeContractsPerTenantFromRepo;
        }
        
        public async Task<IEnumerable<Contract>> GetActiveContractsPerTenantAsync(int tenantId)
        {
            var clientSettingsFromRepo = await _clientSettingsRepository.GetClientSettings();
            var activeContractsPerTenantFromRepo = await _reserbizRepository.ClientDbContext.Contracts
                                                .Where(c =>
                                                    c.IsActive
                                                    && c.TenantId == tenantId
                                                )
                                                .ToListAsync();

            return activeContractsPerTenantFromRepo;
        }
    }
}