using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppGlobalSettingsController : ControllerBase
    {
        private readonly IAppGlobalSettingsRepository<AppGlobalSettings> _appGlobalSettingsRepository;
        private readonly IMapper _mapper;

        public AppGlobalSettingsController(IAppGlobalSettingsRepository<AppGlobalSettings> appGlobalSettingsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _appGlobalSettingsRepository = appGlobalSettingsRepository;
        }

        [HttpGet("getAppGlobalSettings")]
        public async Task<ActionResult<AppGlobalSettingsDto>> GetAppGlobalSettings()
        {
            var appGlobalSettingsFromRepo = await _appGlobalSettingsRepository.GetAppGlobalSettings();

            if (appGlobalSettingsFromRepo == null)
                throw new Exception("No App Global Settings.");

            var appGlobalSettingsToReturn = _mapper.Map<AppGlobalSettingsDto>(appGlobalSettingsFromRepo);

            return Ok(appGlobalSettingsToReturn);
        }
    }
}