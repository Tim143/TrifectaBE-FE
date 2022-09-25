using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ApiBaseController
    {
        private readonly IReferralGroupService groupService;

        public GroupController(IReferralGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet(Constants.Requests.Group.GetTree)]
        public async Task<IActionResult> GetTree(Guid userId)
        {
            return Ok(await groupService.GetReferralTree(userId));
        }
    }
}
