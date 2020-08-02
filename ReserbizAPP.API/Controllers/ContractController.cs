using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System.Linq;

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
        private readonly IPaginationService _paginationService;

        public ContractController(IContractRepository<Contract> contractRepository, ITenantRepository<Tenant> tenantRepository, IMapper mapper, IPaginationService paginationService)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _paginationService = paginationService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ContractDetailDto>> CreateContract(ContractForCreationDto contractForCreationDto)
        {
            var contractToCreate = _mapper.Map<Contract>(contractForCreationDto);

            await _contractRepository
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(contractToCreate);

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
                                                c => c.Term
                                            )
                                            .ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not found.");

            var contractToReturn = _mapper.Map<ContractDetailDto>(contractFromRepo);

            return Ok(contractToReturn);
        }

        [HttpGet("getAllContracts")]
        public async Task<ActionResult<EntityPaginationListDto>> GetAllContracts(string code, int tenantId, DateTime activeFrom, DateTime activeTo, DateTime nextDueDateFrom, DateTime nextDueDateTo, bool openContract, int page)
        {
            var contractsFromRepo = await _contractRepository.GetAllActiveContractsAsync();

            var contractFilter = new ContractFilter
            {
                Code = code,
                TenantId = tenantId,
                ActiveFrom = activeFrom,
                ActiveTo = activeTo,
                NextDueDateFrom = nextDueDateFrom,
                NextDueDateTo = nextDueDateTo,
                OpenContract = openContract
            };

            contractsFromRepo = _contractRepository.GetFilteredContracts(contractsFromRepo.ToList(), contractFilter);

            var sampleLargeList = contractsFromRepo
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo)
                                .Concat(contractsFromRepo);

            var mappedContracts = _mapper.Map<IEnumerable<ContractListDto>>(sampleLargeList);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<ContractPaginationListDto>(mappedContracts, page);

            entityPaginationListDto.TotalNumberOfOpenContracts = mappedContracts.Where((m) => m.IsOpenContract).Count();

            return Ok(entityPaginationListDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, ContractForUpdateDto contractForUpdateDto)
        {

            if (id != contractForUpdateDto.Id)
                return BadRequest("Contract id does not match.");

            var contractFromRepo = await _contractRepository.GetEntity(id)
                                                            .Includes(c => c.AccountStatements)
                                                            .ToObjectAsync();

            if (contractFromRepo == null)
                return NotFound("Contract not exists.");

            if (contractFromRepo.AccountStatements.Count > 0)
                return BadRequest("Contract cannot be updated anymore because it has already account statement attached to it.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(contractForUpdateDto, contractFromRepo);

            if (!_contractRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _contractRepository.SaveChanges())
                return NoContent();

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
        public async Task<IActionResult> SetMultipleContractsStatus( bool status, List<int> entityIds)
        {
            if (entityIds.Count == 0)
                return BadRequest("Empty contracts id list.");

            _contractRepository.SetCurrentUserId(CurrentUserId);

            if (await _contractRepository.SetMultipleContractsStatus(entityIds, status))
                return NoContent();

            throw new Exception($"Error occurs processing contracts!");
        }
    }
}