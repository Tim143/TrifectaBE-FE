using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class ProgressController : ControllerBase
    {
        private readonly IUserProgressService progressService;

        public ProgressController(IUserProgressService progressService)
        {
            this.progressService = progressService;
        }

        [HttpGet(Constants.Requests.Progress.GetUserProgress)]
        public async Task<IActionResult> GetUserProgress(Guid userId)
        {
            return Ok(await progressService.GetUserProgress(userId));
        }
    }
}
