using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class StartBonusRewardsInitialData
    {
        public static StartBonusRewardEntity[] StartBonusRewardSeeds = new StartBonusRewardEntity[]
        {
            new StartBonusRewardEntity {DurationOfDays = 30, CountOfSale = 1, Percent = 25, PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Start] },
            new StartBonusRewardEntity {DurationOfDays = 60, CountOfSale = 3, Percent = 25, PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic] },
            new StartBonusRewardEntity {DurationOfDays = 90, CountOfSale = 7, Percent = 25, PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium] }
        };
    }
}
