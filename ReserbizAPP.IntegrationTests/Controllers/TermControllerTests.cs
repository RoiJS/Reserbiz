using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Helpers.Constants;
using Xunit;

namespace ReserbizAPP.IntegrationTests.Controllers
{
    public class TermControllerTests : BaseIntegrationTest
    {
        public TermControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {

        }

        #region "CreateTerm-Code"
        [Fact]
        public async Task POST_CreateTerm_WithCodeLessThan20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithCodeEqualTo20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.Code = "12345678912345678901";

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithCodeGreaterThan20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.Code = "123456789123456789012";

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region "CreateTerm-AdvancedPaymentDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_CreateTerm_WithAdvancedPaymentDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.AdvancedPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_CreateTerm_WithAdvancedPaymentDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.AdvancedPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_CreateTerm_WithAdvancedPaymentDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.AdvancedPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        #endregion

        #region "CreateTerm-DepositPaymentDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_CreateTerm_WithDepositPaymentDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.DepositPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_CreateTerm_WithDepositPaymentDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.DepositPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_CreateTerm_WithDepositPaymentDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.DurationUnit = durationUnit;
            newTerm.DepositPaymentDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        #endregion

        #region "CreateTerm-PenaltyEffectiveAfterDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_CreateTerm_WithPenaltyEffectiveAfterDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.PenaltyEffectiveAfterDurationUnit = durationUnit;
            newTerm.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_CreateTerm_WithPenaltyEffectiveAfterDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.PenaltyEffectiveAfterDurationUnit = durationUnit;
            newTerm.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_CreateTerm_WithPenaltyEffectiveAfterDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.PenaltyEffectiveAfterDurationUnit = durationUnit;
            newTerm.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        #endregion

        #region "CreateTerm-ExcludeElectricBill"
        [Fact]
        public async Task POST_CreateTerm_WithExcludeElectricBillSetToTrue_And_ElectricBillAmountIsEqualToZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeElectricBill = true;
            newTerm.ElectricBillAmount = 0;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithExcludeElectricBillSetToTrue_And_ElectricBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeElectricBill = true;
            newTerm.ElectricBillAmount = 150;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithExcludeElectricBillSetToFalse_And_ElectricBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeElectricBill = false;
            newTerm.ElectricBillAmount = 150;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region  "CreateTerm-ExcludeWaterBill"
        [Fact]
        public async Task POST_CreateTerm_WithExcludeWaterBillSetToTrue_And_WaterBillAmountIsEqualToZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeWaterBill = true;
            newTerm.WaterBillAmount = 0;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithExcludeWaterBillSetToTrue_And_WaterBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeWaterBill = true;
            newTerm.WaterBillAmount = 150;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_WithExcludeWaterBillSetToFalse_And_WaterBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            newTerm.ExcludeWaterBill = false;
            newTerm.WaterBillAmount = 150;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region "CreateTerm-TermMiscellaneousName"
        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousNameHasLessThan100Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousName = new string('t', 99);
            newTerm.TermMiscellaneous[0].Name = termMiscellaneousName;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousNameHasEqualTo100Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousName = new string('t', 100);
            newTerm.TermMiscellaneous[0].Name = termMiscellaneousName;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousNameHasGreaterThan100Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousName = new string('t', 101);
            newTerm.TermMiscellaneous[0].Name = termMiscellaneousName;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion

        #region "CreateTerm-TermMiscellaneousDescription"
        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousDescriptionHasLessThan200Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousDescription = new string('t', 199);
            newTerm.TermMiscellaneous[0].Description = termMiscellaneousDescription;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousDescriptionHasEqualTo200Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousDescription = new string('t', 200);
            newTerm.TermMiscellaneous[0].Description = termMiscellaneousDescription;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task POST_CreateTerm_TermMiscellaneousDescriptionHasGreaterThan200Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var newTerm = TestTermData();
            var termMiscellaneousDescription = new string('t', 201);
            newTerm.TermMiscellaneous[0].Description = termMiscellaneousDescription;

            // Act
            var createTermResponse = await _client.PostAsJsonAsync<TermForManageDto>(ApiRoutes.TermControllerRoutes.CreateTermURL, newTerm);

            // Assert
            createTermResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        #endregion


