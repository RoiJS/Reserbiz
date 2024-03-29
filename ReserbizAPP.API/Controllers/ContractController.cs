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
using ReserbizAPP.LIB.Helpers.Constants;
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
        private readonly ITermVersionRepository<TermVersion> _termVersionRepository;
        private readonly ITermMiscellaneousRepository<TermMiscellaneous> _termMiscellaneousRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContractController(IAccountStatementRepository<AccountStatement> accountStatementRepository,
            IContractRepository<Contract> contractRepository, ITenantRepository<Tenant> tenantRepository,
            ISpaceTypeRepository<SpaceType> spaceTypeRepository, ITermRepository<Term> termRepository,
            ITermVersionRepository<TermVersion> termVersionRepository,
            ITermMiscellaneousRepository<TermMiscellaneous> termMiscellaneousRepository,
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
            _termVersionRepository = termVersionRepository;
            _termMiscellaneousRepository = termMiscellaneousRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost(ApiRoutes.ContractControllerRoutes.CreateContractURL)]
        public async Task<ActionResult<ContractDetailDto>> CreateContract(ContractManageDto contractManageDto)
        {
            var checkContractFromRepo = await _contractRepository.GetContractByCode(contractManageDto.Code);

            if (checkContractFromRepo != null)
            {
                return Conflict($"Contract with code {checkContractFromRepo.Code} is already exists. Please enter a unique contract code.");
            }

            var contractToCreate = _mapper.Map<Contract>(contractManageDto);

            // (1) Save the new contract
            await _contractRepository
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(contractToCreate);

            try
            {
                // (2) This will auto generate statement of accounts for the contrac
                var dbHashName = _httpContextAccessor.HttpContext.Request.Headers["App-Secret-Token"].ToString();
                await _accountStatementRepository.GenerateContractAccountStatementsForRentalBill(dbHashName, contractToCreate.Id, SendAccountStatementModeEnum.Manual);
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

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetContractURL, Name = "GetContract")]
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

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetAllContracts)]
        public async Task<ActionResult<ContractPaginationListDto>> GetAllContracts(string searchKeyword, int tenantId, DateTime activeFrom, DateTime activeTo, DateTime nextDueDateFrom, DateTime nextDueDateTo, bool openContract, SortOrderEnum sortOrder, bool archived, ArchivedContractStatusEnum archivedContractStatus, SortOrderEnum codeSortOrder, int page)
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
                SortOrder = sortOrder,
                ArchivedContractsIncluded = archived,
                ArchivedContractStatus = archivedContractStatus,
                CodeSortOrder = codeSortOrder
            };

            contractsFromRepo = _contractRepository.GetFilteredContracts(contractsFromRepo.ToList(), contractFilter);

            var mappedContracts = _mapper.Map<IEnumerable<ContractListDto>>(contractsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<ContractPaginationListDto>(mappedContracts, page);

            entityPaginationListDto.TotalNumberOfOpenContracts = mappedContracts.Where((m) => m.IsOpenContract).Count();
            entityPaginationListDto.TotalNumberOfExpiredContracts = mappedContracts.Where((m) => m.IsExpired).Count();
            entityPaginationListDto.TotalNumberOfInactiveContracts = mappedContracts.Where((m) => m.IsExpired == false && m.IsActive == false).Count();

            return Ok(entityPaginationListDto);
        }

        [HttpPut(ApiRoutes.ContractControllerRoutes.UpdateContractURL)]
        public async Task<IActionResult> UpdateContract(int contractId, int termId, ContractManageDto contractManageDto)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId)
                                        .ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not exists.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            await CheckAndKeepPreviousTermVersion(contractId, termId, contractManageDto);

            _mapper.Map(contractManageDto, contractFromRepo);

            if (!_contractRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _contractRepository.SaveChanges())
            {
                return NoContent();
            }

            throw new Exception($"Updating contract information with an id of {contractId} failed on save.");
        }

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetContractsPerTenantURL)]
        public async Task<ActionResult<List<ContractListDto>>> GetContractsPerTenant(int tenantId)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(tenantId).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            var contractsPerTenantFromRepo = await _contractRepository.GetContractsPerTenantAsync(tenantId);

            var contractsPerTenantToReturn = _mapper.Map<List<ContractListDto>>(contractsPerTenantFromRepo);

            return Ok(contractsPerTenantToReturn);
        }

        [HttpPut(ApiRoutes.ContractControllerRoutes.SetContractStatusURL)]
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

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetContractAccountStatementsURL)]
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

        [HttpPost(ApiRoutes.ContractControllerRoutes.DeleteMultipleContractsURL)]
        public async Task<IActionResult> DeleteMultipleContracts(List<int> contractIds)
        {
            if (contractIds.Count == 0)
                return BadRequest("Empty contracts id list.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            if (await _contractRepository.DeleteMultipleContractsAsync(contractIds))
                return NoContent();

            throw new Exception($"Error when deleting contracts!");
        }

        [HttpDelete(ApiRoutes.ContractControllerRoutes.DeleteContractURL)]
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

        [HttpPost(ApiRoutes.ContractControllerRoutes.SetMultipleContractsStatusURL)]
        public async Task<IActionResult> SetMultipleContractsStatus(bool status, List<int> entityIds)
        {
            if (entityIds.Count == 0)
                return BadRequest("Empty contracts id list.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            if (await _contractRepository.SetMultipleContractsStatus(entityIds, status))
                return NoContent();

            throw new Exception($"Error occurs processing contracts!");
        }

        [HttpGet(ApiRoutes.ContractControllerRoutes.CheckTermCodeIfExistsURL)]
        public async Task<ActionResult<bool>> CheckTermCodeIfExists(int contractId, string contractCode)
        {
            var termsFromRepo = await _contractRepository.GetAllEntities().ToListObjectAsync();

            return Ok(_contractRepository.CheckContractCodeIfExists(termsFromRepo, contractId, contractCode));
        }

        [HttpGet(ApiRoutes.ContractControllerRoutes.ValidateExpirationDateURL)]
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

        [HttpGet(ApiRoutes.ContractControllerRoutes.CalculateExpirationDateURL)]
        public ActionResult<DateTime> CalculateExpirationDate(DateTime effectiveDate, DurationEnum durationUnit, int durationValue)
        {
            var durationDays = effectiveDate.CalculateDaysBasedOnDuration(durationValue, durationUnit);
            var expirationDate = effectiveDate.AddDays(durationDays);
            return Ok(expirationDate);
        }

        [HttpPut(ApiRoutes.ContractControllerRoutes.SetEncashDepositAmountStatusURL)]
        public async Task<IActionResult> SetEncashDepositAmountStatus(int contractId, bool status)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId).ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not found.");

            if (await _contractRepository.SetEncashDepositAmountStatus(contractFromRepo, status, CurrentUserId))
                return NoContent();

            throw new Exception($"Updating contract status with an id of {contractId} failed on save.");
        }

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetActiveContractsCountURL)]
        public async Task<ActionResult<int>> GetActiveContractsCount()
        {
            var allActiveContractsFromRepoCount = await _contractRepository.GetAllContractsAsync(false);
            return Ok(allActiveContractsFromRepoCount.Count());
        }

        [HttpGet(ApiRoutes.ContractControllerRoutes.GetAllUpcomingDueDateContractsPerMonthURL)]
        public async Task<ActionResult<ContractPaginationListDto>> GetAllUpcomingDueDateContractsPerMonth(int month)
        {
            var contractsFromRepo = await _contractRepository.GetAllUpcomingDueDateContractsPerMonthAsync(month);

            var mappedContracts = _mapper.Map<IEnumerable<ContractListDto>>(contractsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<ContractPaginationListDto>(mappedContracts, 0);

            return Ok(entityPaginationListDto);
        }

        /// <summary>
        /// This saves a copy of the previous term assigned on the contract.
        /// If there are any changes on the term then we create a new copy, this means 
        /// that the previous term details will be ignored but we will keep a copy 
        /// of the termIds assigned to the contracts. This is important so we can keep
        /// track of the versions of the term. This might be useful for future use just in case
        /// there could be a problem on the generated statement of accounts. 
        /// </summary>
        /// <param name="contractId">Contract Id</param>
        /// <param name="contractManageDto">DTO that contains the contract details including the term and term miscellaneous.</param>
        /// <returns></returns>
        private async Task CheckAndKeepPreviousTermVersion(int contractId, int termId, ContractManageDto contractManageDto)
        {
            if (contractManageDto.Term.Id == 0)
            {
                _termVersionRepository.SetCurrentUserId(CurrentUserId);
                // Saves the term id of the previous version
                await _termVersionRepository.AddTermVersion(contractId, termId);
            }
        }
    }
}