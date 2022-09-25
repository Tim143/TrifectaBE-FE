using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class UserProfileResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Byte[] ProfilePhoto { get; set; }
        public string PersonalReferral { get; set; }
    }
}
