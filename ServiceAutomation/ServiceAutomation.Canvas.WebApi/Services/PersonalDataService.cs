using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class PersonalDataService : IPersonalDataService
    {
        private readonly IPackagesService packagesService;
        private readonly ILevelsService levelsService;

        public PersonalDataService(IPackagesService packagesService, ILevelsService levelsService)
        {
            this.packagesService = packagesService;
            this.levelsService = levelsService;
        }

        public async Task<HomePageResponseModel> GetHomeUserData(Guid userId)
        {
            var package = await packagesService.GetUserPackageAsync(userId);
            var monthlyLevelInfo = await levelsService.GetMonthlyLevelInfoByUserIdAsync(userId);
            var basicLevelInfo = await levelsService.GetBasicLevelInfoByUserIdAsync(userId);
            var nextBasicLevelRequirements = await levelsService.GetNextBasicLevelRequirementsAsync((Level)basicLevelInfo.CurrentLevel.Level);

            var response = new HomePageResponseModel
            {
                Package = package,
                BaseLevelInfo = basicLevelInfo,
                MounthlyLevelInfo = monthlyLevelInfo,
                AllTimeIncome = 0,
                AvailableForWithdrawal = 0,
                AwaitingAccrual = 0,
                ReceivedPayoutPercentage = 0,
                ReuqiredAction = "test comment",
                NextBasicLevelRequirements = nextBasicLevelRequirements,
            };

            return response;
        }
    }
}
