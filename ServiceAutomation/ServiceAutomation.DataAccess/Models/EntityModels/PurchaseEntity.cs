using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class PurchaseEntity: Entity
    {
        public virtual UserEntity User { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual PackageEntity Package { get; set; }

        public virtual Guid PackageId { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
