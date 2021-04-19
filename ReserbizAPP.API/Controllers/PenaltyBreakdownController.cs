using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PenaltyBreakdownController : ReserbizBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPenaltyBreakdownRepository<PenaltyBreakdown> _penaltyBreakdownRepository;
        private readonly IPaginationService _paginationService;

        public PenaltyBreakdownController(
            IPenaltyBreakdownRepository<PenaltyBreakdown> penaltyBreakdownRepository,
            IMapper mapper,
            IPaginationService paginationService)
        {
            _mapper = mapper;
            _paginationService = paginationService;
            _penaltyBreakdownRepository = penaltyBreakdownRepository;
        }

        [HttpGet("getPenaltiesPerAccountStatement")]
        public async Task<ActionResult<IEnumerable<AccountStatementPenaltyItemDetailsDto>>> GetPenalties(int accountStatementId, int page, SortOrderEnum sortOrder)
        {
            var penaltyBreakdownsFromRepo = await _penaltyBreakdownRepository.GetAllPenaltiesAsync(accountStatementId, sortOrder);

            var mappedPenaltyDetails = _mapper.Map<IEnumerable<AccountStatementPenaltyItemDetailsDto>>(penaltyBreakdownsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<PenaltyPaginationListDto>(mappedPenaltyDetails, page);

            entityPaginationListDto.TotalAmount = penaltyBreakdownsFromRepo
                                                .Select(p => p.Amount)
                                                .Sum();

            return Ok(entityPaginationListDto);
        }
    }
}