using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Linq;
using System.Net.Http;
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
        private readonly IAutoBonusCalculatorService _autoBonusCalculatorService;
        private readonly ITeamBonusService _teamBonusService;
        private readonly ILevelsService _levelsService;

        public RewardAccrualService(AppDbContext dbContext,
                                            ILevelBonusCalculatorService levelBonusCalculatorService,
                                            ISaleBonusCalculationService saleBonusCalculationService,
                                            IPackagesService packagesService,
                                            ISalesService salesService,
                                            IAutoBonusCalculatorService autoBonusCalculatorService,
                                            ITeamBonusService teamBonusService,
                                            ILevelsService levelsService)
        {
            _dbContext = dbContext;
            _levelBonusCalculatorService = levelBonusCalculatorService;
            _packagesService = packagesService;
            _saleBonusCalculationService = saleBonusCalculationService;
            _salesService = salesService;
            _autoBonusCalculatorService = autoBonusCalculatorService;
            _teamBonusService = teamBonusService;
            _levelsService = levelsService;
        }

        private async Task<decimal> GetCurrency()
        {
            using HttpClient client = new HttpClient();
            var responseString = await client.GetAsync("https://developerhub.alfabank.by:8273/partner/1.0.1/public/nationalRates?currencyCode=840");
            var jsonResponse = await responseString.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RatesArr>(jsonResponse);

            return result.Rates[0].Rate;
        }

        public async Task AccrueRewardForBasicLevelAsync(Guid userId, LevelInfoModel basicLevelInfo)
        {
            var userPackage = await _packagesService.GetUserPackageByIdAsync(userId);
            if (userPackage == null)
                return;

            await AccrueLevelBonusRewardAsync(userId, basicLevelInfo, userPackage);
            await AccrueAutoBonusRewardAsync(userId, basicLevelInfo, userPackage);
        }

        private async Task AccrueLevelBonusRewardAsync(Guid userId, LevelInfoModel basicLevelInfo, UserPackageModel userPackage)
        {
            var rewardInfo = await _levelBonusCalculatorService.CalculateLevelBonusRewardAsync(basicLevelInfo.CurrentLevel.Id, userPackage.Id);

            if (rewardInfo.Reward == 0)
                return;

            var levelBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.LevelBonus)
                                                       .Select(b => b.Id)
                                                       .FirstAsync();
            var dailyCurrency = await GetCurrency();

            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = levelBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward * dailyCurrency,
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

            var startBonusIsActive = await _saleBonusCalculationService.IsStartBonusActiveAsync(userPackage, whoSoldId);

            if (startBonusIsActive)
            {
                await AccrueRewardForStartBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage);
            }

            await AccuralRewardsForTeamOrDynamicBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage, startBonusIsActive);

        }

        private async Task AccuralRewardsForTeamOrDynamicBonusAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage, bool startBonusIsActive)
        {
            var userBasicLevelInfo = await _levelsService.GetUserBasicLevelAsync(whoSoldId);
            var userMonthlyLevelInfo = await _levelsService.GetUserMonthlyLevelInfoWithouLastPurchaseAsync(whoSoldId, userBasicLevelInfo.CurrentLevel, sellingPrice);

            var teamBonusRewards = await CalculateRewardForTeamBonus(whoSoldId, userBasicLevelInfo, userMonthlyLevelInfo, sellingPrice);

            if (!startBonusIsActive)
            {
                var dinamicBonusReward = await CalculateRewardForDynamicBonusAsync(whoSoldId, sellingPrice, userPackage);

                if (teamBonusRewards.Reward > dinamicBonusReward.Reward)
                {
                    await AccrualTeamBonusRewardsAsync(teamBonusRewards, userMonthlyLevelInfo, whoSoldId, whoBoughtId);
                }
                else
                {
                    await AccrueRewardForDynamicBonusAsync(dinamicBonusReward, whoSoldId, whoBoughtId, sellingPrice, userPackage);
                }
            }

            await AccuralTeamBonusStructureAsync(teamBonusRewards, userMonthlyLevelInfo, whoSoldId, whoBoughtId, sellingPrice);
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
            var dailyCurrency = await GetCurrency();

            var accrual = new AccrualsEntity
            {
                UserId = whoSoldId,
                BonusId = startBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward * dailyCurrency,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<CalulatedRewardInfoModel> CalculateRewardForDynamicBonusAsync(Guid whoSoldId, decimal sellingPrice, UserPackageModel userPackage)
        {
            var userSalesCount = await _salesService.GerSalesCountInMonthAsync(whoSoldId);
            var rewardInfo = await _saleBonusCalculationService.CalculateDynamicBonusRewardAsync(sellingPrice, userPackage, userSalesCount);
            return rewardInfo;
        }

        private async Task AccrueRewardForDynamicBonusAsync(CalulatedRewardInfoModel rewardInfo, Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage)
        {
            if (rewardInfo.Reward == 0)
                return;

            var startBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.DynamicBonus)
                                                       .Select(b => b.Id)
                                                       .FirstAsync();
            var dailyCurrency = await GetCurrency();

            var accrual = new AccrualsEntity
            {
                UserId = whoSoldId,
                BonusId = startBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward * dailyCurrency,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }
    
        private async Task AccrueAutoBonusRewardAsync(Guid userId, LevelInfoModel basicLevelInfo, UserPackageModel userPackage)
        {
            var monthlyLevelInfo = await _levelsService.GetUserMonthlyLevelInfoAsync(userId, basicLevelInfo.CurrentLevel);

            var rewardInfo = await _autoBonusCalculatorService.CalculateAutoBonusRewardAsync(basicLevelInfo.CurrentLevel.Id, userPackage.Id, monthlyLevelInfo.CurrentTurnover);

            if (rewardInfo.Reward == 0)
                return;

            var autoBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                      .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.AutoBonus)
                                                      .Select(b => b.Id)
                                                      .FirstAsync();
            var dailyCurrency = await GetCurrency();

            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = autoBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward * dailyCurrency,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForBsicLevelId = basicLevelInfo.CurrentLevel.Id
            };

            await _dbContext.Accruals.AddAsync(accrual);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<CalulatedRewardInfoModel> CalculateRewardForTeamBonus(Guid whoSoldId, LevelInfoModel userBasicLevelInfo, LevelInfoModel userMonthlyLevelInfo, decimal sellingPrice)
        {
            var userRewardInfo = await _teamBonusService.CalculateTeamBonusRewardAsync(sellingPrice, userMonthlyLevelInfo.CurrentLevel, userBasicLevelInfo.CurrentTurnover, whoSoldId);
            return userRewardInfo;
        }

        private async Task AccrualTeamBonusRewardsAsync(CalulatedRewardInfoModel sellerRewardInfo, LevelInfoModel sellerMonthlyLevelInfo, Guid sellerId, Guid customerId)
        {
            if (sellerRewardInfo.Reward == 0)
                return;

           await AccrualTeamBonusRewardsAsync(sellerId, customerId, sellerRewardInfo);
        }

        private async Task AccuralTeamBonusStructureAsync(CalulatedRewardInfoModel sellerRewardInfo, LevelInfoModel sellerMonthlyLevelInfo, Guid sellerId, Guid customerId, decimal price)
        {
            if (sellerRewardInfo.Reward == 0)
                return;

            var childRefferalRewardInfo = sellerRewardInfo;
            var childRefferalMonthlyLevel = sellerMonthlyLevelInfo.CurrentLevel;

            while (true)
            {
                var parentRefferalGroup = await _dbContext.Users.AsNoTracking()
                                                          .Where(u => u.Id == sellerId)
                                                          .Select(u => u.Group.Parent)
                                                          .FirstOrDefaultAsync();
                if (parentRefferalGroup == null)
                    break;

                var parentRefferalId = parentRefferalGroup.OwnerUserId;

                var parentRefferalBasicLevelInfo = await _levelsService.GetUserBasicLevelAsync(parentRefferalId);
                var parentRefferalMonthlyLevelInfo = await _levelsService.GetUserMonthlyLevelInfoWithouLastPurchaseAsync(parentRefferalId, parentRefferalBasicLevelInfo.CurrentLevel, price);

                var parentRefferalMonthlyLevel = parentRefferalMonthlyLevelInfo.CurrentLevel;

                var parentRefferalRewardInfo = await _teamBonusService.CalculateTeamBonusRewardForChildRefferalAsync(childRefferalRewardInfo, childRefferalMonthlyLevel, parentRefferalMonthlyLevel, parentRefferalBasicLevelInfo.CurrentTurnover);

                if (parentRefferalRewardInfo.Reward == 0)
                    break;

                await AccrualTeamBonusRewardsAsync(parentRefferalId, customerId, parentRefferalRewardInfo);

                childRefferalRewardInfo = parentRefferalRewardInfo;
                childRefferalMonthlyLevel = parentRefferalMonthlyLevel;
            }
        }


        private async Task AccrualTeamBonusRewardsAsync(Guid userId, Guid whoBoughtId, CalulatedRewardInfoModel rewardInfo)
        {
            var autoBonusId = await _dbContext.Bonuses.AsNoTracking()
                                                      .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.TeamBonus)
                                                      .Select(b => b.Id)
                                                      .FirstAsync();

            var dailyCurrency = await GetCurrency();
            var accrual = new AccrualsEntity
            {
                UserId = userId,
                BonusId = autoBonusId,
                TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                AccuralPercent = rewardInfo.Percent,
                InitialAmount = rewardInfo.InitialReward,
                AccuralAmount = rewardInfo.Reward * dailyCurrency,
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
