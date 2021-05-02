using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContractController : ReserbizBaseController
    {
        private readonly IContractRepository<Contract> _contractRepository;
        private readonly IMapper _mapper;
        private readonly ITenantRepository<Tenant> _tenantRepository;

        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;

        private readonly IPaginationService _paginationService;

        private readonly ISpaceTypeRepository<SpaceType> _spaceTypeRepository;
        private readonly ITermRepository<Term> _termRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContractController(IAccountStatementRepository<AccountStatement> accountStatementRepository,
            IContractRepository<Contract> contractRepository, ITenantRepository<Tenant> tenantRepository,
            ISpaceTypeRepository<SpaceType> spaceTypeRepository, ITermRepository<Term> termRepository,
             IMapper mapper, IPaginationService paginationService,
             IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _accountStatementRepository = accountStatementRepository;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _paginationService = paginationService;
            _spaceTypeRepository = spaceTypeRepository;
            _termRepository = termRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ContractDetailDto>> CreateContract(ContractManageDto contractManageDto)
        {
            var contractToCreate = _mapper.Map<Contract>(contractManageDto);

            // (1) Save the new contract
            await _contractRepository
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(contractToCreate);

            try
            {
                // (2) This will auto generate statement of accounts for the contrac
                var dbHashName = _httpContextAccessor.HttpContext.Request.Headers["App-Secret-Token"].ToString();
                 await _accountStatementRepository.GenerateContractAccountStatements(dbHashName, contractToCreate.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                // (3) This will auto generate penalties per statement account for the contract
                await _accountStatementRepository.GenerateAccountStatementPenalties(contractToCreate.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var contractFromRepo = await _contractRepository
                .GetEntity(contractToCreate.Id)
                .Includes(c => c.AccountStatements)
                .ToObjectAsync();

            var contractToReturn = _mapper.Map<ContractDetailDto>(contractToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetContract),
                routeValues: new { id = contractToReturn.Id },
                value: contractToReturn
            );
        }

        [HttpGet("{id}", Name = "GetContract")]
        public async Task<ActionResult<ContractDetailDto>> GetContract(int id)
        {
            var contractFromRepo = await _contractRepository
                .GetEntity(id)
                .Includes(
                    c => c.AccountStatements,
                    c => c.Tenant,
                    c => c.Term,
                    c => c.Space
                )
                .ToObjectAsync();

            if (contractFromRepo == null)
            {
                return NotFound("Contract not found.");
            }

            var spaceType = await _spaceTypeRepository.GetEntity(contractFromRepo.Term.SpaceTypeId).ToObjectAsync();
            var term = await _termRepository.GetEntity(contractFromRepo.TermId).ToObjectAsync();


            if (spaceType == null)
            {
                return NotFound("Space Type not found.");
            }

            var contractToReturn = _mapper.Map<ContractDetailDto>(contractFromRepo);

            contractToReturn.SpaceTypeName = spaceType.Name;
            contractToReturn.SpaceTypeId = spaceType.Id;
            contractToReturn.TermParentId = term.TermParentId;

            return Ok(contractToReturn);
        }

        [HttpGet("getAllContracts")]
        public async Task<ActionResult<ContractPaginationListDto>> GetAllContracts(string searchKeyword, int tenantId, DateTime activeFrom, DateTime activeTo, DateTime nextDueDateFrom, DateTime nextDueDateTo, bool openContract, SortOrderEnum sortOrder, bool archived, int page)
        {
            var contractsFromRepo = await _contractRepository.GetAllContractsAsync(archived);

            var contractFilter = new ContractFilter
            {
                Code = searchKeyword,
                TenantId = tenantId,
                ActiveFrom = activeFrom,
                ActiveTo = activeTo,
                NextDueDateFrom = nextDueDateFrom,
                NextDueDateTo = nextDueDateTo,
                OpenContract = openContract,
                SortOrder = sortOrder
            };

            contractsFromRepo = _contractRepository.GetFilteredContracts(contractsFromRepo.ToList(), contractFilter);

            var mappedContracts = _mapper.Map<IEnumerable<ContractListDto>>(contractsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<ContractPaginationListDto>(mappedContracts, page);

            entityPaginationListDto.TotalNumberOfOpenContracts = mappedContracts.Where((m) => m.IsOpenContract).Count();
            entityPaginationListDto.TotalNumberOfExpiredContracts = mappedContracts.Where((m) => m.IsExpired).Count();
            entityPaginationListDto.TotalNumberOfInactiveContracts = mappedContracts.Where((m) => m.IsExpired == false && m.IsActive == false).Count();

            return Ok(entityPaginationListDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, ContractManageDto contractManageDto)
        {
            var contractFromRepo = await _contractRepository.GetEntity(id)
                                        .ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not exists.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(contractManageDto, contractFromRepo);

            if (!_contractRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _contractRepository.SaveChanges())
            {
                return NoContent();
            }

            throw new Exception($"Updating contract information with an id of {id} failed on save.");
        }

        [HttpGet("getAllContractsPerTenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<ContractListDto>>> GetContractsPerTenant(int tenantId)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(tenantId).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            var contractsPerTenantFromRepo = await _contractRepository.GetContractsPerTenantAsync(tenantId);

            var contractsPerTenantToReturn = _mapper.Map<IEnumerable<ContractListDto>>(contractsPerTenantFromRepo);

            return Ok(contractsPerTenantToReturn);
        }

        [HttpPut("setStatus/{id}/{status}")]
        public async Task<IActionResult> SetContractStatus(int id, bool status)
        {
            var contractFromRepo = await _contractRepository.GetEntity(id).ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not found.");

            _contractRepository
                .SetCurrentUserId(CurrentUserId)
                .SetEntityStatus(contractFromRepo, status);

            if (!_contractRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _contractRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating contract status with an id of {id} failed on save.");
        }

        [HttpGet("getContractAccountStatements/{id}")]
        public async Task<ActionResult<ContractDetailDto>> GetContractAccountStatements(int id)
        {
            var contractFromRepo = await _contractRepository
                .GetEntity(id)
                .Includes(
                    c => c.Tenant,
                    c => c.Term,
                    c => c.AccountStatements
                )
                .ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not found.");

            var contractToReturn = _mapper.Map<ContractDetailDto>(contractFromRepo);

            return Ok(contractToReturn);
        }

        [HttpPost("deleteMultipleContracts")]
        public async Task<IActionResult> DeleteMultipleContracts(List<int> contractIds)
        {
            if (contractIds.Count == 0)
                return BadRequest("Empty contracts id list.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            if (await _contractRepository.DeleteMultipleContractsAsync(contractIds))
                return NoContent();

            throw new Exception($"Error when deleting contracts!");
        }

        [HttpDelete("deleteContract")]
        public async Task<IActionResult> DeleteContract(int contractId)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId).ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract does not exists!");

            _contractRepository
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(contractFromRepo);

            if (await _contractRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Error when deleting contract with id of ${contractId}!");
        }

        [HttpPost("setMultipleContractsStatus/{status}")]
        public async Task<IActionResult> SetMultipleContractsStatus(bool status, List<int> entityIds)
        {
            if (entityIds.Count == 0)
                return BadRequest("Empty contracts id list.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            if (await _contractRepository.SetMultipleContractsStatus(entityIds, status))
                return NoContent();

            throw new Exception($"Error occurs processing contracts!");
        }

        [HttpGet("checkContractCodeIfExists/{contractId}/{contractCode}")]
        public async Task<ActionResult<bool>> CheckTermCodeIfExists(int contractId, string contractCode)
        {
            var termsFromRepo = await _contractRepository.GetAllEntities().ToListObjectAsync();

            return Ok(_contractRepository.CheckContractCodeIfExists(termsFromRepo, contractId, contractCode));
        }

        [HttpGet("validateExpirationDate/{contractId}/{effectiveDate}/{durationUnit}/{durationValue}")]
        public async Task<ActionResult<bool>> ValidateExpirationDate(int contractId, DateTime effectiveDate, DurationEnum durationUnit, int durationValue)
        {
            var contractFromRepo = await _contractRepository
                .GetEntity(contractId)
                .Includes(
                    c => c.AccountStatements,
                    c => c.Tenant,
                    c => c.Term
                )
                .ToObjectAsync();

            if (contractFromRepo == null)
            {
                return NotFound("Contract does not exists!");
            }

            var result = _contractRepository.ValidateExpirationDate(contractFromRepo, effectiveDate, durationUnit, durationValue);

            return Ok(result);
        }

        [HttpGet("calculateExpirationDate/{effectiveDate}/{durationUnit}/{durationValue}")]
        public ActionResult<DateTime> CalculateExpirationDate(DateTime effectiveDate, DurationEnum durationUnit, int durationValue)
        {
            var durationDays = effectiveDate.CalculateDaysBasedOnDuration(durationValue, durationUnit);
            var expirationDate = effectiveDate.AddDays(durationDays);
            return Ok(expirationDate);
        }

        [HttpPut("setEncashDepositAmountStatus/{contractId}/{status}")]
        public async Task<IActionResult> SetEncashDepositAmountStatus(int contractId, bool status)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId).ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not found.");

            if (await _contractRepository.SetEncashDepositAmountStatus(contractFromRepo, status, CurrentUserId))
                return NoContent();

            throw new Exception($"Updating contract status with an id of {contractId} failed on save.");
        }

        [HttpGet("getActiveContractsCount")]
        public async Task<ActionResult<int>> GetActiveContractsCount()
        {
            var allActiveContractsFromRepoCount = await _contractRepository.GetAllContractsAsync(false);
            return Ok(allActiveContractsFromRepoCount.Count());
        }

        [HttpGet("getAllUpcomingDueDateContractsPerMonth")]
        public async Task<ActionResult<ContractPaginationListDto>> GetAllUpcomingDueDateContractsPerMonth(int month)
        {
            var contractsFromRepo = await _contractRepository.GetAllUpcomingDueDateContractsPerMonthAsync(month);

            var mappedContracts = _mapper.Map<IEnumerable<ContractListDto>>(contractsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<ContractPaginationListDto>(mappedContracts, 0);

            return Ok(entityPaginationListDto);
        }
    }
}