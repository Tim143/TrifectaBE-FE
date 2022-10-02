using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class BasicLevelStatisticEntity: LevelStatisticEntity
    {
        public Guid LevelId { get; set; }

        public BasicLevelEntity Level { get; set; }
    }
}
