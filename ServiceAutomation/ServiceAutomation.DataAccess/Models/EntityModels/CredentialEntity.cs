using System;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class CredentialEntity : Entity
    {
        public string IBAN { get; set; }

        public Guid UserId { get; set; }

        public virtual UserEntity User { get; set; }

        public virtual ICollection<WithdrawTransactionEntity> WithdrawTransactions { get; set; }
    }
}
