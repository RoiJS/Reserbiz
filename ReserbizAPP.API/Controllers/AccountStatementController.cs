using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System.Collections.Generic;
using System.Security.Claims;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountStatementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly IContractRepository<Contract> _contractRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;

        public AccountStatementController(IAccountStatementRepository<AccountStatement> accountStatementRepository,
            ITenantRepository<Tenant> tenantRepository, IContractRepository<Contract> contractRepository, IClientSettingsRepository<ClientSettings> clientSettingsRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _accountStatementRepository = accountStatementRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountStatementDetailsDto>> GetAccountStatement(int id)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetAccountStatementAsync(id);

            if (accountStatementFromRepo == null)
                return NotFound("Account Statement not found.");

            var accountStatementToReturn = _mapper.Map<AccountStatementDetailsDto>(accountStatementFromRepo);

            return Ok(accountStatementToReturn);
        }

        [HttpGet("getAccountStatementsPerContract/{contractId}")]
        public async Task<ActionResult<IEnumerable<AccountStatementForListDto>>> GetAccountStatementsPerContract(int contractId)
        {
            var accountStatementsFromRepo = await _accountStatementRepository.GetActiveAccountStatementsPerContractAsync(contractId);

            var accountStatementToReturn = _mapper.Map<IEnumerable<AccountStatementForListDto>>(accountStatementsFromRepo);

            return Ok(accountStatementToReturn);
        }

        [AllowAnonymous]
        [HttpPost("autoGenerateContractAccountStatements")]
        public async Task<IActionResult> AutoGenerateContractAccountStatements()
        {
            var activeTenantsFromRepo = await _tenantRepository.GetActiveTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    try
                    {
                        await _accountStatementRepository.GenerateContractAccountStatements(contract.Id);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("autoGenerateContractAccountStatementPenalties")]
        public async Task<IActionResult> AutoGenerateAccountStatementPenalties()
        {
            var activeTenantsFromRepo = await _tenantRepository.GetActiveTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    try
                    {
                        await _accountStatementRepository.GenerateAccountStatementPenalties(contract.Id);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return Ok();
        }
    }
}