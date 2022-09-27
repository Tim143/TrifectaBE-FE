using ServiceAutomation.DataAccess.Schemas.Enums;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class PackageEntity: Entity
    {
        public PackageType Type { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int DisplayOrder { get; set; }

        public virtual IList<PackageBonusAssociationEntity> PackageBonuses { get; set; }

        public virtual IList<PurchaseEntity> UsersPurchases { get; set; }
    }
}
