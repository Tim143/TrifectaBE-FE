using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
//using System.Transactions;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class WithdrawTransactionEntity : Entity
    {
        public Guid CredentialId { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public virtual CredentialEntity Credential { get; set; }
    }
}
