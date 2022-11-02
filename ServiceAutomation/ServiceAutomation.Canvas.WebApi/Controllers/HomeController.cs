using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceAutomation.Canvas.WebApi.Constants;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Twilio.Base;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class HomeController : ApiBaseController
    {
        private readonly IUserReferralService userReferralService;
        private readonly IPersonalDataService personalDataService;
        private readonly ILevelBonusCalculatorService bonusCalculatorService;

        public HomeController(IUserReferralService userReferralService, IPersonalDataService personalDataService, ILevelBonusCalculatorService bonusCalculatorService)
        {
            this.userReferralService = userReferralService;
            this.personalDataService = personalDataService;
            this.bonusCalculatorService = bonusCalculatorService;
        }

        [HttpGet(Constants.Requests.Home.GetReferralLink)]
        public async Task<string> GetUserReferral(Guid userId)
        {
            return await userReferralService.GetUserRefferal(userId);
        }

        [HttpGet(Constants.Requests.Home.GetPersonalPageInfo)]
        public async Task<IActionResult> GetPersonalPageInfo(Guid userId)
        {
            return Ok(await personalDataService.GetHomeUserData(userId));
        }
    }
}
