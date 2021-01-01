using Microsoft.AspNetCore.Mvc;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class TestController
    {

        [HttpGet("getValues")]
        public string GetValues()
        {
            return "Roi";
        }
    }
}