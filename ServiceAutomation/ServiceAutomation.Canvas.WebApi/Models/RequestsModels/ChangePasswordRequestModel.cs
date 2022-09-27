using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class ChangePasswordRequestModel
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
