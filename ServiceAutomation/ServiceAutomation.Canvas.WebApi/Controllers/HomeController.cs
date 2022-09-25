using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Constants;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ApiBaseController
    {
        private readonly IUserReferralService userReferralService;

        public HomeController(IUserReferralService userReferralService)
        {
            this.userReferralService = userReferralService;
        }

        [Authorize]
        [HttpGet(Constants.Requests.Home.GetReferralLink)]
        public async Task<string> GetUserReferral(Guid userId)
        {
            return await userReferralService.GetUserRefferal(userId);
        }

        [AllowAnonymous]
        [HttpGet(Constants.Requests.Home.GetAction)]
        public async Task<string> GetAction()
        {
            return "Chickha";
        }
    }
}
