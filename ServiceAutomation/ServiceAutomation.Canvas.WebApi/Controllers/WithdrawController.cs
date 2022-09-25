using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawController : ApiBaseController
    {
        private readonly IWithdrawService withdrawService;

        public WithdrawController(IWithdrawService withdrawService)
        {
            this.withdrawService = withdrawService;
        }

        [Authorize]
        [HttpGet(Constants.Requests.Withdraw.GetWithdrawHistory)]
        public async Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid id)
        {
            return await withdrawService.GetWithdrawHistory(id);
        }

        [Authorize]
        [HttpPost(Constants.Requests.Withdraw.MakeWithdraw)]
        public async Task<IEnumerable<WithdrawResponseModel>> Withdraw(WithdrawRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
