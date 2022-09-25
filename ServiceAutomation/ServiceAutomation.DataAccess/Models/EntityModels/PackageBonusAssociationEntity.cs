using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class PackageBonusAssociationEntity : Entity
    {
        public PackageEntity Package { get; set; }

        public Guid PackageId { get; set; }

        public BonusEntity Bonus { get; set; }

        public Guid BonusId { get; set; }

        public int FromLevel { get; set; }

        public int? PayablePercent { get; set; }
    }
}
