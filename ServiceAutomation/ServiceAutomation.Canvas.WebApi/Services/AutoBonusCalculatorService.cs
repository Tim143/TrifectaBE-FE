using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class AutoBonusCalculatorService : IAutoBonusCalculatorService
    {
        private readonly AppDbContext dbContext;

        public AutoBonusCalculatorService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CalulatedRewardInfoModel> CalculateAutoBonusRewardAsync(Guid currentBasicLevelId, Guid currentPackageId, decimal currentMonthlyTurnover)
        {
            var autoBonusReward = await dbContext.AutoBonusRewards
                                                  .AsNoTracking()
                                                  .Where(r => r.BasicLevelId == currentBasicLevelId && r.PackageId == currentPackageId)
                                                  .FirstOrDefaultAsync();

            if (autoBonusReward == null || autoBonusReward.MonthlyTurnover < currentMonthlyTurnover)
                return new CalulatedRewardInfoModel();

            var reward = autoBonusReward.Reward;

            return new CalulatedRewardInfoModel
            {
                InitialReward = reward,
                Reward = reward
            };
        }
    }
}
