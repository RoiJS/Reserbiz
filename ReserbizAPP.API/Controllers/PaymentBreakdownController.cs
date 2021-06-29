using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using ReserbizAPP.LIB.Helpers;
using System;
using AutoMapper;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using Microsoft.Extensions.Options;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentBreakdownController : ReserbizBaseController
    {
        private readonly IPaginationService _paginationService;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IPaymentBreakdownRepository<PaymentBreakdown> _paymentBreakdownRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;
        private readonly IMapper _mapper;
        private readonly IContractRepository<Contract> _contractRepository;

        public PaymentBreakdownController(
            IPaymentBreakdownRepository<PaymentBreakdown> paymentBreakdownRepository,
            IAccountStatementRepository<AccountStatement> accountStatementRepository,
            IContractRepository<Contract> contractRepository,
            IMapper mapper,
            IPaginationService paginationService,
            IOptions<ApplicationSettings> appSettings
            )
        {
            _appSettings = appSettings;
            _contractRepository = contractRepository;
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
                DateTimeReceived = paymentForCreationDto.DateTimeReceived.ToLocalTimeZone(),
                Notes = paymentForCreationDto.Notes,
                IsAmountFromDeposit = paymentForCreationDto.IsAmountFromDeposit,
                PaymentForType = paymentForCreationDto.PaymentForType,
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
        public async Task<ActionResult<PaymentPaginationListDto>> GetPaymentsPerAccountStatement(int contractId, int accountStatementId, int page, SortOrderEnum sortOrder)
        {
            var paymentBreakdownsFromRepo = await _paymentBreakdownRepository.GetAllPaymentsPerAccountStatmentAsync(accountStatementId, sortOrder);

            var mappedPaymentDetails = _mapper.Map<IEnumerable<PaymentBreakdownForDetailsDto>>(paymentBreakdownsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<PaymentPaginationListDto>(mappedPaymentDetails, page);

            await CalculatePaginationTotalAmounts(contractId, accountStatementId, entityPaginationListDto, paymentBreakdownsFromRepo);

            return Ok(entityPaginationListDto);
        }

        private async Task CalculatePaginationTotalAmounts(int contractId, int accountStatementId, PaymentPaginationListDto entityPaginationListDto, IEnumerable<PaymentBreakdown> paymentBreakdowns)
        {
            var firstAccountStatement = await _accountStatementRepository.GetFirstAccountStatement(contractId);

            var currentAccountStatement = await _accountStatementRepository.GetAccountStatementAsync(accountStatementId);

            entityPaginationListDto.TotalAmount = _accountStatementRepository.CalculateTotalAmountPaid(paymentBreakdowns);

            entityPaginationListDto.TotalAmountFromDeposit = _accountStatementRepository.CalculateTotalAmountPaidUsingDeposit(paymentBreakdowns);

            entityPaginationListDto.DepositedAmountBalance = await _accountStatementRepository.CalculatedDepositedAmountBalance(contractId, firstAccountStatement);

            entityPaginationListDto.SuggestedRentalAmount = _accountStatementRepository.CalculatedSuggestedAmountForPayment(paymentBreakdowns, currentAccountStatement, entityPaginationListDto.DepositedAmountBalance);

            entityPaginationListDto.SuggestedElectricBillAmount = _accountStatementRepository.CalculateSuggestedAmountForElectricBill(paymentBreakdowns, currentAccountStatement, entityPaginationListDto.DepositedAmountBalance);

            entityPaginationListDto.SuggestedWaterBillAmount = _accountStatementRepository.CalculateSuggestedAmountForWaterBill(paymentBreakdowns, currentAccountStatement, entityPaginationListDto.DepositedAmountBalance);

            entityPaginationListDto.SuggestedMiscelleneousAmount = _accountStatementRepository.CalculateSuggestedAmountForMiscellaneousFees(paymentBreakdowns, currentAccountStatement, entityPaginationListDto.DepositedAmountBalance);

            entityPaginationListDto.SuggestedPenaltyAmount = _accountStatementRepository.CalculateSuggestedAmountForPenaltyAmount(paymentBreakdowns, currentAccountStatement, entityPaginationListDto.DepositedAmountBalance);
        }
    }
}