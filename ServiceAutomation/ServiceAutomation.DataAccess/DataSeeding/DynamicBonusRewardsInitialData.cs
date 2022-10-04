using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class DynamicBonusRewardsInitialData
    {
        public static DynamicBonusRewardEntity[] DynamicBonusRewardSeeds => new DynamicBonusRewardEntity[]
        {
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 2, Percent = 20 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 3, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 5, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 7, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 9, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 4, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 6, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 8, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], HasRestriction = true, DurationOfDays = 120, SalesNumber = 10, Percent = 25 },

            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 2, Percent = 20 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 3, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 5, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 7, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 9, Percent = 30 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 4, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 6, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 8, Percent = 25 },
            new DynamicBonusRewardEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], HasRestriction = false,  SalesNumber = 10, Percent = 25 },
            
        };
    }
}
