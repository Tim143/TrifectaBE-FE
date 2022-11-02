using ServiceAutomation.DataAccess.Models.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.AdministratorResponseModels
{
    public class UserVerificationResponseModel
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeOfEmployment { get; set; }
        public string SWIFT { get; set; }
        public string Number { get; set; }
        public string IdentityNumber { get; set; }
        public string CertificateDateIssue { get; set; }
        public string CheckingAccount { get; set; }
        public string BaseOrganization { get; set; }
        public string UNP { get; set; }
        public string RegistrationAuthority { get; set; }
        public string CertificateNumber { get; set; }
        public string VerivicationPhoto { get; set; }
        public string VerivicationPhoto2 { get; set; }
        public string VerivicationPhoto3 { get; set; }
        public string VerivicationPhoto4 { get; set; }
    }
}
