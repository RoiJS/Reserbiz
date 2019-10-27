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
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ClientForRegisterDto clientForRegisterDto)
        {
            var clientToCreate = new Client{
                Name = clientForRegisterDto.Name,
                DBName = clientForRegisterDto.DbName,
                Description = clientForRegisterDto.Description,
                ContactNumber = clientForRegisterDto.ContactNumber,
                DateJoined = DateTime.Now
            };

            var createdClient = await _clientRepository.RegisterClient(clientToCreate);

            return StatusCode(201);
        }
    }
}