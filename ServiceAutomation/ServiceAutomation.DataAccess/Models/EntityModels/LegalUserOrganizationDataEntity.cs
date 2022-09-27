using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class LegalUserOrganizationDataEntity : Entity
    {
        public Guid UserId { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string BankStreet { get; set; }
        public string BankHouseNumber { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string CheckingAccount { get; set; }
        public string SWIFT { get; set; }
        public string Disctrict { get; set; }
        public string City { get; set; }
        public string Index { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Flat { get; set; }
    }
}
