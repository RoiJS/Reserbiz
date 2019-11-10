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
    public class SpaceTypeController : ControllerBase
    {
        private readonly ISpaceTypeRepository<SpaceType> _spaceTypeRepo;
        private readonly IMapper _mapper;

        public SpaceTypeController(ISpaceTypeRepository<SpaceType> spaceTypeRepo, IMapper mapper)
        {
            _spaceTypeRepo = spaceTypeRepo;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSpaceType(SpaceTypeForCreationDto spaceTypeForCreationDto)
        {
            var spaceTypeToCreate = _mapper.Map<SpaceType>(spaceTypeForCreationDto);

            await _spaceTypeRepo.AddEntity(spaceTypeToCreate);
            await _spaceTypeRepo.SaveChanges();

            var spaceTypeToReturn = _mapper.Map<SpaceTypeDetailDto>(spaceTypeToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetSpaceType),
                routeValues: new { id = spaceTypeToCreate.Id },
                value: spaceTypeToReturn
            );
        }

        [HttpGet("{id}", Name = "GetSpaceType")]
        public async Task<ActionResult<SpaceTypeDetailDto>> GetSpaceType(int id)
        {
            var spaceTypeFromRepo = await _spaceTypeRepo.GetEntityById(id);

            if (spaceTypeFromRepo == null)
                return NotFound("Space type not found.");

            var spaceTypeToReturn = _mapper.Map<SpaceTypeDetailDto>(spaceTypeFromRepo);

            return Ok(spaceTypeToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceTypeDetailDto>>> GetSpaceTypes(int id)
        {
            var spaceTypesFromRepo = await _spaceTypeRepo.GetAllEntities();

            var spaceTypesToReturn = _mapper.Map<IEnumerable<SpaceTypeDetailDto>>(spaceTypesFromRepo);

            return Ok(spaceTypesToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpaceType(int id, SpaceTypeForUpdateDto spaceTypeForUpdateDto)
        {
            var spaceTypeFromRepo = await _spaceTypeRepo.GetEntityById(id);

            _mapper.Map(spaceTypeForUpdateDto, spaceTypeFromRepo);

            if (!_spaceTypeRepo.HasChanged())
                return BadRequest("Nothing was changed.");

            if (await _spaceTypeRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating space type with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpaceTypeDetailDto>> DeleteSpaceType(int id)
        {
            var spaceTypeFromRepo = await _spaceTypeRepo.GetEntityById(id);

            if (spaceTypeFromRepo == null)
                return BadRequest("Space Type does not exists.");

            _spaceTypeRepo.DeleteEntity(spaceTypeFromRepo);

            var spaceTypeToReturn = _mapper.Map<SpaceTypeDetailDto>(spaceTypeFromRepo);

            if (await _spaceTypeRepo.SaveChanges())
                return Ok(spaceTypeToReturn);

            throw new Exception($"Deleting space type with an id of {id} failed on save.");
        }
    }
}