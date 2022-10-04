using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class LevelBonusRewardPercentEntity: Entity
    {
        public Guid PackageId { get; set; }

        public PackageEntity Package { get; set; }

        public int Percent { get; set; }
    }
}
