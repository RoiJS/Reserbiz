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
    public class AuthControllerTests : BaseIntegrationTest
    {
        public AuthControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task POST_Register()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();
            var newAccount = new AccountForRegisterDto
            {
                FirstName = "Jihyo",
                MiddleName = "Park",
                LastName = "Park",
                Gender = GenderEnum.Female,
                Username = "jipa",
                Password = "Starta123",
                EmailAddress = "jihyo.park@gmail.com"
            };

            // Act
            var registerNewAccountResult = await _client.PostAsJsonAsync<AccountForRegisterDto>(ApiRoutes.AuthControllerRoutes.RegisterURL, newAccount);

            // Assert
            registerNewAccountResult.StatusCode.Should().Be(HttpStatusCode.OK);
            var createdAccount = await registerNewAccountResult.Content.ReadFromJsonAsync<AccountForListDto>();
            var getNewAccount =  await _client.GetFromJsonAsync<AccountForListDto>(ApiRoutes.AuthControllerRoutes.GetAccountURL.Replace("{id}", createdAccount.Id.ToString()));
            getNewAccount.FirstName.Should().Be(newAccount.FirstName);
            getNewAccount.MiddleName.Should().Be(newAccount.MiddleName);
            getNewAccount.LastName.Should().Be(newAccount.LastName);
            getNewAccount.Username.Should().Be(newAccount.Username);
        }

        [Fact]
        public async Task POST_RefreshToken()
        {
            // Arrange
            await InitializeAuthorizationAndTestDataAsync();

            // Act
            var refreshTokenResult = await _client.PostAsJsonAsync(ApiRoutes.AuthControllerRoutes.RefreshTokenURL, new RefreshRequestDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });

            // Assert
            refreshTokenResult.StatusCode.Should().Be(HttpStatusCode.OK);
            var refreshTokenResponse = await refreshTokenResult.Content.ReadFromJsonAsync<AuthenticationTokenInfoDto>();

            refreshTokenResponse.RefreshToken.Should().NotBeNullOrEmpty();
            refreshTokenResponse.RefreshToken.Should().NotBeEquivalentTo(refreshToken);
        }
    }
}
