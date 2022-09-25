using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class WithdrawRequestModel
    {
        public Guid UserId { get; set; }
        public decimal Rate { get; set; }
    }
}