        #region "UpdateTerm-Code"
        [Fact]
        public async Task PUT_UpdateTerm_WithCodeLessThan20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Code = "Term-0005";

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task PUT_UpdateTerm_WithCodeEqualTo20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Code = "Term-678912345678901";

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task PUT_UpdateTerm_WithCodeGreaterThan20Characters()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Code = "Term-6789123456789012";

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region "UpdateTerm-AdvancedPaymentDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_UpdateTerm_WithAdvancedPaymentDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.AdvancedPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_UpdateTerm_WithAdvancedPaymentDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.AdvancedPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_UpdateTerm_WithAdvancedPaymentDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.AdvancedPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region "UpdateTerm-DepositPaymentDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_UpdateTerm_WithDepositPaymentDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.DepositPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_UpdateTerm_WithDepositPaymentDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.DepositPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_UpdateTerm_WithDepositPaymentDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.DurationUnit = durationUnit;
            termDetailsDto.DepositPaymentDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region "UpdateTerm-PenaltyEffectiveAfterDurationValue"
        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE - 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE - 1))]
        public async Task POST_UpdateTerm_WithPenaltyEffectiveAfterDurationValueLessThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.PenaltyEffectiveAfterDurationUnit = durationUnit;
            termDetailsDto.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(DurationEnum.Day, (SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Week, (SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Month, (SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Quarter, (SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE + 1))]
        [InlineData(DurationEnum.Year, (SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE + 1))]
        public async Task POST_UpdateTerm_WithPenaltyEffectiveAfterDurationValueGreaterThanTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.PenaltyEffectiveAfterDurationUnit = durationUnit;
            termDetailsDto.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(DurationEnum.Day, SystemConstants.DurationUnitRangeValues.DAY_MAX_VALUE)]
        [InlineData(DurationEnum.Week, SystemConstants.DurationUnitRangeValues.WEEK_MAX_VALUE)]
        [InlineData(DurationEnum.Month, SystemConstants.DurationUnitRangeValues.MONTH_MAX_VALUE)]
        [InlineData(DurationEnum.Quarter, SystemConstants.DurationUnitRangeValues.QUARTER_MAX_VALUE)]
        [InlineData(DurationEnum.Year, SystemConstants.DurationUnitRangeValues.YEAR_MAX_VALUE)]
        public async Task POST_UpdateTerm_WithPenaltyEffectiveAfterDurationValueEqualToTheMaximumValue(DurationEnum durationUnit, int durationValue)
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.PenaltyEffectiveAfterDurationUnit = durationUnit;
            termDetailsDto.PenaltyEffectiveAfterDurationValue = durationValue;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region "UpdateTerm-ExcludeElectricBill"
        [Fact]
        public async Task POST_UpdateTerm_WithExcludeElectricBillSetToTrue_And_ElectricBillAmountIsEqualToZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeElectricBill = true;
            termDetailsDto.ElectricBillAmount = 0;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_UpdateTerm_WithExcludeElectricBillSetToTrue_And_ElectricBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeElectricBill = true;
            termDetailsDto.ElectricBillAmount = 150;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_UpdateTerm_WithExcludeElectricBillSetToFalse_And_ElectricBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeElectricBill = false;
            termDetailsDto.ElectricBillAmount = 150;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region  "UpdateTerm-ExcludeWaterBill"
        [Fact]
        public async Task POST_UpdateTerm_WithExcludeWaterBillSetToTrue_And_WaterBillAmountIsEqualToZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeWaterBill = true;
            termDetailsDto.WaterBillAmount = 0;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_UpdateTerm_WithExcludeWaterBillSetToTrue_And_WaterBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeWaterBill = true;
            termDetailsDto.WaterBillAmount = 150;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task POST_UpdateTerm_WithExcludeWaterBillSetToFalse_And_WaterBillAmountIsEqualGreaterThanZero()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            var termId = 1;
            var termDetailsDto = await TermDetailsToUpdate(termId);
            termDetailsDto.Name = "Term-0005";
            termDetailsDto.ExcludeWaterBill = false;
            termDetailsDto.WaterBillAmount = 150;

            // Act
            var url = ApiRoutes.TermControllerRoutes.UpdateTermURL.Replace("{id}", termId.ToString());
            var updateTermDetailsResponse = await _client.PutAsJsonAsync<TermForUpdateDto>(url, termDetailsDto);

            // Assert
            updateTermDetailsResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        private async Task<TermForUpdateDto> TermDetailsToUpdate(int termId)
        {
            var termDetailsResponse = await _client.GetFromJsonAsync<TermDetailDto>(ApiRoutes.TermControllerRoutes.GetTermURL.Replace("{id}", termId.ToString()));

            var termDetailsDto = new TermForUpdateDto
            {
                Code = termDetailsResponse.Code,
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
            };

            return termDetailsDto;
        }

        private TermForManageDto TestTermData()
        {
            var unitTypeId = 1; // Room (Non-Studio Type)
            var newTerm = new TermForManageDto
            {
                Code = "0001",
                Name = "Term-0001",
                SpaceTypeId = unitTypeId,
                Rate = 9000,
                MaximumNumberOfOccupants = 2,
                DurationUnit = DurationEnum.Month,
                AdvancedPaymentDurationValue = 1,
                DepositPaymentDurationValue = 2,
                ExcludeElectricBill = true,
                ElectricBillAmount = 550,
                ExcludeWaterBill = true,
                WaterBillAmount = 350,
                TermMiscellaneous = new List<TermMiscellaneousManageDto> {
                        new TermMiscellaneousManageDto
                        {
                            Name = "Utility Fee",
                            Description = "Esse voluptate ullamco nostrud quis velit excepteur.",
                            Amount = 500
                        },
                        new TermMiscellaneousManageDto
                        {
                            Name = "Parking Fee",
                            Description = "Duis eiusmod officia fugiat labore duis enim eiusmod laborum magn",
                            Amount = 1000
                        },
                    },

                PenaltyValue = 50,
                PenaltyValueType = ValueTypeEnum.Fixed,
                PenaltyAmountPerDurationUnit = DurationEnum.Day,
                PenaltyEffectiveAfterDurationValue = 3,
                PenaltyEffectiveAfterDurationUnit = DurationEnum.Day,
                GenerateAccountStatementDaysBeforeValue = 5,
                MiscellaneousDueDate = MiscellaneousDueDateEnum.SameWithRentalDueDate
            };

            return newTerm;
        }
    }
}