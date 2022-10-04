using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserAccuralsVerificationEntity : Entity
    {
        public Guid UserId { get; set; }
        public ICollection<AccrualsEntity> Accurals { get; set; }
        public bool IsVerified { get; set; }
        public virtual UserEntity User { get; set; }
       
    }
}
