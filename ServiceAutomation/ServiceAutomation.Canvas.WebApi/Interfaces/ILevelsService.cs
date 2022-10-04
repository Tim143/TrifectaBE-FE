using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.Models.Enums;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelsService
    {

        Task<NextBasicLevelRequirementsModel> GetNextBasicLevelRequirementsAsync(Level currentUserBasicLevel);

        Task<LevelModel> GetNextMonthlyLevelAsync(int level);

        Task<LevelModel> GetCurrentMonthlyLevelByTurnoverAsync(decimal monthlyTurnover);

    }
}
