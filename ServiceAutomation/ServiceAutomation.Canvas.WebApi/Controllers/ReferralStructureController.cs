using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class ReferralStructureController : ApiBaseController
    {
        private readonly ITenantGroupService groupService;

        public ReferralStructureController(ITenantGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet(Constants.Requests.Structure.GetTree)]
        public async Task<IActionResult> GetTree(Guid userId)
        {
            return Ok(await groupService.GetReferralTree(userId));
        }

        [HttpGet(Constants.Requests.Structure.GetUserReferralGroup)]
        public async Task<IActionResult> GetUserRefferalGroupAsync()
        {
            var userId = GetCurrentUserId();
            return Ok(await groupService.GetReferralGroupByUserIdAsync(userId));
        }

        [HttpGet(Constants.Requests.Structure.GetReferralGroup)]
        public async Task<IActionResult> GetRefferalGroupAsync(Guid groupId)
        {
            return Ok(await groupService.GetReferralGroupAsync(groupId));
        }

        [HttpGet(Constants.Requests.Structure.GetPartnersReferralGroups)]
        public async Task<IActionResult> GetPartnersRefferalGroupaAsync(Guid groupId)
        {
            return Ok(await groupService.GetPartnersReferralGroupsAsync(groupId));
        }

    }
}
