using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class AutoBonusRewardInitialData
    {
        public static AutoBonusRewardEntity[] AutoBonusRewardSeeds => new AutoBonusRewardEntity[]
        {
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.SeventhLevel], Reward = 500, MonthlyTurnover = 81000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.EighthLevel], Reward = 700, MonthlyTurnover = 165000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.NinthLevel], Reward = 1000, MonthlyTurnover = 330000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.TenthLevel], Reward = 1500, MonthlyTurnover = 670000},

            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.FifthLevel], Reward = 500, MonthlyTurnover = 20000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.SixthLevel], Reward = 700, MonthlyTurnover = 41000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.SeventhLevel], Reward = 1000, MonthlyTurnover = 81000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.EighthLevel], Reward = 2000, MonthlyTurnover = 165000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.NinthLevel], Reward = 3000, MonthlyTurnover = 330000},
            new AutoBonusRewardEntity { PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], BasicLevelId = BasicLevelsInitialData.BasicLevelIdsByType[Level.TenthLevel], Reward = 4000, MonthlyTurnover = 670000},
        };
    }
}
