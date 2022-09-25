using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IWithdrawService
    {
        Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid userId);
    }
}
