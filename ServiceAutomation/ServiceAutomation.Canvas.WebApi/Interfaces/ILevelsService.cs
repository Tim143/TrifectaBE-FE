using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelsService
    {
        Task<NextBasicLevelRequirementsModel> GetNextBasicLevelRequirementsAsync(Level currentUserBasicLevel);

        Task<LevelModel> GetNextMonthlyLevelAsync(int level);

        Task<LevelInfoModel> GetUserMonthlyLevelInfoAsync(Guid userId, LevelModel basicLevelModel);

        Task<LevelInfoModel> GetUserBasicLevelAsync(Guid userId);

        Task<LevelInfoModel> CalculateBasicLevelByTurnoverAsync(Guid userId, decimal turnover);
    }
}
