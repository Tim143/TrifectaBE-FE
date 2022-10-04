using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class WithdrawController : ApiBaseController
    {
        private readonly IWithdrawService withdrawService;

        public WithdrawController(IWithdrawService withdrawService)
        {
            this.withdrawService = withdrawService;
        }

        [HttpGet(Constants.Requests.Withdraw.GetWithdrawHistory)]
        public async Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid userId, TransactionStatus transactionStatus = default, PeriodType period = default)
        {
            return await withdrawService.GetWithdrawHistory(userId, transactionStatus, period);
        }

        [HttpGet(Constants.Requests.Withdraw.GetAccuralHistory)]
        public async Task<IEnumerable<AccuralResponseModel>> GetAccuralHistory(Guid userId, TransactionStatus transactionStatus = default, BonusType bonus = default)
        {
            return await withdrawService.GetAccuralHistory(userId, transactionStatus, bonus);
        }

        [HttpPost(Constants.Requests.Withdraw.MakeWithdraw)]
        public async Task MakeWithdraw([FromBody] WithdrawRequestModel requestModel)
        {
            await withdrawService.MakeWithdraw(requestModel.UserId, requestModel.AccuralsId);
        }
    }
}