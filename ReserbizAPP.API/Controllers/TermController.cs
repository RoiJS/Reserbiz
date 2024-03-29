using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Helpers.Constants;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TermController : ReserbizBaseController
    {
        private readonly ITermRepository<Term> _termRepo;
        private readonly IMapper _mapper;

        public TermController(ITermRepository<Term> termRepo, IMapper mapper)
        {
            _mapper = mapper;
            _termRepo = termRepo;
        }

        [HttpPost(ApiRoutes.TermControllerRoutes.CreateTermURL)]
        public async Task<ActionResult<TermDetailDto>> CreateTerm(TermForManageDto termForCreationDto)
        {
            var termToCreate = _mapper.Map<Term>(termForCreationDto);
            termToCreate.TermParentId = null;

            await _termRepo
                .SetCurrentUserId(CurrentUserId)
                .AddEntity(termToCreate);

            var termtoReturn = _mapper.Map<TermDetailDto>(termToCreate);

            return CreatedAtRoute(
                routeName: nameof(GetTerm),
                routeValues: new { id = termToCreate.Id },
                value: termtoReturn
            );
        }

        [HttpGet(ApiRoutes.TermControllerRoutes.GetTermURL, Name = "GetTerm")]
        public async Task<ActionResult<TermDetailDto>> GetTerm(int id)
        {
            var termFromRepo = await _termRepo.GetTermAsync(id);

            if (termFromRepo == null)
                return BadRequest("Term not found.");

            var termToReturn = _mapper.Map<TermDetailDto>(termFromRepo);

            return Ok(termToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TermListDto>>> GetTerms(string termKeywords)
        {
            var termsFromRepo = await _termRepo.GetTermsAsync(termKeywords);

            var termToReturn = _mapper.Map<IEnumerable<TermListDto>>(termsFromRepo);

            return Ok(termToReturn);
        }

        [HttpGet(ApiRoutes.TermControllerRoutes.GetTermsAsOptionsURL)]
        public async Task<ActionResult<IEnumerable<TermOptionDto>>> GetTermsAsOptions()
        {
            var termsFromRepo = await _termRepo.GetTermsAsOptions();

            var termOptions = _mapper.Map<IEnumerable<TermOptionDto>>(termsFromRepo);

            return Ok(termOptions);
        }

        [HttpPut(ApiRoutes.TermControllerRoutes.UpdateTermURL)]
        public async Task<IActionResult> UpdateTerm(int id, TermForUpdateDto termForUpdateDto)
        {
            var termFromRepo = await _termRepo.GetEntity(id).ToObjectAsync();

            if (termFromRepo == null)
                return NotFound("Term not found.");

            _termRepo.SetCurrentUserId(CurrentUserId);

            _mapper.Map(termForUpdateDto, termFromRepo);

            if (!_termRepo.HasChanged())
                return BadRequest("Nothing was changed on the object.");

            if (await _termRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating term with an id of {id} failed on save.");
        }

        [HttpDelete(ApiRoutes.TermControllerRoutes.DeleteTermURL)]
        public async Task<ActionResult<TermDetailDto>> DeleteTerm(int id)
        {
            var termFromRepo = await _termRepo.GetEntity(id).ToObjectAsync();

            if (termFromRepo == null)
                return NotFound("Term not found.");

            _termRepo
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(termFromRepo);

            var termToReturn = _mapper.Map<TermDetailDto>(termFromRepo);

            if (await _termRepo.SaveChanges())
                return Ok(termToReturn);

            throw new Exception($"Deleting term with an id of {id} failed on save.");
        }

        [HttpPost(ApiRoutes.TermControllerRoutes.DeleteMultipleTermsURL)]
        public async Task<IActionResult> DeleteMultipleTerms(List<int> termIds)
        {
            if (termIds.Count == 0)
                return BadRequest("Empty terms id list.");

            _termRepo.SetCurrentUserId(CurrentUserId);

            if (await _termRepo.DeleteMultipleTermsAsync(termIds))
                return NoContent();

            throw new Exception($"Error when deleting terms!");
        }


        [HttpGet(ApiRoutes.TermControllerRoutes.CheckTermCodeIfExistsURL)]
        public async Task<ActionResult<bool>> CheckTermCodeIfExists(int termId, string termCode)
        {
            var termsFromRepo = await _termRepo.GetAllEntities().ToListObjectAsync();

            return Ok(_termRepo.CheckTermCodeIfExists(termsFromRepo, termId, termCode));
        }
    }
}