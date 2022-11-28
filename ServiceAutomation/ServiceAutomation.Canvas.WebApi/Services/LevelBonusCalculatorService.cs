using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelBonusCalculatorService : ILevelBonusCalculatorService
    {
        private readonly AppDbContext dbContext;

        public LevelBonusCalculatorService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CalulatedRewardInfoModel> CalculateLevelBonusRewardAsync(Guid currentBasicLevel, Guid currentPackage)
        {
            var basicLevelReward = await dbContext.LevelBonusRewards
                                                      .AsNoTracking()
                                                      .Where(r => r.LevelId == currentBasicLevel)
                                                      .FirstOrDefaultAsync();
            if (basicLevelReward == null)
                return new CalulatedRewardInfoModel();

            var rewardPercent = await dbContext.LevelBonusRewardPercents
                                                .AsNoTracking()
                                                .Where(p => p.PackageId == currentPackage)
                                                .Select(p => p.Percent)
                                                .SingleAsync();

            var rewardForBasicLevel = basicLevelReward.Reward;
            var reward = (rewardForBasicLevel * rewardPercent) / 100;

            return new CalulatedRewardInfoModel
            {
                InitialReward = rewardForBasicLevel,
                Percent = rewardPercent,
                Reward = reward
            };
        }
    }
}
