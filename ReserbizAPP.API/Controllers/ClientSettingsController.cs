using System;
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
    public class ClientSettingsController : ControllerBase
    {
        private readonly IClientSettingsRepository<ClientSettings> _clientSettingsRepository;
        private readonly IMapper _mapper;
        public ClientSettingsController(IClientSettingsRepository<ClientSettings> clientSettingsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clientSettingsRepository = clientSettingsRepository;
        }

        [HttpPut("saveSettings")]
        public async Task<IActionResult> UpdateSettings(ClientSettingsForUpdateDto clientSettingsForUpdateDto)
        {
            var clientSettingsFromRepo = await _clientSettingsRepository.GetClientSettings();

            if (clientSettingsFromRepo == null)
            {
                var newClientSettings = _mapper.Map<ClientSettings>(clientSettingsForUpdateDto);
                await _clientSettingsRepository.AddEntity(newClientSettings);
                await _clientSettingsRepository.SaveChanges();

                return Ok("New settings has been saved!");
            }

            _mapper.Map(clientSettingsForUpdateDto, clientSettingsFromRepo);

            if (await _clientSettingsRepository.SaveChanges())
                return NoContent();

            throw new Exception("Updating settings failed on save!");
        }
    }
}