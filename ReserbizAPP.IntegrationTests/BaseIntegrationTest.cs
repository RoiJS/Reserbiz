using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Helpers.Constants;
using Respawn;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ReserbizAPP.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        protected readonly ApiWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        private readonly IOptions<DefaultUserAccountDetails> _defaultUserAccountDetails;
        private readonly IOptions<DefaultAccountCredentials> _defaultAccountCredentials;

        protected string refreshToken;
        protected string accessToken;

        public BaseIntegrationTest(ApiWebApplicationFactory fixture)
        {
            _factory = fixture;
            _client = _factory.CreateClient();

            _defaultUserAccountDetails = _factory.Services.GetService<IOptions<DefaultUserAccountDetails>>();
            _defaultAccountCredentials = _factory.Services.GetService<IOptions<DefaultAccountCredentials>>();

            // Console.WriteLine($"Integration Test Connection String: {_factory.Configuration.GetConnectionString("ReserbizClientDeveloperIntegrationTestDBConnection")}");
        }

        protected async Task AuthenticateAsync()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        protected async Task SeedTestDataAsync()
        {
            var clientInfo = await GetClientInformation();
            var clientInformationDto = new ClientInformationDto();

            var defaultAccounId = 1;
            var userAccountDto = new UserAccountDto();
            userAccountDto.FirstName = _defaultUserAccountDetails.Value.Firstname;
            userAccountDto.MiddleName = _defaultUserAccountDetails.Value.Middlename;
            userAccountDto.LastName = _defaultUserAccountDetails.Value.Lastname;
            userAccountDto.EmailAddress = _defaultUserAccountDetails.Value.EmailAddress;

            var clientDto = new ClientDto();
            clientDto.Name = clientInfo.Name;
            clientDto.DBHashName = clientInfo.DBHashName;
            clientDto.Type = ClientTypeEnum.Demo;

            clientInformationDto.UserAccountDto = userAccountDto;
            clientInformationDto.ClientDto = clientDto;

            var populateDbResponse = await _client.PostAsJsonAsync(ApiRoutes.ClientDbManagerControllerRoutes.PopulateDatabaseURL, clientInformationDto);

            var autoGenerateAccountStatement = await _client.PostAsync(ApiRoutes.AccountStatementControllerRoutes.AutoGenerateContractAccountStatementsForNewDatabaseURL.Replace("{currentUserId}", defaultAccounId.ToString()), null);
        }

        protected async Task SyncClientDatabaseSchemaAsync()
        {
            await _client.PostAsJsonAsync(ApiRoutes.ClientDbManagerControllerRoutes.SyncDatabaseURL, "");
        }

        protected async Task AddAppSecretTokenToHeaderAsync()
        {
            var company = await GetClientInformation();
            _client.DefaultRequestHeaders.Add("App-Secret-Token", company.DBHashName);
        }

        protected void AddIntegrationTestIndicatorToHeaderAsync()
        {
            _client.DefaultRequestHeaders.Add("For-Integration-Test", "1");
        }

        protected async Task InitializeAuthorizationAndTestDataAsync()
        {
            AddIntegrationTestIndicatorToHeaderAsync();
            await AddAppSecretTokenToHeaderAsync();
            await SyncClientDatabaseSchemaAsync();
            await ResetClientDatabase();
            await SeedTestDataAsync();
            await AuthenticateAsync();
        }

        private async Task ResetClientDatabase()
        {
            var connection = _factory.Configuration.GetConnectionString("ReserbizClientDeveloperIntegrationTestDBConnection");
            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
            {
                WithReseed = true
            });

            await respawner.ResetAsync(connection);
        }

        private async Task<ClientDetailsDto> GetClientInformation()
        {
            var companyName = _factory.Configuration.GetSection("DefaultAccountCredentials:Company").Value.ToString();
            var getClientInfoResponse = await _client.GetFromJsonAsync<ClientDetailsDto>(ApiRoutes.ClientsControllerRoutes.GetClientInformationURL.Replace("{clientName}", companyName));
            return getClientInfoResponse;
        }

        private async Task<string> GetJwtAsync()
        {
            var username = _factory.Configuration.GetSection("DefaultAccountCredentials:Username").Value.ToString();
            var password = _factory.Configuration.GetSection("DefaultAccountCredentials:Password").Value.ToString();

            var response = await _client.PostAsJsonAsync(ApiRoutes.AuthControllerRoutes.LoginURL, new AccountForLoginDto
            {
                Username = username,
                Password = password
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthenticationTokenInfoDto>();

            // We keep the refresh token for future use
            refreshToken = registrationResponse.RefreshToken;

            // We keep the access token for future use
            accessToken = registrationResponse.AccessToken;

            return registrationResponse.AccessToken;
        }

    }
}
