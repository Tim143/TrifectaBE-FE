using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class LevelBonusRewardPercentsInitialData
    {
        public static LevelBonusRewardPercentEntity[] LevelBonusRewardPercentSeeds = new LevelBonusRewardPercentEntity[]
        {
            new LevelBonusRewardPercentEntity {PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Start], Percent = 0 },
            new LevelBonusRewardPercentEntity {PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], Percent = 50 },
            new LevelBonusRewardPercentEntity {PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], Percent = 100 }
        };
    }
}
