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
    public class TermMiscellaneousController : ControllerBase
    {
        private readonly ITermMiscellaneousRepository<TermMiscellaneous> _termMiscellaneousRepository;
        private readonly IMapper _mapper;
        private readonly ITermRepository<Term> _termRepository;

        public TermMiscellaneousController(ITermMiscellaneousRepository<TermMiscellaneous> termMiscellaneousRepository
            , IMapper mapper, ITermRepository<Term> termRepository)
        {
            _mapper = mapper;
            _termRepository = termRepository;
            _termMiscellaneousRepository = termMiscellaneousRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult<TermMiscellaneousDetailDto>> CreateTermMiscellaneous(int termId,
            TermMiscellaneousForCreationDto termMiscellaneousForCreationDto)
        {
            var termFromRepo = await _termRepository.GetTermAsync(termId);

            if (termFromRepo == null)
                return NotFound("Term not found.");

            var termMiscellaneousToCreate = _mapper.Map<TermMiscellaneous>(termMiscellaneousForCreationDto);

            termFromRepo.TermMiscellaneous.Add(termMiscellaneousToCreate);

            var termMiscellaneousToReturn = _mapper.Map<TermMiscellaneousDetailDto>(termMiscellaneousToCreate);

            await _termRepository.SaveChanges();

            return CreatedAtRoute(
                routeName: nameof(GetTermMiscellaneous),
                routeValues: new { id = termMiscellaneousToCreate.Id },
                value: termMiscellaneousToReturn
            );
        }

        [HttpGet("{id}", Name = "GetTermMiscellaneous")]
        public async Task<ActionResult<TermMiscellaneousDetailDto>> GetTermMiscellaneous(int id)
        {
            var termMiscellaneousFromRepo = await _termMiscellaneousRepository.GetEntity(id).ToObjectAsync();

            if (termMiscellaneousFromRepo == null)
                return NotFound("Term Miscellaneous not found.");

            var termMiscellaneousToReturn = _mapper.Map<TermMiscellaneousDetailDto>(termMiscellaneousFromRepo);

            return Ok(termMiscellaneousToReturn);
        }

        [HttpGet("getAllTermMiscellaneousPerTerm/{termId}")]
        public async Task<ActionResult<IEnumerable<TermMiscellaneousDetailDto>>> GetAllTermMiscellaneous(int termId)
        {
            var termFromRepo = await _termRepository.GetTermAsync(termId);

            var termMiscellaneousToReturn = _mapper.Map<IEnumerable<TermMiscellaneousDetailDto>>(termFromRepo.TermMiscellaneous);

            return Ok(termMiscellaneousToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTermMiscellaneous(int id, TermMiscellaneousForUpdateDto termMiscellaneousForCreationDto)
        {
            var termMiscellaneousFromRepo = await _termMiscellaneousRepository.GetEntity(id).ToObjectAsync();

            if (termMiscellaneousFromRepo == null)
                return NotFound("Term Miscellaneous not found.");

            _mapper.Map(termMiscellaneousForCreationDto, termMiscellaneousFromRepo);

            if (!_termMiscellaneousRepository.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _termMiscellaneousRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating term miscellaneous with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TermMiscellaneousDetailDto>> DeleteTermMiscellaneous(int id)
        {
            var termMiscellaneousFromRepo = await _termMiscellaneousRepository.GetEntity(id).ToObjectAsync();

            if (termMiscellaneousFromRepo == null)
                return NotFound("Term Miscellaneous not found.");

            _termMiscellaneousRepository.DeleteEntity(termMiscellaneousFromRepo);

            var termMiscellaneousToReturn = _mapper.Map<TermMiscellaneousDetailDto>(termMiscellaneousFromRepo);

            if (await _termMiscellaneousRepository.SaveChanges())
                return Ok(termMiscellaneousToReturn);

            throw new Exception($"Deleting term miscellaneous with an id of {id} failed on save.");
        }
    }
}