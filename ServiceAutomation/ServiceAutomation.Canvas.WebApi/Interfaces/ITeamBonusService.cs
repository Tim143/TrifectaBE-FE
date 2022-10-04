
using ServiceAutomation.Canvas.WebApi.Models;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ITeamBonusService
    {
        Task<CalulatedRewardInfoModel> CalculateTeamBonusRewardAsync(decimal initialReward, LevelModel monthlyLevel, decimal commonTurnover);

        Task<CalulatedRewardInfoModel> CalculateTeamBonusRewardAsync(decimal initialReward, LevelModel userMonthlyLevel, LevelModel partnerMonthlyLevel, decimal commonTurnover);
    }
}
