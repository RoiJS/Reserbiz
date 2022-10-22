using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Dtos;
using ReserbizAPP.LIB.Enums;
using ReserbizAPP.LIB.Helpers;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Helpers.Services;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;
using RestSharp;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ClientRepository
        : BaseRepository<Client>, IClientRepository<Client>
    {
        private readonly IOptions<AppSettingsURL> _appSettingsUrl;
        private readonly IOptions<AppHostInfo> _appHostInfo;
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IOptions<EmailServerSettings> _emailServerSettings;

        public ClientRepository(
                IReserbizRepository<Client> reserbizRepository,
                IOptions<AppSettingsURL> appSettingsUrl,
                IOptions<AppHostInfo> appHostInfo,
                IOptions<ApplicationSettings> appSettings,
                IOptions<EmailServerSettings> emailServerSettings
                )
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {
            _appSettingsUrl = appSettingsUrl;
            _appHostInfo = appHostInfo;
            _appSettings = appSettings;
            _emailServerSettings = emailServerSettings;
        }

        public async Task<Client> RegisterClient(Client client)
        {
            client.DBName = GenerateClientDbName(client);

            return await RegisterClientInformation(client);
        }

        public async Task<Client> RegisterDemo(Client client)
        {
            client.DBName = await GenerateDemoDbName(client);

            return await RegisterClientInformation(client);
        }

        public async Task<Client> GetCompanyInfoByName(string companyName)
        {
            var companyInfo = await _reserbizRepository.SystemDbContext.Clients.FirstOrDefaultAsync(c => c.Name == companyName);

            return companyInfo;
        }

        public async Task CreateClientDatabase(Client client)
        {
            try
            {
                var url = String.Format("{0}{1}", _appHostInfo.Value.Domain, _appSettings.Value.AppSettingsURL.SyncDatabaseURL);
                var httpClient = new RestClient(url);
                httpClient.Options.MaxTimeout = -1;
                var httpRequest = new RestRequest(url, Method.Post);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                RestResponse response = await httpClient.ExecuteAsync(httpRequest);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task PopulateDatabase(UserAccount userAccount, Client client, Action<UserAccount> sendEmaiNotification)
        {
            try
            {
                var userAccountDto = new UserAccountDto
                {
                    FirstName = userAccount.FirstName,
                    MiddleName = userAccount.MiddleName,
                    LastName = userAccount.LastName,
                    EmailAddress = userAccount.EmailAddress
                };

                var clientDto = new ClientDto
                {
                    Name = client.Name,
                    DBHashName = client.DBHashName,
                    Type = client.Type,
                    ContactNumber = client.ContactNumber
                };

                var url = String.Format("{0}{1}", _appHostInfo.Value.Domain, _appSettings.Value.AppSettingsURL.PopulateDatabaseURL);
                var httpClient = new RestClient(url);
                httpClient.Options.MaxTimeout = -1;
                var httpRequest = new RestRequest(url, Method.Post);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                httpRequest.AddJsonBody(new
                {
                    UserAccountDto = userAccountDto,
                    ClientDto = clientDto
                });
                var response = await httpClient.ExecuteAsync<UserAccount>(httpRequest);

                var responseUserAccount = response.Data;
                userAccount.Username = responseUserAccount.Username;
                userAccount.Password = responseUserAccount.Password;

                sendEmaiNotification(response.Data);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void SendNewClientRegisteredEmailNotification(UserAccount userAccount, Client client)
        {
            var emailContent = GenerateNewClientRegisteredNotificationContent(userAccount, client);
            var subject = "New Client Account";

            SendEmailNotification(
                subject,
                emailContent,
                _appSettings.Value.ClientDatabaseNotificationSettings.SenderEmailAddress,
                userAccount.EmailAddress,
                _appSettings.Value.ClientDatabaseNotificationSettings.BCCEmailAddress
            );
        }

        public void SendNewDemoRegisteredEmailNotification(UserAccount userAccount, Client client)
        {
            var emailContent = GenerateNewDemoRegisteredNotificationContent(userAccount, client);
            var subject = "New Demo Account";

            SendEmailNotification(
                subject,
                emailContent,
                _appSettings.Value.ClientDatabaseNotificationSettings.SenderEmailAddress,
                userAccount.EmailAddress,
                _appSettings.Value.ClientDatabaseNotificationSettings.BCCEmailAddress
            );
        }

        private void SendEmailNotification(string subject, string content, string emailSender, string emailReceiver, string bcc)
        {
            try
            {
                var emailService = new EmailService(
                                _emailServerSettings.Value.SmtpServer,
                                emailSender,
                                _emailServerSettings.Value.SmtpPassword,
                                _emailServerSettings.Value.SmtpPort
                            );

                emailService.Send(
                    emailSender,
                    emailReceiver,
                    subject,
                    content,
                    bcc
                );
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private string GenerateNewClientRegisteredNotificationContent(UserAccount userAccount, Client client)
        {
            var template = "";

            using (var rdFile = new StreamReader(String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, _appSettings.Value.ClientDatabaseNotificationSettings.NewClientRegisteredEmailNotificationTemplate)))
            {
                template = rdFile.ReadToEnd();
            }

            template = template.Replace("#customername", userAccount.PersonFullName);
            template = template.Replace("#companyname", client.Name);
            template = template.Replace("#username", userAccount.Username);
            template = template.Replace("#password", userAccount.Password);

            return template;
        }

        private string GenerateNewDemoRegisteredNotificationContent(UserAccount userAccount, Client client)
        {
            var template = "";

            using (var rdFile = new StreamReader(String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, _appSettings.Value.ClientDatabaseNotificationSettings.NewDemoEmailNotificationTemplate)))
            {
                template = rdFile.ReadToEnd();
            }

            template = template.Replace("#customername", userAccount.PersonFullName);
            template = template.Replace("#companyname", client.Name);
            template = template.Replace("#username", userAccount.Username);
            template = template.Replace("#password", userAccount.Password);
            template = template.Replace("#expirationdate", DateTime.Now.AddMonths(3).ToString("MMM dd, yyyy"));

            return template;
        }

        private void CreateHashDBName(string dbName, out string dbHashName)
        {
            dbHashName = SystemUtility.EncryptionUtility.Encrypt(dbName);
        }

        private async Task<string> GenerateDemoDbName(Client client)
        {
            var demoDbName = "demo";
            var demoDBNameId = 1;
            var lastDemoDatabase = await _reserbizRepository.SystemDbContext.Clients
                                        .Where(c => c.Type == ClientTypeEnum.Demo)
                                        .OrderByDescending(c => c.Id)
                                        .FirstOrDefaultAsync();

            if (lastDemoDatabase != null)
            {
                // demoDBNameId = Convert.ToInt32(lastDemoDatabase.DBName.Substring(4));
                demoDBNameId = Convert.ToInt32(lastDemoDatabase.DBName.Split("_")[2].Substring(4));
                demoDBNameId += 1;
            }

            var finalDemoName = String.Format("{0}{1}{2}", _appSettings.Value.GeneralSettings.DatabasePrefix, demoDbName, demoDBNameId);

            return finalDemoName;
        }

        private string GenerateClientDbName(Client client)
        {
            var clientDbName = client.Name
                                    .Replace(" ", "")
                                    .Replace("-", "");


            clientDbName = clientDbName.ToLower();

            clientDbName = String.Format("{0}{1}", _appSettings.Value.GeneralSettings.DatabasePrefix, clientDbName);

            return clientDbName;
        }

        private async Task<Client> RegisterClientInformation(Client client)
        {
            string dbHashName;

            CreateHashDBName(client.DBName, out dbHashName);

            client.DBHashName = dbHashName;

            await AddEntity(client);

            return client;
        }
    }
}