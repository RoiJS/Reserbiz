using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ApplicationSettings> _appSettings;
        private readonly IOptions<EmailServerSettings> _emailServerSettings;

        public ClientRepository(
                IReserbizRepository<Client> reserbizRepository,
                IOptions<AppSettingsURL> appSettingsUrl,
                IOptions<ApplicationSettings> appSettings,
                IOptions<EmailServerSettings> emailServerSettings)
            : base(reserbizRepository, reserbizRepository.SystemDbContext)
        {
            _appSettingsUrl = appSettingsUrl;
            _appSettings = appSettings;
            _emailServerSettings = emailServerSettings;
        }

        public async Task<Client> RegisterClient(Client client)
        {
            string dbHashName;

            CreateHashDBName(client.DBName, out dbHashName);

            client.DBHashName = dbHashName;

            await AddEntity(client);

            return client;
        }

        public async Task<Client> RegisterDemo(Client client)
        {
            client.DBName = await GenerateDemoDbName(client);

            return await RegisterClient(client);
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
                var httpClient = new RestClient(_appSettingsUrl.Value.SyncDatabaseURL);
                httpClient.Timeout = -1;
                var httpRequest = new RestRequest(Method.POST);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                IRestResponse response = await httpClient.ExecuteAsync(httpRequest);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void SendEmailNotification(Client client)
        {
            var emailContent = GenerateNotificationContent(client, _appSettings.Value.ClientDatabaseNotificationSettings.EmailNotificationTemplate);
            var subject = "New Client Database";

            try
            {
                var emailService = new EmailService(
                                _emailServerSettings.Value.SmtpServer,
                                _emailServerSettings.Value.SmtpAddress,
                                _emailServerSettings.Value.SmtpPassword
                            );

                emailService.Send(
                    _appSettings.Value.ClientDatabaseNotificationSettings.SenderEmailAddress,
                    _appSettings.Value.ClientDatabaseNotificationSettings.SenderEmailAddress,
                    subject,
                    emailContent,
                    _appSettings.Value.ClientDatabaseNotificationSettings.SenderEmailAddress
                );
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private string GenerateNotificationContent(Client client, string templatePath)
        {
            var template = "";

            using (var rdFile = new StreamReader(String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, templatePath)))
            {
                template = rdFile.ReadToEnd();
            }

            template = template.Replace("#customername", client.Name);
            template = template.Replace("#databasename", client.DBName);
            template = template.Replace("#clienttype", Enum.GetName(typeof(ClientTypeEnum), client.Type));

            return template;
        }

        private void CreateHashDBName(string dbName, out string dbHashName)
        {
            dbHashName = SystemUtility.EncryptionUtility.Encrypt(dbName);
        }

        private async Task<string> GenerateDemoDbName(Client client)
        {
            var demoDbName = "demo{0}";
            var demoDBNameId = 1;
            var lastDemoDatabase = await _reserbizRepository.SystemDbContext.Clients
                                        .Where(c => c.Type == ClientTypeEnum.Demo)
                                        .OrderByDescending(c => c.DBName)
                                        .FirstOrDefaultAsync();

            if (lastDemoDatabase != null)
            {
                demoDBNameId = Convert.ToInt32(lastDemoDatabase.DBName.Substring(4));
                demoDBNameId += 1;
            }

            return String.Format(demoDbName, demoDBNameId);
        }
    }
}