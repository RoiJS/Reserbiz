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

    public class PaymentBreakdownControllerTests : BaseIntegrationTest
    {

        public PaymentBreakdownControllerTests(ApiWebApplicationFactory apiWebApplicationFactory)
            : base(apiWebApplicationFactory)
        {

        }

        [Theory]
        [InlineData(1, 3000, PaymentForTypeEnum.Rental)]
        [InlineData(1, 500, PaymentForTypeEnum.ElectricBill)]
        [InlineData(1, 250, PaymentForTypeEnum.WaterBill)]
        [InlineData(1, 1500, PaymentForTypeEnum.MiscellaneousFee)]
        [InlineData(1, 100, PaymentForTypeEnum.Penalty)]
        public async Task POST_AddPayment(int accountStatementId, float amount, PaymentForTypeEnum paymentForType)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();
            var newPayment = new PaymentBreakdownForDetailsDto
            {
                Amount = amount,
                PaymentForType = paymentForType
            };

            // Act
            var url = $"{ApiRoutes.PaymentBreakdownControllerRoutes.AddPaymentURL}?accountStatementId={accountStatementId}";
            var paymentResult = await _client.PostAsJsonAsync<PaymentBreakdownForDetailsDto>(url, newPayment);

            // Assert
            paymentResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GET_GetPaymentDetails()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();
            var paymentId = 1;

            // Act
            var url = ApiRoutes.PaymentBreakdownControllerRoutes.GetPaymentDetailsURL.Replace("{id}", paymentId.ToString());

            var paymentDetails = _client.GetFromJsonAsync<PaymentBreakdownForDetailsDto>(url);

            // Assert
            paymentDetails.Should().NotBeNull();
        }
    }
}