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
    public class WithdrawController : ApiBaseController
    {
        private readonly IWithdrawService withdrawService;

        public WithdrawController(IWithdrawService withdrawService)
        {
            this.withdrawService = withdrawService;
        }

        [Authorize]
        [HttpGet(Constants.Requests.Withdraw.GetWithdrawHistory)]
        public async Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid userId, TransactionStatus transactionStatus = default, PeriodType period = default)
        {
            return await withdrawService.GetWithdrawHistory(userId, transactionStatus, period);
        }

        [Authorize]
        [HttpGet(Constants.Requests.Withdraw.GetAccuralHistory)]
        public async Task<IEnumerable<AccuralResponseModel>> GetAccuralHistory(Guid userId)
        {
            return await withdrawService.GetAccuralHistory(userId);
        }
    }
}