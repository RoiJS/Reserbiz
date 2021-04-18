using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDbManagerController : ReserbizBaseController
    {
        private readonly ReserbizClientDataContext _context;
        private readonly IDataSeedRepository<IEntity> _dataSeedRepository;
        private readonly IMapper _mapper;

        public ClientDbManagerController(ReserbizClientDataContext context, IDataSeedRepository<IEntity> dataSeedRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dataSeedRepository = dataSeedRepository;
        }

        [HttpPost("syncDatabase")]
        public async Task<IActionResult> SyncDatabase()
        {
            await _context.Database.MigrateAsync();

            return Ok();
        }

        [HttpPost("populateDatabase")]
        public async Task<ActionResult<UserAccount>> PopulateDatabase(ClientInformationDto clientInformationDto)
        {
            var userAccount = _mapper.Map<UserAccount>(clientInformationDto.UserAccountDto);
            var client = _mapper.Map<Client>(clientInformationDto.ClientDto);

            try
            {
                // Populate default data on the newly created database
                await _dataSeedRepository.SeedData(userAccount, client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error populating database {clientInformationDto.ClientDto.Name}. Error Message: {ex.InnerException.Message}");
            }

            var userAccountDtoToReturn = _mapper.Map<UserAccountDto>(userAccount);

            return Ok(userAccountDtoToReturn);
        }
    }
}