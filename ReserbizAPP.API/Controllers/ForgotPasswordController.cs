using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Route("api/forgotPassword")]
    public class ForgotPasswordController : ReserbizBaseController
    {
        private readonly IAccountRepository<Account> _accountRepository;
        private readonly IAuthRepository<Account> _authRepository;
        private readonly IMapper _mapper;

        public ForgotPasswordController(IAccountRepository<Account> accountRepository, IAuthRepository<Account> authRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("verifyUsernameOrEmailAddress/{usernameOrEmailAddress}")]
        public async Task<ActionResult<UserAccountDto>> VerifyUsernameOrEmailAddress(string usernameOrEmailAddress)
        {
            var accountFromRepo = await _accountRepository.VerifyUsernameOrEmailAddress(usernameOrEmailAddress);

            if (accountFromRepo == null)
                return BadRequest("Username or email address does not exists!");

            var accountToReturn = _mapper.Map<UserAccountDto>(accountFromRepo);

            return Ok(accountToReturn);
        }

        [HttpPut("saveNewPassword/{id}/{newPassword}")]
        public async Task<IActionResult> SaveNewPassword(int id, string newPassword)
        {
            var accountFromRepo = await _authRepository.GetEntity(id).ToObjectAsync();

            if (accountFromRepo == null)
                return NotFound("Account does not exists.");

            var securedPassword = _authRepository.GenerateNewPassword(newPassword);
            accountFromRepo.PasswordSalt = securedPassword.PasswordSalt;
            accountFromRepo.PasswordHash = securedPassword.PasswordHash;

            if (!_authRepository.HasChanged())
                return BadRequest("Nothing was change.");

            if (await _authRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating password for user with id of {id} failed on save.");
        }
    }
}