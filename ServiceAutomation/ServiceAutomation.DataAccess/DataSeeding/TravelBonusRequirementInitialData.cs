using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class TravelBonusRequirementInitialData
    {
        public static TravelBonusRequirementEntity[] TravelBonusRequirementSeeds => new TravelBonusRequirementEntity[]
        {
            new TravelBonusRequirementEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Start], Turnover = 25000},
            new TravelBonusRequirementEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Classic], Turnover = 15000},
            new TravelBonusRequirementEntity{ PackageId = PackagesInitialData.PackagesIdsByType[PackageType.Premium], Turnover = 10000}
        };
    }
}
