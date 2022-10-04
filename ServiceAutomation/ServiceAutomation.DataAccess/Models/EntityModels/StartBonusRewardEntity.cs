using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class StartBonusRewardEntity: Entity
    {
        public int DurationOfDays { get; set; }

        public Guid PackageId { get; set; }

        public PackageEntity Package { get; set; }

        public int CountOfSale { get; set; }

        public int Percent { get; set; }

    }
}
