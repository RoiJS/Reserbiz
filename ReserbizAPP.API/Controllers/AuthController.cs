using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public AuthController(IAuthRepository<Account> authRepo, IConfiguration config, IMapper mapper)
        {
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

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

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
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountForUpdateDto accountForUpdateDto)
        {
            if (id != accountForUpdateDto.Id)
                return BadRequest("Id does not match with the account that is to be updated");

            var accountFromRepo = await _authRepo.GetEntityById(id);

            if (accountForUpdateDto == null)
                return NotFound("Account does not exists.");

            _mapper.Map(accountForUpdateDto, accountFromRepo);

            if (!_authRepo.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating account with an id of {id} failed on save.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountForListDto>> GetAccount(int id)
        {
            var accountFromRepo = await _authRepo.GetEntityById(id);

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            var accountToReturn = _mapper.Map<AccountForDetailDto>(accountFromRepo);
            return Ok(accountToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountForListDto>>> GetAllAccounts()
        {
            var accountsFromRepo = await _authRepo.GetAllEntities();
            var accountsToReturn = _mapper.Map<IEnumerable<AccountForListDto>>(accountsFromRepo);
            return Ok(accountsToReturn);
        }
    }
}