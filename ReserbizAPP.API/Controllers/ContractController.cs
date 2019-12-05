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
    public class ContractController : ControllerBase
    {
        private readonly IContractRepository<Contract> _contractRepository;
        private readonly IMapper _mapper;
        private readonly ITenantRepository<Tenant> _tenantRepository;

        public ContractController(IContractRepository<Contract> contractRepository, ITenantRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ContractDetailDto>> CreateContract(ContractForCreationDto contractForCreationDto)
        {
            var contractToCreate = _mapper.Map<Contract>(contractForCreationDto);

            await _contractRepository.CreateContract(contractToCreate);

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

        [HttpGet("getAllContractsPerTenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<ContractListDto>>> GetContractsPerTenant(int tenantId)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(tenantId).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does exists.");

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

            contractFromRepo.IsActive = status;

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
    }
}