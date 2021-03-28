using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneralInformationController : ControllerBase
    {

        private readonly IGeneralInformationRepository<GeneralInformation> _generalInformationRepository;
        private readonly IMapper _mapper;

        public GeneralInformationController(IGeneralInformationRepository<GeneralInformation> generalInformationRepository, IMapper mapper)
        {
            _mapper = mapper;
            _generalInformationRepository = generalInformationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralInformationDto>> GetGeneralInformation()
        {
            var generalInformationFromRepo = await _generalInformationRepository.GetGeneralInformation();
            var generalInformationToReturn = _mapper.Map<GeneralInformationDto>(generalInformationFromRepo);
            return Ok(generalInformationToReturn);
        }

        [HttpPut("setSystemUpdateStatus")]
        public async Task<IActionResult> UpdateSettings(GeneralInformationDto generalInformationUpdateDto)
        {
            var generalInformationFromRepo = await _generalInformationRepository.GetGeneralInformation();

            _mapper.Map(generalInformationUpdateDto, generalInformationFromRepo);

            if (!_generalInformationRepository.HasChanged())
                return BadRequest("Nothing was changed.");

            if (await _generalInformationRepository.SaveChanges())
                return NoContent();

            throw new Exception("Updating system update status failed on save!");
        }
    }
}