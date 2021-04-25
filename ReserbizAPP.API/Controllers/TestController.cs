using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class TestController : ControllerBase {
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IOptions<ApplicationSettings> _appSettings;

        public TestController (IClientRepository<Client> clientRepository, IOptions<ApplicationSettings> appSettings) {
            _appSettings = appSettings;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientForListDto>>> GetAllClients () {
            var clientsFromRepo = await _clientRepository.GetAllEntities().ToListObjectAsync();
            return Ok (clientsFromRepo);
        }

        [HttpGet ("getTimeZonesLocal")]
        public IActionResult GetTimeZonesLocal () {
            var timeZones = TimeZoneInfo.Local;
            return Ok (timeZones);
        }
        
        [HttpGet ("getTimeZones")]
        public IActionResult GetAllTimeZone() {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            return Ok (timeZones);
        }

        [HttpGet ("getCurrentDateTime")]
        public IActionResult GetCurrentDateTime () {
            var currentDateTime = DateTime.Now.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone);
            // var currentDateTime = new DateTime(2021, 04, 24, 15, 20, 38).ToUniversalTime();
            // currentDateTime = currentDateTime.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone);
            return Ok (String.Format("{0} {1}", currentDateTime.ToLongDateString(), currentDateTime.ToLongTimeString()));
        }
    }
}