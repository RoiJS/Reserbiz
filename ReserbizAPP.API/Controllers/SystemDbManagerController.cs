using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.DbContexts;

namespace ReserbizAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemDbManagerController : ReserbizBaseController
    {
        private readonly ReserbizDataContext _context;

        public SystemDbManagerController(ReserbizDataContext context)
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