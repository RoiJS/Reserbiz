using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReserbizAPP.LIB.Helpers.Class;
using ReserbizAPP.LIB.Helpers.Services;

namespace ReserbizAPP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMSController : ReserbizBaseController
    {
        private readonly IOptions<SMSAPISettings> _smsApiSettings;

        public SMSController(IOptions<SMSAPISettings> smsApiSettings)
        {
            _smsApiSettings = smsApiSettings;
        }

        [HttpPost("sendMessage")]
        public async Task SendMessage(MessageDto messageDto)
        {
            var smsService = new SMSService(_smsApiSettings.Value.API_KEY, _smsApiSettings.Value.API_SECRET);
            await smsService.SendMessage(messageDto.senderDisplayName, messageDto.receiverContactNumber, messageDto.message);
        }
    }

    public class MessageDto
    {
        public string senderDisplayName { get; set; }
        public string receiverContactNumber { get; set; }
        public string message { get; set; }
    }
}