namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class HomePageResponseModel
    {
        public decimal AvailableForWithdrawal { get; set; }
        public decimal AwaitingAccrual { get; set; }
        public decimal AllTimeIncome { get; set; }
        public LevelInfoModel BaseLevelInfo { get; set; }
        public NextBasicLevelRequirementsModel NextBasicLevelRequirements { get; set; }
        public LevelInfoModel MounthlyLevelInfo { get; set; }  
        public string ReuqiredAction { get; set; }
        public double ReceivedPayoutPercentage {get; set; }
        public PackageModel Package { get; set; }
    }
}
