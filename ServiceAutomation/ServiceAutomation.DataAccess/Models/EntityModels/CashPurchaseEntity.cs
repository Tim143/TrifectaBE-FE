using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class CashPurchaseEntity : Entity
    {
        public Guid UserId { get; set; }
        public Guid PackageId { get; set; }
        public bool IsClosed { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual PackageEntity Package { get; set; }
    }
}
