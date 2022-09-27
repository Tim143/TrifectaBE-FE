using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class BasicLevelEntity: LevelEntity
    {
        public int? PartnersCount { get; set; } 

        public virtual BasicLevelEntity PartnersLevel { get; set; }

        public virtual Guid? PartnersLevelId { get; set; }
    }
}
