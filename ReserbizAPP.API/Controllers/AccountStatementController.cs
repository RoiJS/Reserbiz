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
        private readonly IClientSettingsRepository<ClientSettings> _clientSettingsRepository;

        public AccountStatementController(IAccountStatementRepository<AccountStatement> accountStatementRepository,
            ITenantRepository<Tenant> tenantRepository, IContractRepository<Contract> contractRepository, IClientSettingsRepository<ClientSettings> clientSettingsRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _accountStatementRepository = accountStatementRepository;
            _clientSettingsRepository = clientSettingsRepository;
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

        /// <summary>
        /// This method is called by a background services which scheduled to run daily.
        /// This will auto generate account statements per contract.
        /// It has been set to AllowAnonymous because scheduler does not
        /// have to be authenticated to called this method.
        /// </summary>
        /// <returns></returns>
        // [AllowAnonymous]
        // [HttpPost("autoGenerateContractAccountStatements")]
        // public async Task<IActionResult> AutoGenerateContractAccountStatements()
        // {
        //     var activeTenantsFromRepo = await _tenantRepository.GetActiveTenantsAsync();

        //     foreach (var tenant in activeTenantsFromRepo)
        //     {
        //         var activeTenantDueContracts = await _contractRepository.GetActiveDueContractsPerTenantAsync(tenant.Id);

        //         foreach (var contract in activeTenantDueContracts)
        //         {
        //             if (!contract.IsExpired)
        //             {
        //                 var newContractAccountStatement = _accountStatementRepository.RegisterNewAccountStament(contract);
        //                 contract.AccountStatements.Add(newContractAccountStatement);

        //                 try
        //                 {
        //                     await _contractRepository.SaveChanges();
        //                 }
        //                 catch (Exception exception)
        //                 {
        //                     throw new Exception($"Registering new account statement failed on save. Error messsage: {exception.Message}");
        //                 }
        //             }
        //         }
        //     }

        //     return Ok();
        // }


        [AllowAnonymous]
        [HttpPost("autoGenerateContractAccountStatements")]
        public async Task<IActionResult> AutoGenerateContractAccountStatements()
        {
            var currentDate = DateTime.Now;
            var clientSettingsFromRepo = await _clientSettingsRepository.GetClientSettings();
            var activeTenantsFromRepo = await _tenantRepository.GetActiveTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    while (contract.IsDueForGeneratingAccountStatement(clientSettingsFromRepo.GenerateAccountStatementDaysBeforeValue))
                    {
                        var newContractAccountStatement = _accountStatementRepository.RegisterNewAccountStament(contract);
                        contract.AccountStatements.Add(newContractAccountStatement);
                    }

                    try
                    {
                        await _contractRepository.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"Registering new account statement failed on save. Error messsage: {exception.Message}");
                    }
                }
            }

            return Ok();
        }


        /// <summary>
        /// This method will auto register new due penalties per account statement.
        /// This function will be called by a background services on the client application.
        /// </summary>
        /// <returns></returns>
        // [AllowAnonymous]
        // [HttpPost("autoGenerateContractAccountStatementPenalties")]
        // public async Task<IActionResult> AutoGenerateAccountStatementPenalties()
        // {
        //     var activeTenantsFromRepo = await _tenantRepository.GetActiveTenantsAsync();

        //     foreach (var tenant in activeTenantsFromRepo)
        //     {
        //         var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

        //         foreach (var contract in activeTenantContracts)
        //         {
        //             var activeContractDueAccountStatements = await _accountStatementRepository.GetActiveDueAccountStatementsPerContractAsync(contract.Id);

        //             foreach (var accountStatement in activeContractDueAccountStatements)
        //             {
        //                 if (!accountStatement.IsFullyPaid)
        //                 {
        //                     var newPenaltyItem = _accountStatementRepository.RegisterNewPenaltyItem(accountStatement);
        //                     accountStatement.PenaltyBreakdowns.Add(newPenaltyItem);

        //                     try
        //                     {
        //                         await _accountStatementRepository.SaveChanges();
        //                     }
        //                     catch (Exception exception)
        //                     {
        //                         throw new Exception($"Registering new penalty details failed on save. Error messsage: {exception.Message}");
        //                     }
        //                 }
        //             }
        //         }
        //     }

        //     return Ok();
        // }

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
                    var activeContractAccountStatements = await _accountStatementRepository.GetActiveAccountStatementsPerContractAsync(contract.Id);

                    foreach (var accountStatement in activeContractAccountStatements)
                    {
                        if (!accountStatement.IsPenaltySettingActive) continue;

                        while (accountStatement.IsValidForGeneratingPenalty)
                        {
                            var newPenaltyItem = _accountStatementRepository.RegisterNewPenaltyItem(accountStatement);
                            accountStatement.PenaltyBreakdowns.Add(newPenaltyItem);
                        }

                        try
                        {
                            await _accountStatementRepository.SaveChanges();
                        }
                        catch (Exception exception)
                        {
                            throw new Exception($"Registering new penalty details failed on save. Error messsage: {exception.Message}");
                        }
                    }
                }
            }

            return Ok();
        }
    }
}