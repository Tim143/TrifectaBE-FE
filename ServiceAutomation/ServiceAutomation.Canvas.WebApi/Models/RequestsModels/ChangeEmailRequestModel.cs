using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class ChangeEmailRequestModel
    {
        public Guid UserId { get; set; }
        public string NewEmailAdress { get; set; }
    }
}
