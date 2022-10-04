using ServiceAutomation.Canvas.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IAutoBonusCalculatorService
    {
        Task<CalulatedRewardInfoModel> CalculateAutoBonusRewardAsync(Guid currentBasicLevelId, Guid currentPackageId, decimal currentMonthlyTurnover);
    }
}
