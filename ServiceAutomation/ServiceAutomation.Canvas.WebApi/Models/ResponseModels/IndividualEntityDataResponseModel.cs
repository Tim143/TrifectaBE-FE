using ServiceAutomation.DataAccess.Models.Enums;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class IndividualEntityDataResponseModel
    {
        public string LegalEntityFullName { get; set; }
        public string HeadFullName { get; set; }
        public string LegalEntityAbbreviatedName { get; set; }
        public string HeadPosition { get; set; }
        public string UNP { get; set; }
        public string BaseOrganization { get; set; }
        public string AccountantName { get; set; }
        public string CertificateNumber { get; set; }
        public string RegistrationAuthority { get; set; }
        public DateTime CertificateDateIssue { get; set; }
        public string BankRegion { get; set; }
        public string BankLocality { get; set; }
        public string BankStreet { get; set; }
        public string BankHouseNumber { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string CheckingAccount { get; set; }
        public string SWIFT { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Index { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public Location Location { get; set; }
        public string RoomNumber { get; set; }
    }
}
