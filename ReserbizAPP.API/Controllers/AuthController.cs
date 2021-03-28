using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReserbizAPP.API.Helpers.Interfaces;
using ReserbizAPP.API.Hubs;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ReserbizBaseController
    {
        private readonly IAuthRepository<Account> _authRepo;
        private readonly IRefreshTokenRepository<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IHubContext<ReserbizMainHub, IReserbizMainHubClient> _hubContext;

        public AuthController(
            IAuthRepository<Account> authRepo,
            IRefreshTokenRepository<RefreshToken> refreshTokenRepository,
            IHubContext<ReserbizMainHub, IReserbizMainHubClient> hubContext,
            IConfiguration config,
            IMapper mapper,
            IOptions<ApplicationSettings> appSettings
        )
        {
            _appSettings = appSettings;
            _mapper = mapper;
            _hubContext = hubContext;
            _authRepo = authRepo;
            _refreshTokenRepository = refreshTokenRepository;
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
        public async Task<ActionResult<AuthenticationTokenInfoDto>> Login(AccountForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
            {
                return BadRequest("Invalid username or password!");
            }

            var accessToken = GenerateAccessToken(userFromRepo);
            var refreshToken = _authRepo.GenerateNewRefreshToken();

            userFromRepo.RefreshTokens.Add(refreshToken);

            // Removed expired refresh tokens
            await _authRepo.RemoveExpiredRefreshTokens();

            await _authRepo.SaveChanges();

            // Broadcast Validate Login to make sure user account is only logged in on single device
            await this._hubContext.Clients.All.BroadCastValidateLogin(userFromRepo.Id, userFromRepo.Username);

            return Ok(new AuthenticationTokenInfoDto
            {
                AccessToken = accessToken,
                ExpiresIn = refreshToken.ExpirationDate,
                RefreshToken = refreshToken.Token
            });
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<AuthenticationTokenInfoDto>> Refresh(RefreshRequestDto request)
        {
            var userId = GetUserIdFromAccessToken(request.AccessToken);

            var userFromRepo = await _authRepo
                    .GetEntity(userId)
                    .Includes(a => a.RefreshTokens)
                    .ToObjectAsync();

            if (userFromRepo == null)
            {
                return BadRequest("User does not exist!");
            }

            if (!ValidateRefreshToken(userFromRepo, request.RefreshToken))
            {
                throw new SecurityTokenException("Invalid refresh token!");
            }

            var newAccessToken = GenerateAccessToken(userFromRepo);
            var userRefreshTokenFromRepo = await _refreshTokenRepository.GetRefreshToken(request.RefreshToken);
            var newRefreshToken = _authRepo.GenerateNewRefreshToken();

            // Removed expired refresh tokens
            await _authRepo.RemoveExpiredRefreshTokens();

            userFromRepo.RefreshTokens.Add(newRefreshToken);

            if (!await _authRepo.SaveChanges())
            {
                throw new Exception($"Updating account refresh token failed on save!");
            }

            return Ok(new AuthenticationTokenInfoDto
            {
                AccessToken = newAccessToken,
                ExpiresIn = userRefreshTokenFromRepo.ExpirationDate,
                RefreshToken = userRefreshTokenFromRepo.Token
            });
        }

        [Authorize]
        [HttpPut("updateAccountInformation/{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountForUpdateDto accountForUpdateDto)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            if (await _authRepo.UserExists(accountForUpdateDto.Username, accountFromRepo.Id))
                return BadRequest("Username already exists.");

            _authRepo.SetCurrentUserId(CurrentUserId);

            accountFromRepo.Username = accountForUpdateDto.Username;

            // Only update password if it is not empty
            if (!String.IsNullOrWhiteSpace(accountForUpdateDto.Password))
            {
                var securedPassword = _authRepo.GenerateNewPassword(accountForUpdateDto.Password);
                accountFromRepo.PasswordSalt = securedPassword.PasswordSalt;
                accountFromRepo.PasswordHash = securedPassword.PasswordHash;
            }

            if (!_authRepo.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating account with an id of {id} failed on save.");
        }

        [Authorize]
        [HttpGet("validateUsernameExists/{id}/{username}")]
        public async Task<ActionResult<bool>> ValidateUsernameExists(int id, string username)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            if (await _authRepo.UserExists(username, accountFromRepo.Id))
                return Ok(true);

            return Ok(false);
        }

        [Authorize]
        [HttpPut("updatePersonalInformation/{id}")]
        public async Task<IActionResult> UpdatePersonalInformation(int id, PersonalInformationForUpdateDto personalInformationForUpdate)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (personalInformationForUpdate == null)
                return NotFound("Account does not exists.");

            _authRepo.SetCurrentUserId(CurrentUserId);

            _mapper.Map(personalInformationForUpdate, accountFromRepo);

            if (!_authRepo.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepo.SaveChanges())
                return NoContent();

            throw new Exception($"Updating account with an id of {id} failed on save.");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountForListDto>> GetAccount(int id)
        {
            var accountFromRepo = await _authRepo.GetEntity(id).ToObjectAsync();

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            var accountToReturn = _mapper.Map<AccountForDetailDto>(accountFromRepo);
            return Ok(accountToReturn);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountForListDto>>> GetAllAccounts()
        {
            var accountsFromRepo = await _authRepo.GetAllEntities().ToListObjectAsync();
            var accountsToReturn = _mapper.Map<IEnumerable<AccountForListDto>>(accountsFromRepo);
            return Ok(accountsToReturn);
        }
        private string GenerateAccessToken(Account user)
        {
            var claims = new[] {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ReserbizClaimTypes.FirstName, user.FirstName),
                new Claim (ReserbizClaimTypes.MiddleName, user.MiddleName),
                new Claim (ReserbizClaimTypes.LastName, user.LastName),
                new Claim (ReserbizClaimTypes.Gender, ((int)user.Gender).ToString()),
                new Claim (ReserbizClaimTypes.Username, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(createToken);

            return token;
        }

        private int GetUserIdFromAccessToken(string accessToken)
        {
            var tokenValidationParamters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.Token)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException($"Missing claim: {ClaimTypes.NameIdentifier}!");
            }

            return Convert.ToInt32(userId);
        }

        private bool ValidateRefreshToken(Account user, string refreshToken)
        {
            if (user == null ||
                !user.RefreshTokens.Exists(rt => rt.Token == refreshToken))
            {
                return false;
            }

            var storedRefreshToken = user.RefreshTokens.Find(rt => rt.Token == refreshToken);

            // Ensure that the refresh token that we got from storage is not yet expired.
            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
            {
                return false;
            }

            return true;
        }
    }
}