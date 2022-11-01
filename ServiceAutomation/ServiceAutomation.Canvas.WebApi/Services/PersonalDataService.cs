using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class PersonalDataService : IPersonalDataService
    {
        private readonly IPackagesService packagesService;
        private readonly ILevelsService levelsService;
        private readonly ILevelStatisticService levelStatisticService;
        private readonly AppDbContext dbContext;

        public PersonalDataService(IPackagesService packagesService, ILevelsService levelsService, ILevelStatisticService levelStatisticService, AppDbContext dbContext)
        {
            this.packagesService = packagesService;
            this.levelsService = levelsService;
            this.levelStatisticService = levelStatisticService;
            this.dbContext = dbContext;
        }

        public async Task<HomePageResponseModel> GetHomeUserData(Guid userId)
        {
            var userAccurals = await dbContext.Accruals.Where(x => x.UserId == userId && x.IsAvailable == false).ToListAsync();
            var currentDate = DateTime.UtcNow;

            for (int i = 0; i < userAccurals.Count; i++)
            {
                if (userAccurals[i].AvailableIn.Date <= currentDate)
                {
                    userAccurals[i].IsAvailable = true;
                    userAccurals[i].TransactionStatus = DataAccess.Schemas.Enums.TransactionStatus.ReadyForWithdraw;
                }
            }

            await dbContext.SaveChangesAsync();

            var package = await packagesService.GetUserPackageByIdAsync(userId);
            var monthlyLevelInfo = await levelsService.GetCurrentMonthlyLevelAsync(userId);
            var basicLevelInfo = await levelsService.GetUserBasicLevelAsync(userId);
            var nextBasicLevelRequirements = await levelsService.GetNextBasicLevelRequirementsAsync((Level)basicLevelInfo.CurrentLevel.Level);
            var allTimeIncome = await dbContext.Accruals.Where(x => x.UserId == userId).ToListAsync();
            var availableForWithdraw = await dbContext.Accruals.Where(x => x.UserId == userId && x.IsAvailable == true && x.TransactionStatus == TransactionStatus.ReadyForWithdraw).ToListAsync();
            var awaitingAccural = await dbContext.Accruals.Where(x => x.UserId == userId && x.IsAvailable == false).ToListAsync();
            double receivedPayoutPercentage = 0;

            decimal awaitin = 0;
            foreach (var accural in awaitingAccural)
            {
                awaitin += accural.AccuralAmount;
            }

            switch (monthlyLevelInfo.CurrentLevel.Level)
            {
                case 2:
                    receivedPayoutPercentage = 12.5;
                    break;
                case 3:
                    receivedPayoutPercentage = 15;
                    break;
                case 4:
                    receivedPayoutPercentage = 17;
                    break;
                case 5:
                    receivedPayoutPercentage = 19;
                    break;
                case 6:
                    receivedPayoutPercentage = 20.5;
                    break;
                case 7:
                    receivedPayoutPercentage = 22;
                    break;
                case 8:
                    receivedPayoutPercentage = 23;
                    break;
                case 9:
                    receivedPayoutPercentage = 24;
                    break;
                case 10:
                    receivedPayoutPercentage = 25;
                    break;
            }

            var response = new HomePageResponseModel
            {
                Package = package,
                BaseLevelInfo = basicLevelInfo,
                MounthlyLevelInfo = monthlyLevelInfo,
                AllTimeIncome = allTimeIncome.Sum(x => x.AccuralAmount),
                AvailableForWithdrawal = availableForWithdraw.Sum(x => x.AccuralAmount),
                AwaitingAccrual = awaitin,
                ReceivedPayoutPercentage = receivedPayoutPercentage,
                ReuqiredAction = "test comment",
                NextBasicLevelRequirements = nextBasicLevelRequirements,
            };

            return response;
        }
    }
}
