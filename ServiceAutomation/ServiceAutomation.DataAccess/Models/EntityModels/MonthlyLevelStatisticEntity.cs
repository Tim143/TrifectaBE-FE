
using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class MonthlyLevelStatisticEntity: LevelStatisticEntity
    {
        public Guid LevelId { get; set; }

        public MonthlyLevelEntity Level { get; set; }
    }
}
