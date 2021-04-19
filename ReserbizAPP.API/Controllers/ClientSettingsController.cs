using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ClientSettingsController : ReserbizBaseController
    {
        private readonly ITermRepository<Term> _termRepository;
        private readonly ISpaceTypeRepository<SpaceType> _spaceTypeRepository;
        private readonly IContactPersonRepository<ContactPerson> _contactPersonRepository;
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly IAuthRepository<Account> _authRepository;
        private readonly IPaymentBreakdownRepository<PaymentBreakdown> _paymentBreakdownRepository;
        private readonly IPenaltyBreakdownRepository<PenaltyBreakdown> _penaltyBreakdownRepository;
        private readonly IClientSettingsRepository<ClientSettings> _clientSettingsRepository;
        private readonly IAccountStatementMiscellaneousRepository<AccountStatementMiscellaneous> _accountStatementMiscellaneousRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;
        private readonly IContractRepository<Contract> _contractRepository;
        private readonly ITermMiscellaneousRepository<TermMiscellaneous> _termMiscellaneousRepository;
        private readonly IMapper _mapper;

        public ClientSettingsController(
            IPaymentBreakdownRepository<PaymentBreakdown> paymentBreakdownRepository,
            IPenaltyBreakdownRepository<PenaltyBreakdown> penaltyBreakdownRepository,
            IAccountStatementMiscellaneousRepository<AccountStatementMiscellaneous> accountStatementMiscellaneousRepository,
            IAccountStatementRepository<AccountStatement> accountStatementRepository,
            IContractRepository<Contract> contractRepository,
            ITermMiscellaneousRepository<TermMiscellaneous> termMiscellaneousRepository,
            ITermRepository<Term> termRepository,
            ISpaceTypeRepository<SpaceType> spaceTypeRepository,
            IContactPersonRepository<ContactPerson> contactPersonRepository,
            ITenantRepository<Tenant> tenantRepository,
            IAuthRepository<Account> authRepository,
            IClientSettingsRepository<ClientSettings> clientSettingsRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _termRepository = termRepository;
            _spaceTypeRepository = spaceTypeRepository;
            _contactPersonRepository = contactPersonRepository;
            _tenantRepository = tenantRepository;
            _authRepository = authRepository;
            _paymentBreakdownRepository = paymentBreakdownRepository;
            _penaltyBreakdownRepository = penaltyBreakdownRepository;
            _clientSettingsRepository = clientSettingsRepository;
            _accountStatementMiscellaneousRepository = accountStatementMiscellaneousRepository;
            _accountStatementRepository = accountStatementRepository;
            _contractRepository = contractRepository;
            _termMiscellaneousRepository = termMiscellaneousRepository;
        }

        [HttpPut("saveSettings")]
        public async Task<IActionResult> UpdateSettings(ClientSettingsForUpdateDto clientSettingsForUpdateDto)
        {
            var clientSettingsFromRepo = await _clientSettingsRepository.GetClientSettings();

            if (clientSettingsFromRepo == null)
            {
                var newClientSettings = _mapper.Map<ClientSettings>(clientSettingsForUpdateDto);
                await _clientSettingsRepository
                    .SetCurrentUserId(CurrentUserId)
                    .AddEntity(newClientSettings);

                return Ok("New settings has been saved!");
            }

            _clientSettingsRepository.SetCurrentUserId(CurrentUserId);

            _mapper.Map(clientSettingsForUpdateDto, clientSettingsFromRepo);

            if (!_clientSettingsRepository.HasChanged())
                return BadRequest("Nothing was changed.");

            if (await _clientSettingsRepository.SaveChanges())
                return NoContent();

            throw new Exception("Updating settings failed on save!");
        }

        [HttpGet("getSettings")]
        public async Task<ActionResult<ClientSettingsDetailsDto>> GetSettings()
        {
            try
            {
                var settingsFromRepo = await _clientSettingsRepository.GetClientSettings();
                var settingsToReturn = _mapper.Map<ClientSettingsDetailsDto>(settingsFromRepo);

                return Ok(settingsToReturn);
            }
            catch
            {
                throw new Exception("Error on getting settings!");
            }
        }

        [AllowAnonymous]
        [HttpPost("systemResetData")]
        public async Task<IActionResult> SystemResetData()
        {
            await _paymentBreakdownRepository.Reset();
            await _penaltyBreakdownRepository.Reset();
            await _accountStatementMiscellaneousRepository.Reset();
            await _accountStatementRepository.Reset();
            await _contractRepository.Reset();
            await _termMiscellaneousRepository.Reset();
            await _termRepository.Reset();
            await _spaceTypeRepository.Reset();
            await _tenantRepository.Reset();
            await _authRepository.Reset();
            await _clientSettingsRepository.Reset();

            return NoContent();
        }
    }
}