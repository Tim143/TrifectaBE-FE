using ServiceAutomation.DataAccess.Schemas.Enums;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class BonusEntity: Entity
    {
        public BonusType Type { get; set; }

        public string Name { get; set; }

        public IList<PackageBonusAssociationEntity> PackageBonuses { get; set; }
    }
}
