using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IWithdrawService
    {
        Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid userId, TransactionStatus transactionStatus = default, PeriodType period = default);
        Task<IEnumerable<AccuralResponseModel>> GetAccuralHistory(Guid userId, TransactionStatus transactionStatus = default, BonusType bonus = default);
        Task MakeWithdraw(Guid userId, ICollection<Guid> accuralsId);
    }
}
