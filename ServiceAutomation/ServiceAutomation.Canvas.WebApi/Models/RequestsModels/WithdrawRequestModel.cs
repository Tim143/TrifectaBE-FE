using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class WithdrawRequestModel
    {
        public Guid UserId { get; set; }
        public ICollection<Guid> AccuralsId { get; set; }
    }
}
