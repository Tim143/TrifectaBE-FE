using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserContactVerificationEntity : Entity
    {
        public Guid UserId { get; set; }
        public ContactVerificationType VerificationType { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public bool IsVerified { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
