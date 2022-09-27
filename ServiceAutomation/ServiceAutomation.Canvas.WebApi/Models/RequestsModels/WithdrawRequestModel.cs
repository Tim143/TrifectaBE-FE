using ServiceAutomation.DataAccess.Schemas.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class WithdrawRequestModel
    {
        public Guid UserId { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}
