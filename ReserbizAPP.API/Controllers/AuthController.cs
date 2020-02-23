using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository<Account> _authRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOptions<IApplicationSettings> _appSettings;

        public AuthController(
            IAuthRepository<Account> authRepo,
            IConfiguration config,
            IMapper mapper,
            IOptions<IApplicationSettings> appSettings
        )
        {
            _appSettings = appSettings;
            _mapper = mapper;
            _authRepo = authRepo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountForRegisterDto accountForRegisterDto)
        {
            accountForRegisterDto.Username = accountForRegisterDto.Username.ToLower();

            if (await _authRepo.UserExists(accountForRegisterDto.Username))
                return BadRequest("Username already exists.");

            var userToCreate = new Account
            {
                FirstName = accountForRegisterDto.FirstName,
                MiddleName = accountForRegisterDto.MiddleName,
                LastName = accountForRegisterDto.LastName,
                Gender = accountForRegisterDto.Gender,
                Username = accountForRegisterDto.Username
            };

            var createdUser = await _authRepo.Register(userToCreate, accountForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var userToReturn = _mapper.Map<AccountForDetailDto>(userFromRepo);

            var claims = new[] {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                currentUser = userToReturn,
                expiresIn = tokenDescriptor.Expires
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountForUpdateDto accountForUpdateDto)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (accountForUpdateDto == null)
                return NotFound("Account does not exists.");

            _mapper.Map(accountForUpdateDto, accountFromRepo);

            if (!_authRepo.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating account with an id of {id} failed on save.");
        }
        
        [HttpPut("updatePersonalInformation/{id}")]
        public async Task<IActionResult> UpdatePersonalInformation(int id, PersonalInformationForUpdateDto personalInformationForUpdate)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (personalInformationForUpdate == null)
                return NotFound("Account does not exists.");

            _mapper.Map(personalInformationForUpdate, accountFromRepo);

            if (!_authRepo.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating account with an id of {id} failed on save.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountForListDto>> GetAccount(int id)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            var accountToReturn = _mapper.Map<AccountForDetailDto>(accountFromRepo);
            return Ok(accountToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountForListDto>>> GetAllAccounts()
        {
            var accountsFromRepo = await _authRepo.GetAllEntities().ToListObjectAsync();
            var accountsToReturn = _mapper.Map<IEnumerable<AccountForListDto>>(accountsFromRepo);
            return Ok(accountsToReturn);
        }
    }
}