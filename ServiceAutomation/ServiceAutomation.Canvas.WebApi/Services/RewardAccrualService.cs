using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class RewardAccrualService : IRewardAccrualService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILevelBonusCalculatorService _levelBonusCalculatorService;
        private readonly IPackagesService _packagesService;
        private readonly ISaleBonusCalculationService _saleBonusCalculationService;
        private readonly ISalesService _salesService;
        private readonly ILevelStatisticService _levelStatisticService;
        private readonly IAutoBonusCalculatorService _autoBonusCalculatorService;
        private readonly ITeamBonusService _teamBonusService;

        public RewardAccrualService(AppDbContext dbContext,
                                            ILevelBonusCalculatorService levelBonusCalculatorService,
                                            ISaleBonusCalculationService saleBonusCalculationService,
                                            IPackagesService packagesService,
                                            ISalesService salesService,
                                            ILevelStatisticService levelStatisticService,
                                            IAutoBonusCalculatorService autoBonusCalculatorService,
                                            ITeamBonusService teamBonusService)
        {
            _dbContext = dbContext;
            _levelBonusCalculatorService = levelBonusCalculatorService;
            _packagesService = packagesService;
            _saleBonusCalculationService = saleBonusCalculationService;
            _salesService = salesService;
            _levelStatisticService = levelStatisticService;
            _autoBonusCalculatorService = autoBonusCalculatorService;
            _teamBonusService = teamBonusService;
        }

        public async Task AccrueRewardForBasicLevelAsync(Guid userId)
        {
            var userPackage = await _packagesService.GetUserPackageByIdAsync(userId);
            if (userPackage == null)
                return;

            var userBasicLevelId = await _dbContext.Users.Where(u => u.Id == userId).Select(u => u.BasicLevelId).FirstOrDefaultAsync();

            if (userBasicLevelId == null)
                return;

            await AccrueLevelBonusRewardAsync(userId, userBasicLevelId.Value, userPackage);
            await AccrueAutoBonusRewardAsync(userId, userBasicLevelId.Value, userPackage);
        }

        private async Task AccrueLevelBonusRewardAsync(Guid userId, Guid userBasicLevelId, UserPackageModel userPackage)
        {
            var rewardInfo = await _levelBonusCalculatorService.CalculateLevelBonusRewardAsync(userBasicLevelId, userPackage.Id);

            if (rewardInfo.Reward == 0)
                return;

            var basicLevel = await _dbContext.Users.AsNoTracking().Where(u => u.Id == userId).Select(u => u.BasicLevelId).FirstAsync();
            var levelBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.LevelBonus)
                                                       .Select(b => b.Id)
                                                       .FirstAsync();
            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = levelBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AccrueRewardForSaleAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice)
        {
            var userPackage = await _packagesService.GetUserPackageByIdAsync(whoSoldId);
            if (userPackage == null)
                return;

            await AccrueRewardForStartBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage);
            await AccrueRewardForDynamicBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage);
            await AccrualTeamBonusRewardsAsync(whoSoldId, whoBoughtId, sellingPrice);
        }

        private async Task AccrueRewardForStartBonusAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage)
        {
            var userSalesCount = await _salesService.GetUserSalesCountAsync(whoSoldId);

            var rewardInfo = await _saleBonusCalculationService.CalculateStartBonusRewardAsync(sellingPrice, userPackage, userSalesCount);

            if (rewardInfo.Reward == 0)
                return;

            var startBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.StartBonus)
                                                       .Select(b => b.Id)
                                                       .FirstAsync();
            var accrual = new AccrualsEntity
            {
                UserId = whoSoldId,
                BonusId = startBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }

        private async Task AccrueRewardForDynamicBonusAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage)
        {
            var userSalesCount = await _salesService.GerSalesCountInMonthAsync(whoSoldId);
            var rewardInfo = await _saleBonusCalculationService.CalculateDynamicBonusRewardAsync(sellingPrice, userPackage, userSalesCount);

            if (rewardInfo.Reward == 0)
                return;

            var startBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.DynamicBonus)
                                                       .Select(b => b.Id)
                                                       .FirstAsync();
            var accrual = new AccrualsEntity
            {
                UserId = whoSoldId,
                BonusId = startBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }
    
        private async Task AccrueAutoBonusRewardAsync(Guid userId, Guid userBasicLevelId, UserPackageModel userPackage)
        {
            var monthlyLevelInfo = await _levelStatisticService.GetMonthlyLevelInfoByUserIdAsync(userId);

            var rewardInfo = await _autoBonusCalculatorService.CalculateAutoBonusRewardAsync(userBasicLevelId, userPackage.Id, monthlyLevelInfo.CurrentTurnover);

            if (rewardInfo.Reward == 0)
                return;

            var autoBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                      .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.AutoBonus)
                                                      .Select(b => b.Id)
                                                      .FirstAsync();
            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = autoBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForBsicLevelId = userBasicLevelId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }

        private async Task AccrualTeamBonusRewardsAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice)
        {
            var userMonthlyLevel = await _levelStatisticService.GetMonthlyLevelByUserIdAsync(whoSoldId);
            var userBasicLevelInfo = await _levelStatisticService.GetBasicLevelInfoByUserIdAsync(whoSoldId);

            var userRewardInfo = await _teamBonusService.CalculateTeamBonusRewardAsync(sellingPrice, userMonthlyLevel, userBasicLevelInfo.CurrentTurnover, whoSoldId);

            if (userRewardInfo.Reward == 0)
                return;

            await AccrualTeamBonusRewardsAsync(whoSoldId, whoBoughtId, userRewardInfo);

            decimal tempReward = userRewardInfo.Reward;
            LevelModel tempMonthlyLevel = userMonthlyLevel;

            while (true)
            {
                var parentGroup = await _dbContext.Users.AsNoTracking()
                                                       .Where(u => u.Id == whoSoldId)
                                                       .Select(u => u.Group.Parent)
                                                       .FirstOrDefaultAsync();
                if (parentGroup == null)
                    break;

                var parentUser = parentGroup.OwnerUserId;
                var monthlyLevel = await _levelStatisticService.GetMonthlyLevelByUserIdAsync(parentUser);
                var basicLevelInfo = await _levelStatisticService.GetBasicLevelInfoByUserIdAsync(parentUser);

                var rewardInfo = await _teamBonusService.CalculateTeamBonusRewardAsync(tempReward, monthlyLevel, tempMonthlyLevel, basicLevelInfo.CurrentTurnover, parentUser);

                if (rewardInfo.Reward == 0)
                    break;

                await AccrualTeamBonusRewardsAsync(parentUser, whoBoughtId, rewardInfo);

                tempReward = rewardInfo.Reward;
                tempMonthlyLevel = monthlyLevel;
            }
        }


        private async Task AccrualTeamBonusRewardsAsync(Guid userId, Guid whoBoughtId, CalulatedRewardInfoModel rewardInfo)
        {
            var autoBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                      .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.TeamBonus)
                                                      .Select(b => b.Id)
                                                      .FirstAsync();
            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = autoBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }
    }
}
