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
    public class TermController : ControllerBase
    {
        private readonly ITermRepository<Term> _termRepo;
        private readonly IMapper _mapper;

        public TermController(ITermRepository<Term> termRepo, IMapper mapper)
        {
            _mapper = mapper;
            _termRepo = termRepo;
        }

        [HttpPost("create")]
        public async Task<ActionResult<TermDetailDto>> CreateTerm(TermForCreationDto termForCreationDto)
        {
            var termToCreate = _mapper.Map<Term>(termForCreationDto);

            await _termRepo.AddTerm(termToCreate);

            var termtoReturn = _mapper.Map<TermDetailDto>(termToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetTerm),
                routeValues: new { id = termToCreate.Id },
                value: termtoReturn
            );
        }


        [HttpGet("{id}", Name = "GetTerm")] 
        public async Task<ActionResult<TermDetailDto>> GetTerm(int id)
        {
            var termFromRepo = await _termRepo.GetTermAsync(id);

            if (termFromRepo == null)
                return BadRequest("Term not found.");

            var termToReturn = _mapper.Map<TermDetailDto>(termFromRepo);

            return Ok(termToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermListDto>>> GetTerms()
        {
            var termsFromRepo = await _termRepo.GetAllEntities().ToListObjectAsync();

            var termToReturn = _mapper.Map<IEnumerable<TermListDto>>(termsFromRepo);

            return Ok(termToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTerm(int id, TermForUpdateDto termForUpdateDto)
        {
            var termFromRepo = await _termRepo.GetEntity(id).ToObjectAsync();

            if (termFromRepo == null)
                return NotFound("Term not found.");

            _mapper.Map(termForUpdateDto, termFromRepo);

            if (!_termRepo.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _termRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating term with an id of {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TermDetailDto>> DeleteTerm(int id)
        {
            var termFromRepo = await _termRepo.GetEntity(id).ToObjectAsync();

            if (termFromRepo == null)
                return NotFound("Term not found.");

            _termRepo.DeleteEntity(termFromRepo);

            var termToReturn = _mapper.Map<TermDetailDto>(termFromRepo);

            if (await _termRepo.SaveChanges())
                return Ok(termToReturn);

            throw new Exception($"Deleting term with an id of {id} failed on save.");
        }
    }
}