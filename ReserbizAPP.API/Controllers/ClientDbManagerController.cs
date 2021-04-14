using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReserbizAPP.LIB.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientDbManagerController : ReserbizBaseController
    {
        private readonly ReserbizClientDataContext _context;

        public ClientDbManagerController(ReserbizClientDataContext context)
        {
            _context = context;
        }

        [HttpPost("syncDatabase")]
        public async Task<IActionResult> SyncDatabase()
        {
            await _context.Database.MigrateAsync();

            return Ok();
        }
    }
}