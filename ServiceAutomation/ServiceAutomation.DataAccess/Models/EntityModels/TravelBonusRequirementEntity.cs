using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class TravelBonusRequirementEntity: Entity
    {
        public Guid PackageId { get; set; }

        public PackageEntity Package { get; set; }

        public decimal Turnover { get; set; }
    }
}
