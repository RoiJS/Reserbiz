using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SpaceController : ReserbizBaseController
    {
        private readonly ISpaceRepository<Space> _spaceRepository;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        public SpaceController(ISpaceRepository<Space> spaceRepository, IMapper mapper, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _mapper = mapper;
            _spaceRepository = spaceRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSpaceType(SpaceForCreationDto spaceForCreationDto)
        {
            var spaceToCreate = _mapper.Map<Space>(spaceForCreationDto);

            await _spaceRepository
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(spaceToCreate);

            var spaceToReturn = _mapper.Map<SpaceDetailDto>(spaceToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetSpace),
                routeValues: new { id = spaceToCreate.Id },
                value: spaceToReturn
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpace(int id, SpaceForUpdateDto spaceForUpdateDto)
        {
            var spaceFromRepo = await _spaceRepository.GetEntity(id).ToObjectAsync();

            _spaceRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(spaceForUpdateDto, spaceFromRepo);

            if (!_spaceRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _spaceRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating space with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SpaceDetailDto>> DeleteSpace(int id)
        {
            var spaceFromRepo = await _spaceRepository.GetEntity(id).ToObjectAsync();

            if (spaceFromRepo == null)
                return BadRequest("Space does not exists.");

            _spaceRepository
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(spaceFromRepo);

            var spaceTypeToReturn = _mapper.Map<SpaceDetailDto>(spaceFromRepo);

            if (await _spaceRepository.SaveChanges())
                return Ok(spaceTypeToReturn);

            throw new Exception($"Deleting space with an id of {id} failed on save.");
        }

        [HttpPost("deleteMultipleSpaces")]
        public async Task<IActionResult> DeleteMultipleSpac(List<int> spaceIds)
        {
            if (spaceIds.Count == 0)
                return BadRequest("Empty space id list.");

            _spaceRepository.SetCurrentUserId(CurrentUserId);

            if (await _spaceRepository.DeleteMultipleSpacesAsync(spaceIds))
                return NoContent();

            throw new Exception($"Error when deleting space!");
        }

        [HttpGet("{id}", Name = "GetSpace")]
        public async Task<ActionResult<SpaceDetailDto>> GetSpace(int id)
        {
            var spaceFromRepo = await _spaceRepository.GetSpaceAsync(id);

            if (spaceFromRepo == null)
                return NotFound("Space not found.");

            var spaceToReturn = _mapper.Map<SpaceDetailDto>(spaceFromRepo);

            return Ok(spaceToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<SpacePaginationListDto>> GetSpaces(string searchKeyword, int page)
        {
            var spacesFromRepo = await _spaceRepository.GetAllActiveSpaces();

            var spaceFilter = new SpaceFilter
            {
                Description = searchKeyword,
            };

            spacesFromRepo = _spaceRepository.GetFilteredSpaces(spacesFromRepo.ToList(), spaceFilter);

            var mappedSpaces = _mapper.Map<IEnumerable<SpaceDetailDto>>(spacesFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<SpacePaginationListDto>(mappedSpaces, page);
            return Ok(entityPaginationListDto);
        }

        [HttpGet("getSpaceAsOptions")]
        public async Task<ActionResult<IEnumerable<SpaceOptionDto>>> GetSpacesAsOptions()
        {
            var spacesFromRepo = await _spaceRepository.GetSpacesAsOptions();

            var spaceOptions = _mapper.Map<IEnumerable<SpaceOptionDto>>(spacesFromRepo);

            return Ok(spaceOptions);
        }

        [HttpGet("getAvailableSpacesCount")]
        public async Task<ActionResult<int>> GetAvailableSpacesCount()
        {
            var spacesFromRepo = await _spaceRepository.GetAllEntities()
                                                       .Includes(s => s.Contracts)
                                                       .ToListObjectAsync();

            var availableSpacesCount = spacesFromRepo.Where(s => s.IsNotOccupied)
                                                     .Count();
            return Ok(availableSpacesCount);
        }

    }
}