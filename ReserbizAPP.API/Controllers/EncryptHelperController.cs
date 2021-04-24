using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.Helpers;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptHelperController : ControllerBase
    {
        public EncryptHelperController()
        {
            
        }

        [HttpGet("encrypt/{value}")]
        public string Encrypt(string value)
        {
            return SystemUtility.EncryptionUtility.Encrypt(value);
        }
    }
}