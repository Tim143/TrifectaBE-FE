using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelsService
    {
        Task<LevelModel> GetMonthlyLevelByUserIdAsync(Guid userId);

        Task<LevelInfoModel> GetMonthlyLevelInfoByUserIdAsync(Guid userId);

        Task<LevelInfoModel> GetBasicLevelInfoByUserIdAsync(Guid userId);

        Task<NextBasicLevelRequirementsModel> GetNextBasicLevelRequirementsAsync(Level currentUserBasicLevel);

        Task СalculatePartnersBasicLevelsAsync(Guid userId);
    }
}
