using ServiceAutomation.Canvas.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelStatisticService
    {
        Task<LevelModel> GetMonthlyLevelByUserIdAsync(Guid userId);

        Task<LevelInfoModel> GetMonthlyLevelInfoByUserIdAsync(Guid userId);

        Task<LevelInfoModel> GetBasicLevelInfoByUserIdAsync(Guid userId);

        Task AddMonthlyLevelInfoAsync(Guid userId, Guid newLevelId, decimal currentMonthlyTurnover);

        Task UpdateMonthlyLevelInfoAsync(Guid userId, Guid levelId, decimal newMonthTurnover);

        Task AddBasicLevelInfoAsync(Guid userId, Guid newLevelId, decimal currentTurnover);

        Task UpdateBasicLevelInfoAsync(Guid userId, Guid ForLevelId, decimal newTurnover);

        Task AddLevelsInfoForNewUserAsync(Guid userId);
    }
}
