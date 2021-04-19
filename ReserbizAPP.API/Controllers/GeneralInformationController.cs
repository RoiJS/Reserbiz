using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReserbizAPP.API.Helpers.Interfaces;
using ReserbizAPP.API.Hubs;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralInformationController : ControllerBase
    {

        private readonly IGeneralInformationRepository<GeneralInformation> _generalInformationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ReserbizMainHub, IReserbizMainHubClient> _hubContext;

        public GeneralInformationController(IGeneralInformationRepository<GeneralInformation> generalInformationRepository,
            IHubContext<ReserbizMainHub, IReserbizMainHubClient> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
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
            {
                // Broadcast to all connected clients the current status of system update
                await _hubContext.Clients.All.BroadCastSystemUpdateStatus(generalInformationUpdateDto.SystemUpdateStatus);
                return NoContent();
            }

            throw new Exception("Updating system update status failed on save!");
        }
    }
}