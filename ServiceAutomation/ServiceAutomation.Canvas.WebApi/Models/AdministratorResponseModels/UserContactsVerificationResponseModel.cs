using ServiceAutomation.DataAccess.Models.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.AdministratorResponseModels
{
    public class UserContactsVerificationResponseModel
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public ContactVerificationType ContactVerificationType { get; set; }
        public string NewData { get; set; }
    }
}
