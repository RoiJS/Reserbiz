using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System.Security.Claims;
using System;
using AutoMapper;
using System.Collections.Generic;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentBreakdownController : ControllerBase
    {
        private readonly IPaymentBreakdownRepository<PaymentBreakdown> _paymentBreakdownRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;
        private readonly IMapper _mapper;

        public PaymentBreakdownController(IPaymentBreakdownRepository<PaymentBreakdown> paymentBreakdownRepository,
        IAccountStatementRepository<AccountStatement> accountStatementRepository, IMapper mapper)
        {
            _accountStatementRepository = accountStatementRepository;
            _paymentBreakdownRepository = paymentBreakdownRepository;
            _mapper = mapper;
        }

        [HttpPost("addPayment")]
        public async Task<ActionResult<PaymentBreakdownForDetailsDto>> AddPayment(int accountStatementId, PaymentForCreationDto paymentForCreationDto)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetEntity(accountStatementId)
                                                                            .Includes(a => a.PaymentBreakdowns)
                                                                            .ToObjectAsync();

            if (accountStatementFromRepo == null)
                return NotFound("Account Statemtent not found.");

            var newPaymentDetails = new PaymentBreakdown()
            {
                ReceivedById = Convert.ToInt32(User.Identity.GetUserClaim(ClaimTypes.NameIdentifier)),
                Amount = paymentForCreationDto.Amount,
                DateTimeReceived = paymentForCreationDto.DateTimeReceived
            };

            accountStatementFromRepo.PaymentBreakdowns.Add(newPaymentDetails);

            if (!await _accountStatementRepository.SaveChanges())
                throw new Exception($"Saving payment details failed on save!");

            var paymentFromRepo = await _paymentBreakdownRepository.GetEntity(newPaymentDetails.Id).Includes(p => p.ReceivedBy).ToObjectAsync();
            var paymentDetailsToReturn = _mapper.Map<PaymentBreakdownForDetailsDto>(paymentFromRepo);

            return CreatedAtRoute(
               routeName: nameof(GetPaymentDetails),
               routeValues: new { id = paymentDetailsToReturn.Id },
               value: paymentDetailsToReturn
           );
        }

        [HttpGet("{id}", Name = "GetPaymentDetails")]
        public async Task<ActionResult<PaymentBreakdownForDetailsDto>> GetPaymentDetails(int id)
        {
            var paymentFromRepo = await _paymentBreakdownRepository.GetEntity(id).Includes(p => p.ReceivedBy).ToObjectAsync();

            if (paymentFromRepo == null)
                return NotFound("Payment details not found.");

            var paymentDetailsToReturn = _mapper.Map<PaymentBreakdownForDetailsDto>(paymentFromRepo);

            return Ok(paymentDetailsToReturn);
        }

        [HttpGet("getPaymentsPerAccountStatement/{accountStatementId}")]
        public async Task<ActionResult<IEnumerable<PaymentBreakdownForDetailsDto>>> GetPaymentsPerAccountStatement(int accountStatementId)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetEntity(accountStatementId)
                                                                            .Includes(a => a.PaymentBreakdowns)
                                                                            .ToObjectAsync();
            if (accountStatementFromRepo == null)
                return NotFound("Account Statement not found.");

            var paymentDetailsListToReturn = _mapper.Map<IEnumerable<PaymentBreakdownForDetailsDto>>(accountStatementFromRepo.PaymentBreakdowns);

            return Ok(paymentDetailsListToReturn);
        }
    }
}