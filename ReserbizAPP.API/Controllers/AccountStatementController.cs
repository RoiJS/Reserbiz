using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using System.Collections.Generic;
using ReserbizAPP.LIB.Enums;
using Microsoft.AspNetCore.Http;
using ReserbizAPP.LIB.Helpers.Services;
using ReserbizAPP.LIB.Helpers.Constants;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountStatementController : ReserbizBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITenantRepository<Tenant> _tenantRepository;
        private readonly IContractRepository<Contract> _contractRepository;
        private readonly IAccountStatementRepository<AccountStatement> _accountStatementRepository;
        private readonly IPaginationService _paginationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReserbizRepository<Entity> _genericReserbizRepository;

        public AccountStatementController(
            IAccountStatementRepository<AccountStatement> accountStatementRepository,
            ITenantRepository<Tenant> tenantRepository,
            IContractRepository<Contract> contractRepository,
            IMapper mapper, IPaginationService paginationService,
            IHttpContextAccessor httpContextAccessor,
            IReserbizRepository<Entity> genericReserbizRepository)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            _contractRepository = contractRepository;
            _accountStatementRepository = accountStatementRepository;
            _paginationService = paginationService;
            _httpContextAccessor = httpContextAccessor;
            _genericReserbizRepository = genericReserbizRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountStatementDetailsDto>> GetAccountStatement(int id)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetAccountStatementAsync(id);

            if (accountStatementFromRepo == null)
                return NotFound("Account Statement not found.");

            var accountStatementToReturn = _mapper.Map<AccountStatementDetailsDto>(accountStatementFromRepo);

            // Make sure to check if there is an unread user notification for this statement of account, 
            // If so, then mark it as read status.
            var accountStatementNotificationService = new AccountStatementNotificationService();
            await accountStatementNotificationService.MarkItemAsRead(_genericReserbizRepository, id, CurrentUserId, CurrentUserType);

            return Ok(accountStatementToReturn);
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.GetAccountStatementsPerContractURL)]
        public async Task<ActionResult<AccountStatementPaginationListDto>> GetAccountStatementsPerContract(int contractId, DateTime fromDate, DateTime toDate, PaymentStatusEnum paymentStatus, AccountStatementTypeEnum accountStatementType, SortOrderEnum sortOrder, int page)
        {
            var contractFromRepo = await _contractRepository.GetEntity(contractId).ToObjectAsync();
            var accountStatementsFromRepo = await _accountStatementRepository.GetActiveAccountStatementsPerContractAsync(contractId);
            var firstAccountStatement = await _accountStatementRepository.GetFirstAccountStatement(contractId);

            var accountStatementFilter = new AccountStatementFilter
            {
                FromDate = fromDate,
                ToDate = toDate,
                PaymentStatus = paymentStatus,
                AccountStatementType = accountStatementType,
                SortOrder = sortOrder
            };

            var filteredAccountStatementsFromRepo = _accountStatementRepository.GetFilteredAccountStatements(accountStatementsFromRepo.ToList(), accountStatementFilter);

            var mappedAccountStatements = _mapper.Map<IEnumerable<AccountStatementForListDto>>(filteredAccountStatementsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<AccountStatementPaginationListDto>(mappedAccountStatements, page);

            entityPaginationListDto.TotalExpectedAmount = accountStatementsFromRepo.Sum(a => a.AccountStatementTotalAmount);
            entityPaginationListDto.TotalPaidAmount = accountStatementsFromRepo.Sum(a => a.CurrentAmountPaid);
            entityPaginationListDto.TotalExpectedDepositAmount = firstAccountStatement != null ? firstAccountStatement.CalculatedDepositAmount : 0;
            entityPaginationListDto.TotalPaidAmountFromDeposit = await _accountStatementRepository.CalculateOverAllPaymentUsedFromDepositedAmount(contractId);
            entityPaginationListDto.TotalEncashedDepositedAmount = contractFromRepo.EncashDepositAmount ? (entityPaginationListDto.TotalExpectedDepositAmount - entityPaginationListDto.TotalPaidAmountFromDeposit) : 0;

            return Ok(entityPaginationListDto);
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.GetUnpaidAccountStatementsAsyncURL)]
        public async Task<ActionResult<AccountStatementPaginationListDto>> GetUnpaidAccountStatementsAsync()
        {
            var accountStatementsFromRepo = await _accountStatementRepository.GetUnpaidAccountStatementsAsync();

            var mappedAccountStatements = _mapper.Map<IEnumerable<AccountStatementForListDto>>(accountStatementsFromRepo);

            var entityPaginationListDto = _paginationService.PaginateEntityListGeneric<AccountStatementPaginationListDto>(mappedAccountStatements, 0);

            return Ok(entityPaginationListDto);
        }

        [HttpPost(ApiRoutes.AccountStatementControllerRoutes.UpdateWaterAndElectricBillAmountURL)]
        public async Task<IActionResult> UpdateWaterAndElectricBillAmount(AccountStatementWaterAndElectricBillUpdateDto accountStatementWaterAndElectricBillUpdateDto)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetEntity(accountStatementWaterAndElectricBillUpdateDto.Id).ToObjectAsync();

            if (accountStatementFromRepo == null)
            {
                return BadRequest("Account Statement does not exists!");
            }

            _accountStatementRepository.SetCurrentUserId(CurrentUserId);

            accountStatementFromRepo.WaterBill = accountStatementWaterAndElectricBillUpdateDto.WaterBillAmount;
            accountStatementFromRepo.ElectricBill = accountStatementWaterAndElectricBillUpdateDto.ElectricBillAmount;
            accountStatementFromRepo.DueDate = accountStatementWaterAndElectricBillUpdateDto.DueDate != "" ? Convert.ToDateTime(accountStatementWaterAndElectricBillUpdateDto.DueDate) : new DateTime(0001, 01, 01);

            if (await _accountStatementRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Updating water and electric bill amount for account statement with an id of {accountStatementFromRepo.Id} failed on save.");
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.GetFirstAccountStatementURL)]
        public async Task<ActionResult<AccountStatementDetailsDto>> GetFirstAccountStatement(int contractId)
        {
            var contractFromRepo = await _contractRepository
                                            .GetEntity(contractId)
                                            .ToObjectAsync();

            if (contractFromRepo == null)
                return BadRequest("Contract does not exists!");

            var firstAccountStatement = await _accountStatementRepository.GetFirstAccountStatement(contractId);

            var accountStatementToReturn = firstAccountStatement != null ? _mapper.Map<AccountStatementDetailsDto>(firstAccountStatement) : null;

            return Ok(accountStatementToReturn);
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.GetAccountStatementsAmountSummaryURL)]
        public async Task<ActionResult<AccountStatementsAmountSummary>> GetAccountStatementsAmountSummary()
        {
            var accountStatementsAmountSummary = await _accountStatementRepository.GetAccountStatementsAmountSummary();
            return Ok(accountStatementsAmountSummary);
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.SendAccountStatementURL)]
        public async Task<ActionResult> SendAccountStatement(int id)
        {
            await _accountStatementRepository.SendAccountStatement(id);
            return Ok();
        }

        [HttpGet(ApiRoutes.AccountStatementControllerRoutes.GetSuggestedNewAccountStatementURL)]
        public async Task<ActionResult<AccountStatementDetailsDto>> GetSuggestedNewAccountStatement(int contractId)
        {
            var suggestedAccountStatementFromRepo = await _accountStatementRepository.GetSuggestedNewAccountStatement(contractId);

            if (suggestedAccountStatementFromRepo == null)
                return NotFound("Account Statement not found.");

            var suggestedAccountStatementToReturn = _mapper.Map<AccountStatementDetailsDto>(suggestedAccountStatementFromRepo);

            return Ok(suggestedAccountStatementToReturn);
        }

        [HttpPost(ApiRoutes.AccountStatementControllerRoutes.CreateNewAccountStatementURL)]
        public async Task<ActionResult<AccountStatementDetailsDto>> CreateNewAccountStatement(NewAccountStatementDto newAccountStatementDto, bool marksAsPaid)
        {
            try
            {
                var accountStatement = _mapper.Map<AccountStatement>(newAccountStatementDto);

                // Generate single statement of accounts
                await _accountStatementRepository.GenerateNewContractAccountStatement(accountStatement, marksAsPaid, CurrentUserId);

                // Generate penalties
                await _accountStatementRepository.GenerateAccountStatementPenalties(accountStatement.ContractId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete(ApiRoutes.AccountStatementControllerRoutes.DeleteAccountStatementURL)]
        public async Task<IActionResult> DeleteAccountStatement(int accountStatementId)
        {
            var accountStatementFromRepo = await _accountStatementRepository.GetEntity(accountStatementId).ToObjectAsync();

            if (accountStatementFromRepo == null)
                return NotFound("Account Statement does not exists!");

            _accountStatementRepository
                .SetCurrentUserId(CurrentUserId)
                .DeleteEntity(accountStatementFromRepo);

            if (await _accountStatementRepository.SaveChanges())
                return NoContent();

            throw new Exception($"Error when deleting account statement with id of ${accountStatementId}!");
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateContractAccountStatementsForNewDatabaseURL)]
        public async Task<IActionResult> AutoGenerateContractAccountStatementsForNewDatabase(int currentUserId)
        {
            var activeTenantsFromRepo = await _tenantRepository.GetTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    try
                    {
                        await _accountStatementRepository.GenerateContractAccountStatementsRentalBillForNewDatabase(contract.Id, currentUserId);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateContractAccountStatementsURL)]
        public async Task<IActionResult> AutoGenerateContractAccountStatements()
        {
            var activeTenantsFromRepo = await _tenantRepository.GetTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    try
                    {
                        var dbHashName = _httpContextAccessor.HttpContext.Request.Headers["App-Secret-Token"].ToString();
                        await _accountStatementRepository.GenerateContractAccountStatementsForRentalBill(dbHashName, contract.Id, SendAccountStatementModeEnum.Automatic);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateAccountStatementPenaltiesURL)]
        public async Task<IActionResult> AutoGenerateAccountStatementPenalties()
        {
            var activeTenantsFromRepo = await _tenantRepository.GetTenantsAsync();

            foreach (var tenant in activeTenantsFromRepo)
            {
                var activeTenantContracts = await _contractRepository.GetActiveContractsPerTenantAsync(tenant.Id);

                foreach (var contract in activeTenantContracts)
                {
                    try
                    {
                        await _accountStatementRepository.GenerateAccountStatementPenalties(contract.Id);
                    }
                    catch (Exception exception)
                    {
                        throw new Exception(exception.Message);
                    }
                }
            }

            return Ok();
        }
    }
}