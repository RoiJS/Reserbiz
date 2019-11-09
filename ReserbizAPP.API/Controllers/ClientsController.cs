using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository<Client> clientRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ClientForRegisterDto clientForRegisterDto)
        {
            var clientToCreate = new Client
            {
                Name = clientForRegisterDto.Name,
                DBName = clientForRegisterDto.DbName,
                Description = clientForRegisterDto.Description,
                ContactNumber = clientForRegisterDto.ContactNumber,
                DateJoined = DateTime.Now
            };

            var createdClient = await _clientRepository.RegisterClient(clientToCreate);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var clientFromRepo = await _clientRepository.GetEntityById(id);

            if (clientFromRepo == null)
                return NotFound("Client does not exists.");

            _clientRepository.DeleteEntity(clientFromRepo);

            if (await _clientRepository.SaveChanges())
                return Ok(clientFromRepo);

            throw new Exception($"Deleting client with an id of {id} failed on save.");
        }

        [HttpGet("{clientName}")]
        public async Task<ActionResult<ClientDetailsDto>> GetClientInformation(string clientName)
        {
            var clientInfo = await _clientRepository.GetCompanyInfoByName(clientName);

            if (clientInfo == null)
                return BadRequest("Company does not exists.");

            var clientInfoToReturn = _mapper.Map<ClientDetailsDto>(clientInfo);

            return Ok(clientInfoToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientForUpdateDto clientForUpdateDto)
        {
            var clientFromRepo = await _clientRepository.GetEntityById(id);

            if (clientFromRepo == null)
                return NotFound("Client does not exists.");

            _mapper.Map(clientForUpdateDto, clientFromRepo);

            if (!_clientRepository.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _clientRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating client with an id of {id} failed on save.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientForListDto>>> GetAllClients()
        {
            var clientsFromRepo = await _clientRepository.GetAllEntities();
            var clietsToReturn = _mapper.Map<IEnumerable<ClientForListDto>>(clientsFromRepo);
            return Ok(clietsToReturn);
        }
    }
}