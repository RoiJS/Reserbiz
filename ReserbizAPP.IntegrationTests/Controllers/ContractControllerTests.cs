using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Constants;
using Xunit;

namespace ReserbizAPP.IntegrationTests.Controllers
{
    public class ContractControllerTests : BaseIntegrationTest
    {
        public ContractControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Theory]
        // Tenant: Patrick James
        // Unit: Room 105
        // Term Template: Term-0001
        // Open Contract: false
        // Duration Value: 12
        // Duration Unit: Month
        [InlineData(6, 4, 4, "C-0008", false, 12, DurationEnum.Month)]

        // Tenant: Snyder Malone
        // Unit: Room 103 - Bed Unit 3
        // Term Template: Term-0002
        // Open Contract: false
        // Duration Value: 4
        // Duration Unit: Quarter
        [InlineData(2, 6, 3, "C-0009", false, 4, DurationEnum.Quarter)]

        // Tenant: Patrick James
        // Unit: Room 105
        // Term Template: Term-0001
        // Open Contract: true
        // Duration Value: 0
        // Duration Unit: Day
        [InlineData(1, 1, 1, "C-00010", true, 0, DurationEnum.Day)]
        public async Task POST_CreateContract(int tenantId, int unitId, int termId, string contractCode, bool isOpenContract, int durationValue, DurationEnum durationUnit)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termDetailsResponse = await _client.GetFromJsonAsync<TermDetailDto>(ApiRoutes.TermControllerRoutes.GetTermURL.Replace("{id}", termId.ToString()));

            var termDetailsDto = new TermForManageDto
            {
                Code = termDetailsResponse.Code,
                TermParentId = termId,
                Name = termDetailsResponse.Name,
                SpaceTypeId = termDetailsResponse.SpaceTypeId,
                Rate = termDetailsResponse.Rate,
                MaximumNumberOfOccupants = termDetailsResponse.MaximumNumberOfOccupants,
                DurationUnit = termDetailsResponse.DurationUnit,
                AdvancedPaymentDurationValue = termDetailsResponse.AdvancedPaymentDurationValue,
                DepositPaymentDurationValue = termDetailsResponse.DepositPaymentDurationValue,
                ExcludeElectricBill = termDetailsResponse.ExcludeElectricBill,
                ElectricBillAmount = termDetailsResponse.ElectricBillAmount,
                ExcludeWaterBill = termDetailsResponse.ExcludeWaterBill,
                WaterBillAmount = termDetailsResponse.WaterBillAmount,
                PenaltyValue = termDetailsResponse.PenaltyValue,
                PenaltyValueType = termDetailsResponse.PenaltyValueType,
                PenaltyAmountPerDurationUnit = termDetailsResponse.PenaltyAmountPerDurationUnit,
                PenaltyEffectiveAfterDurationValue = termDetailsResponse.PenaltyEffectiveAfterDurationValue,
                PenaltyEffectiveAfterDurationUnit = termDetailsResponse.PenaltyEffectiveAfterDurationUnit,
                GenerateAccountStatementDaysBeforeValue = termDetailsResponse.GenerateAccountStatementDaysBeforeValue,
                AutoSendNewAccountStatement = termDetailsResponse.AutoSendNewAccountStatement,
                MiscellaneousDueDate = termDetailsResponse.MiscellaneousDueDate,
                IncludeMiscellaneousCheckAndCalculateForPenalty = termDetailsResponse.IncludeMiscellaneousCheckAndCalculateForPenalty,
                TermMiscellaneous = GetTermMiscellaneousManageDto(true, termDetailsResponse.TermMiscellaneous)
            };

            var contractToCreate = new ContractManageDto
            {
                Code = contractCode,
                TenantId = tenantId,
                TermId = termId,
                SpaceId = unitId,
                Term = termDetailsDto,
                EffectiveDate = DateTime.Now.AddMonths(-8).AddDays(15),
                IsOpenContract = isOpenContract,
            };

            // Only set Duration Value and Unit if Not Open Contract
            if (isOpenContract == false)
            {
                contractToCreate.DurationValue = durationValue;
                contractToCreate.DurationUnit = durationUnit;
            }

            // Act
            var createContractResponse = _client.PostAsJsonAsync<ContractManageDto>(ApiRoutes.ContractControllerRoutes.CreateContractURL, contractToCreate);
            var createContractContent = await createContractResponse.Result.Content.ReadFromJsonAsync<ContractDetailDto>();

            // Assert
            createContractResponse.Result.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContract = await _client.GetFromJsonAsync<ContractDetailDto>(ApiRoutes.ContractControllerRoutes.GetContractURL.Replace("{id}", createContractContent.Id.ToString()));
            var createdTermDetails = await _client.GetFromJsonAsync<TermDetailDto>(ApiRoutes.TermControllerRoutes.GetTermURL.Replace("{id}", createdContract.TermId.ToString()));

