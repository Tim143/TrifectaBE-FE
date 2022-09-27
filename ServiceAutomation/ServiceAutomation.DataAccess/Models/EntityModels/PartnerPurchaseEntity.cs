using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class PartnerPurchaseEntity
    {
        public Guid UserId { get; set; }

        public decimal? PurchasePrice { get; set; }
    }
}
