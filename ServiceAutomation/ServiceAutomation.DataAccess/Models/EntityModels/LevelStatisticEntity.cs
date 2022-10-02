using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public abstract class LevelStatisticEntity: Entity
    {
        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        public DateTime ReachingLevelDate { get; set; }

        public decimal? Turnover { get; set; }
    }
}
