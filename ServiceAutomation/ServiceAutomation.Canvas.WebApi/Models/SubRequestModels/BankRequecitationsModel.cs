namespace ServiceAutomation.Canvas.WebApi.Models.SubRequestModels
{
    public class BankRequecitationsModel 
    {
        public string BankRegion { get; set; }
        public string BankLocality { get; set; }
        public string BankStreet { get; set; }
        public string BankHouseNumber { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string CheckingAccount { get; set; }
        public string SWIFT { get; set; }
    }
}
