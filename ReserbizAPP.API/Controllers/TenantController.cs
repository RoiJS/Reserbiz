using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly IMapper _mapper;

        public TenantController(ITenantRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<TenantDetailsDto>> CreateTenant(TenantForCreationDto tenantForCreationDto)
        {
            var tenantToCreate = _mapper.Map<Tenant>(tenantForCreationDto);

            await _tenantRepository.CreateTenant(tenantToCreate);

            var tenantToReturn = _mapper.Map<TenantDetailsDto>(tenantToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetTenant),
                routeValues: new { id = tenantToCreate.Id },
                value: tenantToReturn
            );
        }

        [HttpGet("{id}", Name = "GetTenant")]
        public async Task<ActionResult<TenantDetailsDto>> GetTenant(int id)
        {
            var tenantInfo = await _tenantRepository.GetTenantAsync(id);

            if (tenantInfo == null)
                return NotFound("Tenant does not exists.");

            var tenantToReturn = _mapper.Map<TenantDetailsDto>(tenantInfo);

            return Ok(tenantToReturn);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDetailsDto>>> GetAllTenants(int id)
        {
            var tenantsFromRepo = await _tenantRepository.GetAllEntities().ToListObjectAsync();
            var tenantsToReturn = _mapper.Map<IEnumerable<TenantDetailsDto>>(tenantsFromRepo);
            return Ok(tenantsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, TenantForUpdateDto tenantForEditDto)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(id).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            _mapper.Map(tenantForEditDto, tenantFromRepo);

            if (!_tenantRepository.HasChanged())
            {
                return BadRequest("There are no changes to applied.");
            }

            if (!await _tenantRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating tenant with an id of {id} failed on save.");
        }

        [HttpPut("setStatus/{id}/{status}")]
        public async Task<IActionResult> SetTenantStatus(int id, bool status)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(id).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            _tenantRepository.SetEntityStatus(tenantFromRepo, status);

            if (!_tenantRepository.HasChanged())
                return BadRequest("Nothing was changed on the object");

            if (await _tenantRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating tenant status with an id of {id} failed on save.");
        }
    }

}