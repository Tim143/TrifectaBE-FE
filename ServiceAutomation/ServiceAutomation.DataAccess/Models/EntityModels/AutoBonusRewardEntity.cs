using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class AutoBonusRewardEntity: Entity
    {
        public Guid BasicLevelId { get; set; }

        public BasicLevelEntity BasicLevel { get; set; }

        public Guid PackageId { get; set; }

        public PackageEntity Package { get; set; }

        public decimal Reward { get; set; }

        public decimal MonthlyTurnover { get; set; }
    }
}
