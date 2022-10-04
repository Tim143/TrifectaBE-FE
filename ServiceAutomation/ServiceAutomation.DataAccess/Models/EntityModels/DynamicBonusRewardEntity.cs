using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class DynamicBonusRewardEntity: Entity
    {
        public int SalesNumber { get; set; }

        public int Percent { get; set; }

        public int? DurationOfDays { get; set; }

        public bool HasRestriction { get; set; }

        public Guid PackageId { get; set; }

        public PackageEntity Package { get; set; }

    }
}
