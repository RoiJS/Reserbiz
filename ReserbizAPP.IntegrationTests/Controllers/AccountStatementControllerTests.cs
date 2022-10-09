using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Constants;
using ReserbizAPP.LIB.Models;
using Xunit;

namespace ReserbizAPP.IntegrationTests.Controllers
{
    public class AccountStatementControllerTests : BaseIntegrationTest
    {
        public AccountStatementControllerTests(ApiWebApplicationFactory apiWebApplicationFactory)
            : base(apiWebApplicationFactory)
        {

        }

        [Fact]
        public async Task POST_CreateNewAccountStatement_ForRentalAccountStatementType_And_MarkAsPaidTrue()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newAccountStatementDto = new NewAccountStatementDto();
            newAccountStatementDto.ContractId = 1;
            newAccountStatementDto.AccountStatementType = AccountStatementTypeEnum.RentalBill;

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.CreateNewAccountStatementURL.Replace("{marksAsPaid}", "true");
            var createNewAccountStatementResponse = await _client.PostAsJsonAsync<NewAccountStatementDto>(url, newAccountStatementDto);

            // Assert
            createNewAccountStatementResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_CreateNewAccountStatement_ForRentalAccountStatementType_And_MarkAsPaidFalse()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newAccountStatementDto = new NewAccountStatementDto();
            newAccountStatementDto.ContractId = 1;
            newAccountStatementDto.AccountStatementType = AccountStatementTypeEnum.RentalBill;

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.CreateNewAccountStatementURL.Replace("{marksAsPaid}", "false");
            var createNewAccountStatementResponse = await _client.PostAsJsonAsync<NewAccountStatementDto>(url, newAccountStatementDto);

            // Assert
            createNewAccountStatementResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_CreateNewAccountStatement_ForUtilityAccountStatementType_And_MarkAsPaidTrue()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newAccountStatementDto = new NewAccountStatementDto();
            newAccountStatementDto.ContractId = 1;
            newAccountStatementDto.ElectricBill = 150;
            newAccountStatementDto.WaterBill = 250;
            newAccountStatementDto.DueDate = DateTime.Now;
            newAccountStatementDto.AccountStatementType = AccountStatementTypeEnum.UtilityBill;

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.CreateNewAccountStatementURL.Replace("{marksAsPaid}", "true");
            var createNewAccountStatementResponse = await _client.PostAsJsonAsync<NewAccountStatementDto>(url, newAccountStatementDto);

            // Assert
            createNewAccountStatementResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_CreateNewAccountStatement_ForUtilityAccountStatementType_And_MarkAsPaidFalse()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newAccountStatementDto = new NewAccountStatementDto();
            newAccountStatementDto.ContractId = 1;
            newAccountStatementDto.ElectricBill = 150;
            newAccountStatementDto.WaterBill = 250;
            newAccountStatementDto.DueDate = DateTime.Now;
            newAccountStatementDto.AccountStatementType = AccountStatementTypeEnum.UtilityBill;

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.CreateNewAccountStatementURL.Replace("{marksAsPaid}", "false");
            var createNewAccountStatementResponse = await _client.PostAsJsonAsync<NewAccountStatementDto>(url, newAccountStatementDto);

            // Assert
            createNewAccountStatementResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        // Commented out. There is unknown issue on sending email. Needs to further investigate.
        // [Theory]
        // [InlineData(1)]
        // [InlineData(17)]
        // [InlineData(29)]
        // public async Task GET_SendAccountStatement(int accountStatementId)
        // {
        //     // Arrange
        //     await InitializeAuthorizationAndTestDataAsync();

        //     // Act
        //     var url = ApiRoutes.AccountStatementControllerRoutes.SendAccountStatementURL.Replace("{id}", accountStatementId.ToString());
        //     var response = await _client.GetAsync(url);

        //     // Assert
        //     response.StatusCode.Should().Be(HttpStatusCode.OK);
        // }

        [Theory]
        [InlineData(1, 6)]
        [InlineData(17, 5)]
        [InlineData(29, 7)]
        public async Task GET_GetFirstAccountStatement(int accountStatementId, int contractId)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.GetFirstAccountStatementURL.Replace("{contractId}", contractId.ToString());
            var firstAccountStatement = await _client.GetFromJsonAsync<AccountStatementDetailsDto>(url);

            // Assert
            firstAccountStatement.Id.Should().Be(accountStatementId);
        }

        [Fact]
        public async Task GET_GetAccountStatementsAmountSummary()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.GetAccountStatementsAmountSummaryURL;
            var accountStatementsAmountSummary = await _client.GetFromJsonAsync<AccountStatementsAmountSummary>(url);

            // Assert
            accountStatementsAmountSummary.TotalAmountPaid.Should().Be(560050);
            accountStatementsAmountSummary.TotalUnpaidAmount.Should().Be(0);
        }

        [Fact]
        public async Task GET_GetSuggestedNewAccountStatement()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();
            var contractId = 1;

            // Act
            var url = ApiRoutes.AccountStatementControllerRoutes.GetSuggestedNewAccountStatementURL.Replace("{contractId}", contractId.ToString());
            var suggestedAccountStatement = await _client.GetFromJsonAsync<AccountStatementDetailsDto>(url);

            // Assert
            suggestedAccountStatement.Should().NotBeNull();
        }

        [Fact]
        public async Task POST_AutoGenerateContractAccountStatements()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var result = await _client.PostAsync(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateContractAccountStatementsURL, null);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_AutoGenerateAccountStatementPenalties()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var result = await _client.PostAsync(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateAccountStatementPenaltiesURL, null);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}