using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using RestSharp;
using ReserbizAPP.LIB.Dtos;
using System.Threading.Tasks;

namespace ReserbizAPP.Hangfire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestClientRequestController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepository;

        public TestClientRequestController(IClientRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientForListDto>>> GetAllClients()
        {
            var clientsFromRepo = await _clientRepository.GetAllEntities().ToListObjectAsync();
            return Ok(clientsFromRepo);
        }
    }
}