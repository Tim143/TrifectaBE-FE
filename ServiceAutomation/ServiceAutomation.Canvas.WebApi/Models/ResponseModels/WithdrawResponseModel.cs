using ServiceAutomation.DataAccess.Schemas.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class WithdrawResponseModel
    {
        public string CardCode { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
