using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class UploadUserProfileRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string IdentityCode { get; set; }
    }
}
