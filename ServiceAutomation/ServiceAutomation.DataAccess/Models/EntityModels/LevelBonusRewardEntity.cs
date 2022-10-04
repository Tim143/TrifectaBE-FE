using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class LevelBonusRewardEntity: Entity
    {
        public Guid LevelId { get; set; }

        public BasicLevelEntity Level { get; set; }

        public decimal Reward { get; set; }
    }
}
