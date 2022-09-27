using ServiceAutomation.DataAccess.Models.Enums;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public abstract class LevelEntity: Entity
    {
        public string Name { get; set; }

        public decimal? Turnover { get; set; }

        public Level Level { get; set; }
    }
}
