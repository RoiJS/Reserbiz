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
using ReserbizAPP.LIB.Enums;
using System.Linq;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentBreakdownController : ReserbizBaseController
    {
        private readonly IPaginationService _paginationService;
        private readonly IPaymentBreakdownRepository<PaymentBreakdown> _paymentBreakdownRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;
        private readonly IMapper _mapper;

        public PaymentBreakdownController(IPaymentBreakdownRepository<PaymentBreakdown> paymentBreakdownRepository,
        IAccountStatementRepository<AccountStatement> accountStatementRepository, IMapper mapper, IPaginationService paginationService)
        {
            _accountStatementRepository = accountStatementRepository;
            _paymentBreakdownRepository = paymentBreakdownRepository;
            _mapper = mapper;
            _paginationService = paginationService;
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
                ReceivedById = CurrentUserId,
                Amount = paymentForCreationDto.Amount,
                DateTimeReceived = paymentForCreationDto.DateTimeReceived,
                Notes = paymentForCreationDto.Notes
            };

            _accountStatementRepository.SetCurrentUserId(CurrentUserId);

            accountStatementFromRepo.PaymentBreakdowns.Add(newPaymentDetails);

            if (!await _accountStatementRepository.SaveChanges())
                throw new Exception($"Saving payment details failed on save!");

            var paymentFromRepo = await _paymentBreakdownRepository
                                        .GetEntity(newPaymentDetails.Id)
                                        .Includes(p => p.ReceivedBy)
                                        .ToObjectAsync();

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
            var paymentFromRepo = await _paymentBreakdownRepository
                                        .GetEntity(id)
                                        .Includes(p => p.ReceivedBy)
                                        .ToObjectAsync();

            if (paymentFromRepo == null)
                return NotFound("Payment details not found.");

            var paymentDetailsToReturn = _mapper.Map<PaymentBreakdownForDetailsDto>(paymentFromRepo);

            return Ok(paymentDetailsToReturn);
        }

        [HttpGet("getPaymentsPerAccountStatement")]
        public async Task<ActionResult<IEnumerable<PaymentBreakdownForDetailsDto>>> GetPaymentsPerAccountStatement(int accountStatementId, int page, SortOrderEnum sortOrder)
        {
            var paymentBreakdownsFromRepo = await _paymentBreakdownRepository.GetAllPaymentsAsync(accountStatementId, sortOrder);

            var mappedPaymentDetails = _mapper.Map<IEnumerable<PaymentBreakdownForDetailsDto>>(paymentBreakdownsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<PaymentPaginationListDto>(mappedPaymentDetails, page);
            entityPaginationListDto.TotalAmount = mappedPaymentDetails
                                                        .Select(m => m.Amount)
                                                        .Sum();

            return Ok(entityPaginationListDto);
        }
    }
}