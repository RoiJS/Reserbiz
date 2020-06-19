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
    public class TenantController : ReserbizBaseController
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

            await _tenantRepository
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(tenantToCreate);

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
        public async Task<ActionResult<IEnumerable<TenantForListDto>>> GetAllTenants(string tenantName)
        {
            var tenantsFromRepo = await _tenantRepository.GetTenantsBasedOnNameAsync(tenantName);
            var tenantsToReturn = _mapper.Map<IEnumerable<TenantForListDto>>(tenantsFromRepo);
            return Ok(tenantsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(int id, TenantForUpdateDto tenantForUpdateDto)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(id).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            _tenantRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(tenantForUpdateDto, tenantFromRepo);

            if (!_tenantRepository.HasChanged())
            {
                return BadRequest("There are no changes to applied.");
            }

            if (await _tenantRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating tenant with an id of {id} failed on save.");
        }

        [HttpPut("setStatus/{id}/{status}")]
        public async Task<IActionResult> SetTenantStatus(int id, bool status)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(id).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists.");

            _tenantRepository
                .SetCurrentUserId(CurrentUserId)
                .SetEntityStatus(tenantFromRepo, status);

            if (!_tenantRepository.HasChanged())
                return BadRequest("Nothing was changed on the object");

            if (await _tenantRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating tenant status with an id of {id} failed on save.");
        }

        [HttpPost("deleteMultipleTenants")]
        public async Task<IActionResult> DeleteMultipleTenants(List<int> tenantIds)
        {
            if (tenantIds.Count == 0)
                return BadRequest("Empty tenants id list.");

            _tenantRepository.SetCurrentUserId(CurrentUserId);

            if (await _tenantRepository.DeleteMultipleTenantsAsync(tenantIds))
                return NoContent();

            throw new Exception($"Error when deleting tenants!");
        }

        [HttpDelete("deleteTenant")]
        public async Task<IActionResult> DeleteTenant(int tenantId)
        {
            var tenantFromRepo = await _tenantRepository.GetEntity(tenantId).ToObjectAsync();

            if (tenantFromRepo == null)
                return NotFound("Tenant does not exists!");

            _tenantRepository.DeleteEntity(tenantFromRepo);

            if (await _tenantRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Error when deleting tenant with id of ${tenantId}!");
        }
    }
}