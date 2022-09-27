using ServiceAutomation.DataAccess.Schemas.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class AccuralResponseModel
    {
        public string AccuralName { get; set; }
        public string ReferralName { get; set; }
        public int AccuralPercent { get; set; }
        public string TransactionStatus { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal AccuralAmount { get; set; }
        public DateTime AccuralDate { get; set; }
    }
}
