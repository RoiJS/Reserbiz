using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
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
        private readonly IGeneralInformationRepository<GeneralInformation> _generalInformationRepository;

        public ClientsController(
            IClientRepository<Client> clientRepository,
            IGeneralInformationRepository<GeneralInformation> generalInformationRepository,
            IMapper mapper
        )
        {
            _clientRepository = clientRepository;
            _generalInformationRepository = generalInformationRepository;
            _mapper = mapper;
        }

        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterClient(ClientForRegisterDto clientForRegisterDto)
        {
            var clientToCreate = new Client
            {
                Name = clientForRegisterDto.Name,
                Type = ClientTypeEnum.Regular,
                Description = clientForRegisterDto.Description,
                ContactNumber = clientForRegisterDto.ContactNumber,
                DateJoined = DateTime.Now
            };

            var userAccount = new UserAccount
            {
                FirstName = clientForRegisterDto.FirstName,
                MiddleName = clientForRegisterDto.MiddleName,
                LastName = clientForRegisterDto.LastName,
                EmailAddress = clientForRegisterDto.EmailAddress
            };

            try
            {
                // Save client information
                var createdClient = await _clientRepository.RegisterClient(clientToCreate);

                // Create client database
                await _clientRepository.CreateClientDatabase(createdClient);

                // Populate default data on the client database
                await _clientRepository.PopulateDatabase(userAccount, createdClient, (UserAccount account) =>
                {
                    // Send email notification after database has been successfully created
                    _clientRepository.SendNewClientRegisteredEmailNotification(account, createdClient);
                });

            }
            catch (Exception ex)
            {
                throw new Exception($"Error on creating client database. Error message: {ex.InnerException.Message}");
            }

            return StatusCode(201);
        }

        [HttpPost("registerDemo")]
        public async Task<IActionResult> RegisterDemo(DemoForRegisterDto demoForRegisterDto)
        {
            var demoToCreate = new Client
            {
                Name = demoForRegisterDto.Name,
                Type = ClientTypeEnum.Demo,
                ContactNumber = demoForRegisterDto.ContactNumber,
                DateJoined = DateTime.Now
            };

            var userAccount = new UserAccount
            {
                FirstName = demoForRegisterDto.FirstName,
                MiddleName = demoForRegisterDto.MiddleName,
                LastName = demoForRegisterDto.LastName,
                EmailAddress = demoForRegisterDto.EmailAddress
            };

            try
            {
                // Save client information
                var createdDemoClient = await _clientRepository.RegisterDemo(demoToCreate);

                // Create demo database
                await _clientRepository.CreateClientDatabase(createdDemoClient);

                // Populate default data on the demo database
                await _clientRepository.PopulateDatabase(userAccount, createdDemoClient, (UserAccount account) =>
                {
                    // Send email notification after database has been successfully created
                    _clientRepository.SendNewDemoRegisteredEmailNotification(account, createdDemoClient);
                });

            }
            catch (Exception ex)
            {
                throw new Exception($"Error on creating demo database. Error message: {ex.InnerException.Message}");
            }

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientDetailsDto>> DeleteClient(int id)
        {
            var clientFromRepo = await _clientRepository.GetEntity(id).ToObjectAsync();

            if (clientFromRepo == null)
                return NotFound("Client does not exists.");

            _clientRepository.DeleteEntity(clientFromRepo);

            var clientToReturn = _mapper.Map<ClientDetailsDto>(clientFromRepo);

            if (await _clientRepository.SaveChanges())
                return Ok(clientToReturn);

            throw new Exception($"Deleting client with an id of {id} failed on save.");
        }

        [HttpGet("{clientName}")]
        public async Task<ActionResult<ClientDetailsDto>> GetClientInformation(string clientName)
        {
            var clientInfo = await _clientRepository.GetCompanyInfoByName(clientName);

            if (clientInfo == null)
                return BadRequest("Company does not exists.");

            var generalInformation = await _generalInformationRepository.GetGeneralInformation();

            if (generalInformation.SystemUpdateStatus)
                return BadRequest("System is locked and currently undergoing maintenance. Please comeback later.");

            var clientInfoToReturn = _mapper.Map<ClientDetailsDto>(clientInfo);

            return Ok(clientInfoToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientForUpdateDto clientForUpdateDto)
        {
            var clientFromRepo = await _clientRepository.GetEntity(id).ToObjectAsync();

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
            var clientsFromRepo = await _clientRepository.GetAllEntities().ToListObjectAsync();
            var clietsToReturn = _mapper.Map<IEnumerable<ClientForListDto>>(clientsFromRepo);
            return Ok(clietsToReturn);
        }
    }
}