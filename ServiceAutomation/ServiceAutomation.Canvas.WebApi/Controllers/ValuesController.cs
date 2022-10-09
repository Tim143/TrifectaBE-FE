using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.DataAccess.DbContexts;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        public ValuesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
