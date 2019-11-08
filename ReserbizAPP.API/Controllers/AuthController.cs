using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;
        private readonly IClientRepository _clientRepo;

        public AuthController(IAuthRepository authRepo, IClientRepository clientRepo, IConfiguration config)
        {
            _clientRepo = clientRepo;
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
    }
}