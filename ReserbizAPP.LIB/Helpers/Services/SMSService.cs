using System;
using System.Threading.Tasks;
using Vonage.Messaging;
using Vonage.Request;

namespace ReserbizAPP.LIB.Helpers.Services
{
    public class SMSService
    {
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public SMSService(string apiKey, string apiSecret)
        {
            _apiSecret = apiSecret;
            _apiKey = apiKey;

        }

        public async Task SendMessage(string sender, string receiver, string message)
        {
            try
            {
                var credentials = Credentials.FromApiKeyAndSecret(_apiKey, _apiSecret);
                var client = new SmsClient(credentials);
                var request = new SendSmsRequest { To = receiver, From = sender, Text = message };
                var response = await client.SendAnSmsAsync(request);
            }
            catch (VonageSmsResponseException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}