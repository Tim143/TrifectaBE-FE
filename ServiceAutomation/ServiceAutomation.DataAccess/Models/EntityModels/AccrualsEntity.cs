using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class AccrualsEntity : Entity
    {
        public Guid UserId { get; set; }
        public string AccuralName { get; set; }
        public string ReferralName { get; set; }
        public int AccuralPercent { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal AccuralAmount { get; set; }
        public DateTime AccuralDate { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
