using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class RewardAccrualService : IRewardAccrualService
    {
        private readonly AppDbContext dbContext;
        private readonly ILevelBonusCalculatorService levelBonusCalculatorService;
        private readonly IPackagesService packagesService;
        private readonly ISaleBonusCalculationService saleBonusCalculationService;
        private readonly ISalesService salesService;
        private readonly IAutoBonusCalculatorService autoBonusCalculatorService;
        private readonly ITeamBonusService teamBonusService;
        private readonly ILevelsService levelsService;

        public RewardAccrualService(AppDbContext dbContext,
                                            ILevelBonusCalculatorService levelBonusCalculatorService,
                                            ISaleBonusCalculationService saleBonusCalculationService,
                                            IPackagesService packagesService,
                                            ISalesService salesService,
                                            IAutoBonusCalculatorService autoBonusCalculatorService,
                                            ITeamBonusService teamBonusService,
                                            ILevelsService levelsService)
        {
            this.dbContext = dbContext;
            this.levelBonusCalculatorService = levelBonusCalculatorService;
            this.packagesService = packagesService;
            this.saleBonusCalculationService = saleBonusCalculationService;
            this.salesService = salesService;
            this.autoBonusCalculatorService = autoBonusCalculatorService;
            this.teamBonusService = teamBonusService;
            this.levelsService = levelsService;
        }

        private async Task<decimal> GetCurrency()
        {
            using HttpClient client = new HttpClient();
            var responseString = await client.GetAsync("https://www.nbrb.by/api/exrates/rates/431");
            var jsonResponse = await responseString.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RatesArr>(jsonResponse);

            return (decimal)2.42;
            return result.Rates[0].Cur_OfficialRate;
        }

        public async Task AccrueRewardForBasicLevelAsync(Guid userId, LevelInfoModel basicLevelInfo)
        {
            var userPackage = await packagesService.GetUserPackageByIdAsync(userId);
            if (userPackage == null)
                return;

            await AccrueLevelBonusRewardAsync(userId, basicLevelInfo, userPackage);
            await AccrueAutoBonusRewardAsync(userId, basicLevelInfo, userPackage);
        }

        private async Task AccrueLevelBonusRewardAsync(Guid userId, LevelInfoModel basicLevelInfo, UserPackageModel userPackage)
        {
            var rewardInfo = await levelBonusCalculatorService.CalculateLevelBonusRewardAsync(basicLevelInfo.CurrentLevel.Id, userPackage.Id);

            if (rewardInfo.Reward == 0)
                return;

            

            var levelBonusId = await dbContext.Bonuses.AsNoTracking()
                                                       .Where(b => b.Type == DataAccess.Schemas.Enums.BonusType.LevelBonus)
                                                       .Select(b => b.Id)
            .FirstAsync();

            var userLevelAccural = await dbContext.Accruals.FirstOrDefaultAsync(x => x.AccuralPercent == rewardInfo.Percent && x.InitialAmount == rewardInfo.InitialReward && x.Bonus.Id == levelBonusId);

            if(userLevelAccural == null)
            {
                var dailyCurrency = await GetCurrency();

                var accrual = new AccrualsEntity
                {
                    UserId = userId,
                    BonusId = levelBonusId,
                    TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.Pending,
                    AccuralPercent = rewardInfo.Percent,
                    InitialAmount = rewardInfo.InitialReward,
                    AccuralAmount = rewardInfo.Reward * dailyCurrency,
                    AccuralAmountUSD = rewardInfo.Reward,
                    AccuralDate = DateTime.Now,
                    AvailableIn = DateTime.Now.AddDays(14),
                    IsAvailable = false,
                };

                await dbContext.Accruals.AddAsync(accrual);
                await dbContext.SaveChangesAsync();
            }
            
        }

        public async Task AccrueRewardForSaleAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice)
        {
            var userPackage = await packagesService.GetUserPackageByIdAsync(whoSoldId);
            if (userPackage == null)
                return;

            var startBonusIsActive = await saleBonusCalculationService.IsStartBonusActiveAsync(userPackage, whoSoldId);

            if (startBonusIsActive)
            {
                await AccrueRewardForStartBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage);
            }

            await AccuralRewardsForTeamOrDynamicBonusAsync(whoSoldId, whoBoughtId, sellingPrice, userPackage, startBonusIsActive);

        }

        private async Task AccuralRewardsForTeamOrDynamicBonusAsync(Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage, bool startBonusIsActive)
        {
            var userBasicLevelInfo = await levelsService.CalculateBasicLevelByTurnoverWithPreviousPurchaseAsync(whoSoldId, sellingPrice);
            var userMonthlyLevelInfo = await levelsService.GetUserMonthlyLevelInfoWithouLastPurchaseAsync(whoSoldId, userBasicLevelInfo.CurrentLevel, sellingPrice);

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
            var userSalesCount = await salesService.GetUserSalesCountAsync(whoSoldId);

            var rewardInfo = await saleBonusCalculationService.CalculateStartBonusRewardAsync(sellingPrice, userPackage, userSalesCount);

            if (rewardInfo.Reward == 0)
                return;

            var startBonusId = await dbContext.Bonuses.AsNoTracking()
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
                AccuralAmountUSD = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await dbContext.Accruals.AddAsync(accrual);
            await dbContext.SaveChangesAsync();
        }

        private async Task<CalulatedRewardInfoModel> CalculateRewardForDynamicBonusAsync(Guid whoSoldId, decimal sellingPrice, UserPackageModel userPackage)
        {
            var userSalesCount = await salesService.GerSalesCountInMonthAsync(whoSoldId);
            var rewardInfo = await saleBonusCalculationService.CalculateDynamicBonusRewardAsync(sellingPrice, userPackage, userSalesCount);
            return rewardInfo;
        }

        private async Task AccrueRewardForDynamicBonusAsync(CalulatedRewardInfoModel rewardInfo, Guid whoSoldId, Guid whoBoughtId, decimal sellingPrice, UserPackageModel userPackage)
        {
            if (rewardInfo.Reward == 0)
                return;

            var startBonusId = await dbContext.Bonuses.AsNoTracking()
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
                AccuralAmountUSD = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await dbContext.Accruals.AddAsync(accrual);
            await dbContext.SaveChangesAsync();
        }
    
        private async Task AccrueAutoBonusRewardAsync(Guid userId, LevelInfoModel basicLevelInfo, UserPackageModel userPackage)
        {
            var monthlyLevelInfo = await levelsService.GetUserMonthlyLevelInfoAsync(userId, basicLevelInfo.CurrentLevel);

            var rewardInfo = await autoBonusCalculatorService.CalculateAutoBonusRewardAsync(basicLevelInfo.CurrentLevel.Id, userPackage.Id, monthlyLevelInfo.CurrentTurnover);

            if (rewardInfo.Reward == 0)
                return;

            var autoBonusId = await dbContext.Bonuses.AsNoTracking()
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
                AccuralAmountUSD = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForBsicLevelId = basicLevelInfo.CurrentLevel.Id
            };

            await dbContext.Accruals.AddAsync(accrual);
            await dbContext.SaveChangesAsync();
        }

        private async Task<CalulatedRewardInfoModel> CalculateRewardForTeamBonus(Guid whoSoldId, LevelInfoModel userBasicLevelInfo, LevelInfoModel userMonthlyLevelInfo, decimal sellingPrice)
        {
            var userRewardInfo = await teamBonusService.CalculateTeamBonusRewardAsync(sellingPrice, userMonthlyLevelInfo.CurrentLevel, userBasicLevelInfo.CurrentTurnover, whoSoldId);
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
            var childRefferalRewardInfo = sellerRewardInfo;
            var childRefferalMonthlyLevel = sellerMonthlyLevelInfo.CurrentLevel;

            while (true)
            {
                var parentRefferalGroup = await dbContext.Users.AsNoTracking()
                                                          .Where(u => u.Id == sellerId)
                                                          .Select(u => u.Group.Parent)
                                                          .FirstOrDefaultAsync();
                if (parentRefferalGroup == null)
                    break;

                var parentRefferalId = parentRefferalGroup.OwnerUserId;
                
                var parentRefferalBasicLevelInfo = await levelsService.GetUserBasicLevelAsync(parentRefferalId);
                var parentRefferalMonthlyLevelInfo = await levelsService.GetUserMonthlyLevelInfoAsync(parentRefferalId, parentRefferalBasicLevelInfo.CurrentLevel);

                var parentRefferalMonthlyLevel = parentRefferalMonthlyLevelInfo.CurrentLevel;

                var parentRefferalRewardInfo = await teamBonusService.CalculateTeamBonusRewardForChildRefferalAsync(childRefferalRewardInfo, childRefferalMonthlyLevel, parentRefferalMonthlyLevel, parentRefferalBasicLevelInfo.CurrentTurnover);

                if (parentRefferalRewardInfo.Reward != 0) { await AccrualTeamBonusRewardsAsync(parentRefferalId, customerId, parentRefferalRewardInfo); }

                var parentAccuralPercent = await CalculateRewardForTeamBonus(sellerId, parentRefferalBasicLevelInfo, parentRefferalMonthlyLevelInfo, price);


                if (parentRefferalRewardInfo.Percent != null)
                {
                    childRefferalRewardInfo = parentAccuralPercent;
                }
                
                //childRefferalRewardInfo = parentRefferalRewardInfo;
                childRefferalMonthlyLevel = parentRefferalMonthlyLevel;

                sellerId = parentRefferalGroup.OwnerUserId;
            }
        }

        private async Task AccrualTeamBonusRewardsAsync(Guid userId, Guid whoBoughtId, CalulatedRewardInfoModel rewardInfo)
        {
            var autoBonusId = await dbContext.Bonuses.AsNoTracking()
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
                AccuralAmountUSD = rewardInfo.Reward,
                AccuralDate = DateTime.Now,
                AvailableIn = DateTime.Now.AddDays(14),
                IsAvailable = false,
                ForWhomId = whoBoughtId
            };

            await dbContext.Accruals.AddAsync(accrual);
            await dbContext.SaveChangesAsync();
        }
    }
}
