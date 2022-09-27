using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class ChangePhoneNumberRequestModel
    {
        public Guid UserId { get; set; }
        public string NewPhoneNumber { get; set; }
    }
}
