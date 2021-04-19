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
    [Route("[controller]")]
    public class SpaceTypeController : ReserbizBaseController
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

            await _spaceTypeRepo
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(spaceTypeToCreate);

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
            var spaceTypeFromRepo = await _spaceTypeRepo.GetSpaceTypeAsync(id);

            if (spaceTypeFromRepo == null)
                return NotFound("Space type not found.");

            var spaceTypeToReturn = _mapper.Map<SpaceTypeDetailDto>(spaceTypeFromRepo);

            return Ok(spaceTypeToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceTypeDetailDto>>> GetSpaceTypes(string spaceTypeName)
        {
            var spaceTypesFromRepo = await _spaceTypeRepo.GetSpaceTypesBasedOnNameAsync(spaceTypeName);

            var spaceTypesToReturn = _mapper.Map<IEnumerable<SpaceTypeDetailDto>>(spaceTypesFromRepo);

            return Ok(spaceTypesToReturn);
        }

        [HttpGet("getSpaceTypeAsOptions")]
        public async Task<ActionResult<IEnumerable<SpaceTypeOptionDto>>> GetSpaceTypesAsOptions()
        {
            var spaceTypesFromRepo = await _spaceTypeRepo.GetSpaceTypesAsOptions();

            var spaceTypeOptions = _mapper.Map<IEnumerable<SpaceTypeOptionDto>>(spaceTypesFromRepo);

            return Ok(spaceTypeOptions);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpaceType(int id, SpaceTypeForUpdateDto spaceTypeForUpdateDto)
        {
            var spaceTypeFromRepo = await _spaceTypeRepo.GetEntity(id).ToObjectAsync();

            _spaceTypeRepo.SetCurrentUserId(CurrentUserId);

            _mapper.Map(spaceTypeForUpdateDto, spaceTypeFromRepo);

            if (!_spaceTypeRepo.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _spaceTypeRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating space type with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpaceTypeDetailDto>> DeleteSpaceType(int id)
        {
            var spaceTypeFromRepo = await _spaceTypeRepo.GetEntity(id).ToObjectAsync();

            if (spaceTypeFromRepo == null)
                return BadRequest("Space Type does not exists.");

            _spaceTypeRepo
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(spaceTypeFromRepo);

            var spaceTypeToReturn = _mapper.Map<SpaceTypeDetailDto>(spaceTypeFromRepo);

            if (await _spaceTypeRepo.SaveChanges())
                return Ok(spaceTypeToReturn);

            throw new Exception($"Deleting space type with an id of {id} failed on save.");
        }

        [HttpPost("deleteMultipleSpaceTypes")]
        public async Task<IActionResult> DeleteMultipleSpaceTypes(List<int> spaceTypeIds)
        {
            if (spaceTypeIds.Count == 0)
                return BadRequest("Empty space type id list.");

            _spaceTypeRepo.SetCurrentUserId(CurrentUserId);

            if (await _spaceTypeRepo.DeleteMultipleSpaceTypesAsync(spaceTypeIds))
                return NoContent();

            throw new Exception($"Error when deleting space types!");
        }
    }
}