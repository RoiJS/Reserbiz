using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Dtos;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.Hangfire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestClientRequestController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IOptions<ApplicationSettings> _appSettings;

        public TestClientRequestController(IClientRepository<Client> clientRepository, IOptions<ApplicationSettings> appSettings)
        {
            _appSettings = appSettings;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientForListDto>>> GetAllClients()
        {
            var clientsFromRepo = await _clientRepository.GetAllEntities().ToListObjectAsync();
            return Ok(clientsFromRepo);
        }

        [HttpGet("getTimeZones")]
        public IActionResult GetAllTimeZone()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();
            return Ok(timeZones);
        }

        [HttpGet("getCurrentDateTime")]
        public IActionResult GetCurrentDateTime()
        {
            var currentDateTime = DateTime.Now.ConvertToTimeZone(_appSettings.Value.GeneralSettings.TimeZone);
            return Ok(currentDateTime);
        }
    }
}