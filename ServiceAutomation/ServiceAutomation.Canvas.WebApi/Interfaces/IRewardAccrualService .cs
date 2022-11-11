using ServiceAutomation.Canvas.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IRewardAccrualService
    {
        Task AccrueRewardForBasicLevelAsync(Guid userId, LevelInfoModel basicLevelInfo);

        Task AccrueRewardForSaleAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice);
    }
}
