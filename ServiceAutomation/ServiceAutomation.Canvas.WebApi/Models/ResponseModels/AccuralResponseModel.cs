using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class AccuralResponseModel
    {
        public Guid Id { get; set; }
        public string AccuralName { get; set; }
        public string ReferralName { get; set; }
        public double AccuralPercent { get; set; }
        public string TransactionStatus { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal AccuralAmount { get; set; }
        public DateTime AccuralDate { get; set; }
    }
}
