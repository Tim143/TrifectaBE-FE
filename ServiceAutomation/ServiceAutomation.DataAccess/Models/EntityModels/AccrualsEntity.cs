using ServiceAutomation.DataAccess.Schemas.Enums;
using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class AccrualsEntity : Entity
    {
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public Guid BonusId { get; set; }
        public BonusEntity Bonus { get; set; }

        public Guid? ForWhomId { get; set; }

        public virtual UserEntity ForWhom { get; set; }

        public double? AccuralPercent { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public decimal InitialAmount { get; set; }
        public decimal AccuralAmount { get; set; }

        public DateTime AccuralDate { get; set; }

        public Guid? ForBsicLevelId { get; set; }
        public DateTime AvailableIn { get; set; }
        public bool IsAvailable { get; set; }

        public BasicLevelEntity ForBsicLevel { get; set; }
    }
}
