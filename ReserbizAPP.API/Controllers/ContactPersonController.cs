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
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContactPersonController : ReserbizBaseController
    {
        private readonly IContactPersonRepository<ContactPerson> _contactPersonRepository;
        private readonly IMapper _mapper;
        private readonly ITenantRepository<Tenant> _tenantRepository;

        public ContactPersonController(IContactPersonRepository<ContactPerson> contactPersonRepository,
            ITenantRepository<Tenant> tenantRepository, IMapper mapper)
        {
            _contactPersonRepository = contactPersonRepository;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ContactPersonDetailDto>> CreateContactPerson(int tenantId, ContactPersonForCreationDto contactPersonForCreationDto)
        {
            var tenantFromRepo = await _tenantRepository.GetTenantAsync(tenantId);

            if (tenantFromRepo == null)
                return NotFound("Tenant information not found.");

            _tenantRepository.SetCurrentUserId(CurrentUserId);

            var contactPersonToCreate = _mapper.Map<ContactPerson>(contactPersonForCreationDto);

            tenantFromRepo.ContactPersons.Add(contactPersonToCreate);

            if (!await _tenantRepository.SaveChanges())
                return BadRequest("Failed to add contact person");

            var contactPersonToReturn = _mapper.Map<ContactPersonDetailDto>(contactPersonToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetContactPerson),
                routeValues: new { id = contactPersonToCreate.Id },
                value: contactPersonToReturn
            );
        }

        [HttpGet("{id}", Name = "GetContactPerson")]
        public async Task<ActionResult<ContactPersonDetailDto>> GetContactPerson(int id)
        {
            var contactPersonFromRepo = await _contactPersonRepository.GetEntity(id).ToObjectAsync();

            if (contactPersonFromRepo == null)
                return NotFound("Contact person not exists.");

            var contactPersonToReturn = _mapper.Map<ContactPersonDetailDto>(contactPersonFromRepo);

            return Ok(contactPersonToReturn);
        }

        [HttpGet("getAllContactPersonsPerTenant/{tenantId}")]
        public async Task<ActionResult<IEnumerable<ContactPersonDetailDto>>> GetAllContactPersons(int tenantId)
        {
            var contactPersonsFromRepo = await _contactPersonRepository.GetContactPersonsPerTenant(tenantId);

            var contactPersonsToReturn = _mapper.Map<IEnumerable<ContactPersonDetailDto>>(contactPersonsFromRepo);

            return Ok(contactPersonsToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactPerson(int id, ContactPersonForUpdateDto contactPersonForUpdateDto)
        {
            var contactPersonFromRepo = await _contactPersonRepository.GetEntity(id).ToObjectAsync();

            if (contactPersonFromRepo == null)
                return NotFound("Contact Person not found.");

            _contactPersonRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(contactPersonForUpdateDto, contactPersonFromRepo);

            if (!_contactPersonRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _contactPersonRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating contact person with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactPersonDetailDto>> DeleteContactPerson(int id)
        {
            var contactPersonFromRepo = await _contactPersonRepository.GetEntity(id).ToObjectAsync();

            if (contactPersonFromRepo == null)
                return NotFound("Contact person not found.");

            _contactPersonRepository
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(contactPersonFromRepo);

            var contactPersonToReturn = _mapper.Map<ContactPersonDetailDto>(contactPersonFromRepo);

            if (await _contactPersonRepository.SaveChanges())
                return Ok(contactPersonToReturn);

            throw new Exception($"Deleting contact person with an id of {id} failed on save.");
        }

        [HttpPost("deleteMultipleContactPersons")]
        public async Task<IActionResult> DeleteMultipleContactPersons(List<int> contactPersonIds)
        {
            if (contactPersonIds.Count == 0)
                return BadRequest("Empty contact person id list.");

            _contactPersonRepository.SetCurrentUserId(CurrentUserId);

            if (await _contactPersonRepository.DeleteMultipleContactPersons(contactPersonIds))
                return NoContent();

            throw new Exception($"Error when deleting contact persons!");
        }
    }
}