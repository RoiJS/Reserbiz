using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReserbizAPP.Hangfire.Interfaces;
using ReserbizAPP.LIB.BusinessLogic;
using ReserbizAPP.LIB.DbContexts;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Helpers.Services;
using ReserbizAPP.LIB.Models;
using RestSharp;

namespace ReserbizAPP.Hangfire.Helpers.Services
{
    public class ReserbizRecurringJobsService
        : IReserbizRecurringJobsService

    {
        private GlobalErrorLogRepository _globalErrorLogRepository;
        private IOptions<ReserbizHangFireApplicationSettings> _appSettings;
        private IOptions<EmailServerSettings> _emailServerSettings;

        IServiceProvider _serviceProvider;
        public ReserbizRecurringJobsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // Destructor
        ~ReserbizRecurringJobsService()
        {
            _appSettings = null;
            _emailServerSettings = null;
        }

        public void RegisterAutoGenerateAccountStatements()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _appSettings = scope.ServiceProvider.GetService<IOptions<ReserbizHangFireApplicationSettings>>();
                _emailServerSettings = scope.ServiceProvider.GetService<IOptions<EmailServerSettings>>();
                _globalErrorLogRepository = scope.ServiceProvider.GetService<GlobalErrorLogRepository>();

                using (ReserbizDataContext dbContext = scope.ServiceProvider.GetRequiredService<ReserbizDataContext>())
                {
                    var activeClients = dbContext.Clients
                                .Where(c => c.IsActive)
                                .ToList();

                    foreach (var client in activeClients)
                    {
                        // Auto-generate account statements
                        SendRequestToGenerateAccountStatement(client);

                        // Auto-generate penalties
                        SendRequestToGeneratePenalties(client);
                    }
                }
            }
        }

        private void SendRequestToGenerateAccountStatement(Client client)
        {
            // Send request to auto-generate account statements
            SendRequest(client, _appSettings.Value.AutoGenerateAccountStatementsURL, (Client c) =>
            {
                // Send email notification that the generating of account statements is successfull
                SendEmailNotificationAfterAccountStatementsGenerated(c);
            });

        }

        private void SendRequestToGeneratePenalties(Client client)
        {
            // Send request to auto-generate penalties
            SendRequest(client, _appSettings.Value.AutoGeneratePenaltiesURL, (Client c) =>
            {
                // Send email notification that the generating of account statements is successfull
                SendEmailNotificationAfterPenaltiesGenerated(c);
            });
        }

        private void SendRequest(Client client, string url, Action<Client> sendEmailNotification)
        {
            try
            {
                var httpClient = new RestClient(url);
                httpClient.Options.MaxTimeout = -1;
                var httpRequest = new RestRequest(url, Method.Post);
                httpRequest.AddHeader("App-Secret-Token", client.DBHashName);
                httpRequest.AddHeader("Content-Type", "application/json");
                RestResponse response = httpClient.Execute(httpRequest);

                // Only send email if the response is Ok (200)
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return;

                sendEmailNotification(client);
            }
            catch (Exception exception)
            {
                Task.Run(async () =>
                {
                    await _globalErrorLogRepository.RegisterGlobalError(
                                     exception.Source,
                                     exception.Message,
                                     exception.StackTrace,
                                     client.Id
                                 );

                    throw new Exception(exception.Message);
                });
            }
        }

        private void SendEmailNotificationAfterAccountStatementsGenerated(Client client)
        {
            SendEmailNotification(client.Id, "Account Statements Auto-generation Success", GenerateEmailTemplate(_appSettings.Value.SchedulerEmailNotificationSettings.AutoGenerateAccountStatementEmailTemplate, client.DBName));
        }

        private void SendEmailNotificationAfterPenaltiesGenerated(Client client)
        {
            SendEmailNotification(client.Id, "Penalties Auto-generation Success", GenerateEmailTemplate(_appSettings.Value.SchedulerEmailNotificationSettings.AutoGeneratePenaltiesEmailTemplate, client.DBName));
        }

        private string GenerateEmailTemplate(string emailTemplatePath, string dbName)
        {
            var mailTemplate = "";
            using (var rdFile = new StreamReader(String.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, emailTemplatePath)))
            {
                mailTemplate = rdFile.ReadToEnd();
            }

            mailTemplate = mailTemplate.Replace("#customerName", dbName);
            mailTemplate = mailTemplate.Replace("#datetime", DateTime.Now.ToString("hh:mm dd MMM yyyy"));

            return mailTemplate;
        }

        private void SendEmailNotification(int clientId, string subject, string body)
        {
            try
            {
                var emailService = new EmailService(
                                _emailServerSettings.Value.SmtpServer,
                                _emailServerSettings.Value.SmtpAddress,
                                _emailServerSettings.Value.SmtpPassword,
                                _emailServerSettings.Value.SmtpPort
                            );

                emailService.Send(
                    _appSettings.Value.SchedulerEmailNotificationSettings.SenderEmailAddress,
                    _appSettings.Value.SchedulerEmailNotificationSettings.ReceiverEmailAddress,
                    subject,
                    body
                );

            }
            catch (Exception exception)
            {
                Task.Run(async () =>
                {
                    await _globalErrorLogRepository.RegisterGlobalError(
                                    exception.Source,
                                    exception.Message,
                                    exception.StackTrace,
                                    clientId
                                );

                    throw new Exception(exception.Message);
                });
            }
        }
    }
}