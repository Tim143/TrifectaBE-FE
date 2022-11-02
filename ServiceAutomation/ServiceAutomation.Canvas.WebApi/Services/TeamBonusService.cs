using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ServiceAutomation.Canvas.WebApi.Constants.Requests;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class TeamBonusService: ITeamBonusService
    {
        private readonly AppDbContext _dbContext;
        private readonly ISaleBonusCalculationService _saleBonusCalculationService;
        private readonly IPackagesService _packagesService;

        public TeamBonusService(AppDbContext dbContext, ISaleBonusCalculationService saleBonusCalculationService, IPackagesService packagesService)
        {
            _dbContext = dbContext;
            _saleBonusCalculationService = saleBonusCalculationService;
            _packagesService = packagesService;
        }

        public async Task<CalulatedRewardInfoModel> CalculateTeamBonusRewardAsync(decimal initialReward, LevelModel monthlyLevel, decimal commonTurnover, Guid userId)
        {
            var currentUserPackage = await _packagesService.GetUserPackageByIdAsync(userId);

            var startBonusIsActive = await _saleBonusCalculationService.IsStartBonusActiveAsync(currentUserPackage, userId);

            if (startBonusIsActive)
            {
                return new CalulatedRewardInfoModel();
            }

            var appropriateBonusRewars = await _dbContext.TeamBonusRewards.AsNoTracking()
                                                                          .Where(t => t.MonthlyLevel.Level <= (Level)monthlyLevel.Level && t.CommonTurnover <= commonTurnover)
                                                                          .OrderByDescending(t => t.MonthlyLevel.Level)
                                                                          .FirstOrDefaultAsync();


            if (appropriateBonusRewars == null)
                return new CalulatedRewardInfoModel();

            var percent = appropriateBonusRewars.Percent;
            var reward = (initialReward * (decimal)percent) / 100;

            return new CalulatedRewardInfoModel
            {
                InitialReward = initialReward,
                Percent = (int)percent,
                Reward = reward
            };
        }

        public async Task<CalulatedRewardInfoModel> CalculateTeamBonusRewardAsync(decimal initialReward, LevelModel userMonthlyLevel, LevelModel partnerMonthlyLevel, decimal commonTurnover, Guid userId)
        {
            var currentUserPackage = await _packagesService.GetUserPackageByIdAsync(userId);

            var startBonusIsActive = await _saleBonusCalculationService.IsStartBonusActiveAsync(currentUserPackage, userId);

            if (startBonusIsActive)
            {
                return new CalulatedRewardInfoModel();
            }

            var appropriateBonusRewars = await _dbContext.TeamBonusRewards.AsNoTracking()
                                                              .Where(t => t.MonthlyLevel.Level <= (Level)userMonthlyLevel.Level && t.CommonTurnover <= commonTurnover)
                                                              .OrderByDescending(t => t.MonthlyLevel.Level)
                                                              .FirstOrDefaultAsync();


            if (appropriateBonusRewars == null)
                return new CalulatedRewardInfoModel();

            var partnerBonusReward = await _dbContext.TeamBonusRewards.AsNoTracking()
                                                              .Where(t => t.MonthlyLevel.Level == (Level)partnerMonthlyLevel.Level )
                                                              .FirstOrDefaultAsync();

            var percent = appropriateBonusRewars.Percent - partnerBonusReward.Percent;

            if (percent <= 0)
                return new CalulatedRewardInfoModel();

            var reward = (initialReward * (decimal)percent) / 100;

            return new CalulatedRewardInfoModel
            {
                InitialReward = initialReward,
                Percent = (int)percent,
                Reward = reward
            };
        }
    }
}