            createContractContent.Id.Should().Equals(createdContract.Id);
            createdContract.Code.Should().Equals(contractToCreate.Code);
            createdContract.TenantId.Should().Equals(contractToCreate.TenantId);
            createdContract.SpaceId.Should().Equals(contractToCreate.SpaceId);
            createdContract.EffectiveDate.Should().Equals(contractToCreate.EffectiveDate);
            createdContract.IsOpenContract.Should().Equals(contractToCreate.IsOpenContract);
            createdContract.DurationValue.Should().Equals(contractToCreate.DurationValue);
            createdContract.DurationUnit.Should().Equals(contractToCreate.DurationUnit);
            createdContract.DurationUnit.Should().Equals(contractToCreate.DurationUnit);

            createdContract.TermId.Should().NotBe(termId);
            createdContract.Term.Code.Should().Equals(contractToCreate.Term.Code);
            createdContract.Term.Name.Should().Equals(contractToCreate.Term.Name);

            for (var idx = 0; idx < createdTermDetails.TermMiscellaneous.Count; idx++)
            {
                var termMiscellaneousDto = termDetailsDto.TermMiscellaneous[idx];
                var createdTermMiscellaneous = createdTermDetails.TermMiscellaneous[idx];

                termMiscellaneousDto.Name.Should().Be(createdTermMiscellaneous.Name);
                termMiscellaneousDto.Description.Should().Be(createdTermMiscellaneous.Description);
                termMiscellaneousDto.Amount.Should().Be(createdTermMiscellaneous.Amount);
            }
        }

        [Theory]

        // Contract: C-0006
        // New Code: C-1006
        // New Unit: Room 106
        // Simulate Update Term Details: True
        [InlineData(1, "C-1006", 3, true)]

        // Contract: C-0005
        // New Code: C-1005
        // New Unit: Room 105
        // Simulate Update Term Details: False
        [InlineData(2, "C-1005", 4, false)]
        public async Task PUT_UpdateContract(int contractId, string newCode, int newUnitId, bool simulateUpdateTermDetails)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var contractDetails = await _client.GetFromJsonAsync<ContractDetailDto>(ApiRoutes.ContractControllerRoutes.GetContractURL.Replace("{id}", contractId.ToString()));

            var termDetailsResponse = await _client.GetFromJsonAsync<TermDetailDto>(ApiRoutes.TermControllerRoutes.GetTermURL.Replace("{id}", contractDetails.TermId.ToString()));

            // Keep a copy of the original term id to compare later.
            var originalTermId = termDetailsResponse.Id;

            var termDetailsDto = new TermForManageDto
            {
                Code = termDetailsResponse.Code,
                TermParentId = termDetailsResponse.TermParentId,
                Name = termDetailsResponse.Name,
                SpaceTypeId = termDetailsResponse.SpaceTypeId,
                Rate = termDetailsResponse.Rate,
                MaximumNumberOfOccupants = termDetailsResponse.MaximumNumberOfOccupants,
                DurationUnit = termDetailsResponse.DurationUnit,
                AdvancedPaymentDurationValue = termDetailsResponse.AdvancedPaymentDurationValue,
                DepositPaymentDurationValue = termDetailsResponse.DepositPaymentDurationValue,
                ExcludeElectricBill = termDetailsResponse.ExcludeElectricBill,
                ElectricBillAmount = termDetailsResponse.ElectricBillAmount,
                ExcludeWaterBill = termDetailsResponse.ExcludeWaterBill,
                WaterBillAmount = termDetailsResponse.WaterBillAmount,
                PenaltyValue = termDetailsResponse.PenaltyValue,
                PenaltyValueType = termDetailsResponse.PenaltyValueType,
                PenaltyAmountPerDurationUnit = termDetailsResponse.PenaltyAmountPerDurationUnit,
                PenaltyEffectiveAfterDurationValue = termDetailsResponse.PenaltyEffectiveAfterDurationValue,
                PenaltyEffectiveAfterDurationUnit = termDetailsResponse.PenaltyEffectiveAfterDurationUnit,
                GenerateAccountStatementDaysBeforeValue = termDetailsResponse.GenerateAccountStatementDaysBeforeValue,
                AutoSendNewAccountStatement = termDetailsResponse.AutoSendNewAccountStatement,
                MiscellaneousDueDate = termDetailsResponse.MiscellaneousDueDate,
                IncludeMiscellaneousCheckAndCalculateForPenalty = termDetailsResponse.IncludeMiscellaneousCheckAndCalculateForPenalty,
                TermMiscellaneous = GetTermMiscellaneousManageDto(simulateUpdateTermDetails, termDetailsResponse.TermMiscellaneous)
            };

            if (!simulateUpdateTermDetails)
            {
                termDetailsDto.Id = originalTermId;
            }

            var contractToUpdate = new ContractManageDto
            {
                Code = newCode,
                TenantId = contractDetails.TenantId,
                TermId = contractDetails.TermId,
                SpaceId = newUnitId,
                Term = termDetailsDto,
                EffectiveDate = contractDetails.EffectiveDate,
                IsOpenContract = contractDetails.IsOpenContract,
            };

            // Act
            var url = ApiRoutes.ContractControllerRoutes.UpdateContractURL.Replace("{contractId}", contractId.ToString()).Replace("{termId}", originalTermId.ToString());
            var updateContractResponse = _client.PutAsJsonAsync<ContractManageDto>(url, contractToUpdate);

            // Assert
            updateContractResponse.Result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var updatedContractDetails = await _client.GetFromJsonAsync<ContractDetailDto>(ApiRoutes.ContractControllerRoutes.GetContractURL.Replace("{id}", contractId.ToString()));
            var updatedTermDetails = await _client.GetFromJsonAsync<TermDetailDto>(ApiRoutes.TermControllerRoutes.GetTermURL.Replace("{id}", updatedContractDetails.TermId.ToString()));

            contractId.Should().Equals(updatedContractDetails.Id);
            updatedContractDetails.Code.Should().Equals(contractToUpdate.Code);
            updatedContractDetails.TenantId.Should().Equals(contractToUpdate.TenantId);
            updatedContractDetails.SpaceId.Should().Equals(contractToUpdate.SpaceId);
            updatedContractDetails.EffectiveDate.Should().Equals(contractToUpdate.EffectiveDate);
            updatedContractDetails.IsOpenContract.Should().Equals(contractToUpdate.IsOpenContract);
            updatedContractDetails.DurationValue.Should().Equals(contractToUpdate.DurationValue);
            updatedContractDetails.DurationUnit.Should().Equals(contractToUpdate.DurationUnit);
            updatedContractDetails.DurationUnit.Should().Equals(contractToUpdate.DurationUnit);
            updatedContractDetails.Term.Code.Should().Equals(contractToUpdate.Term.Code);
            updatedContractDetails.Term.Name.Should().Equals(contractToUpdate.Term.Name);

            if (!simulateUpdateTermDetails)
            {
                updatedContractDetails.TermId.Should().Be(originalTermId);
            }
            else
            {
                updatedContractDetails.TermId.Should().NotBe(originalTermId);
            }

            for (var idx = 0; idx < updatedTermDetails.TermMiscellaneous.Count; idx++)
            {
                var termMiscellaneousDto = termDetailsDto.TermMiscellaneous[idx];
                var createdTermMiscellaneous = updatedTermDetails.TermMiscellaneous[idx];

                termMiscellaneousDto.Name.Should().Be(createdTermMiscellaneous.Name);
                termMiscellaneousDto.Description.Should().Be(createdTermMiscellaneous.Description);
                termMiscellaneousDto.Amount.Should().Be(createdTermMiscellaneous.Amount);
            }
        }


        [Theory]
        [InlineData(3, 2)]
        [InlineData(2, 2)]
        [InlineData(4, 1)]
        public async Task GET_GetContractsPerTenant(int tenantId, int expectedContractCount)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var contractsPerTenants = await _client.GetFromJsonAsync<List<ContractListDto>>(ApiRoutes.ContractControllerRoutes.GetContractsPerTenantURL.Replace("{tenantId}", tenantId.ToString()));

            // Assert
            contractsPerTenants.Count.Should().Be(expectedContractCount);
        }

        [Theory]
        [InlineData(6, true)]
        [InlineData(5, false)]
        [InlineData(7, true)]
        public async Task PUT_SetEncashDepositAmountStatus(int contractId, bool status)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var url = ApiRoutes.ContractControllerRoutes.SetEncashDepositAmountStatusURL
                            .Replace("{contractId}", contractId.ToString())
                            .Replace("{status}", status.ToString());

            var setEncashDepositAmountStatusResponse = await _client.PutAsync(url, null);

            // Assert
            setEncashDepositAmountStatusResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var contracDetails = await _client.GetFromJsonAsync<ContractDetailDto>(ApiRoutes.ContractControllerRoutes.GetContractURL.Replace("{id}", contractId.ToString()));
            contracDetails.EncashDepositAmount.Should().Be(status);
        }

        [Fact]
        public async Task GET_GetActiveContractsCount()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var url = ApiRoutes.ContractControllerRoutes.GetActiveContractsCountURL;
            var activeContractCountResponse = await _client.GetAsync(url);

            // Assert
            var expectedActiveContractCount = 6;
            activeContractCountResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var activeContractCountContent = await activeContractCountResponse.Content.ReadFromJsonAsync<int>();
            activeContractCountContent.Should().Be(expectedActiveContractCount);
        }
        
        private List<TermMiscellaneousManageDto> GetTermMiscellaneousManageDto(bool newContract, List<TermMiscellaneousDetailDto> termMiscellaneous)
        {
            var termMiscellaneousDto = new List<TermMiscellaneousManageDto>();

            foreach (var item in termMiscellaneous)
            {
                var termMiscellaneousDetails = new TermMiscellaneousManageDto
                {
                    Name = item.Name,
                    Description = item.Description,
                    Amount = item.Amount,
                };

                if (!newContract)
                {
                    termMiscellaneousDetails.Id = item.Id;
                }

                termMiscellaneousDto.Add(termMiscellaneousDetails);
            }

            return termMiscellaneousDto;
        }
    }
}