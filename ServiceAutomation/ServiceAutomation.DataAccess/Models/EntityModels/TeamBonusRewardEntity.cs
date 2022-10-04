using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class TeamBonusRewardEntity: Entity
    {
        public Guid MonthlyLevelId { get; set; }

        public MonthlyLevelEntity MonthlyLevel { get; set; }

        public double Percent { get; set; }

        public decimal CommonTurnover { get; set; }
    }
}
