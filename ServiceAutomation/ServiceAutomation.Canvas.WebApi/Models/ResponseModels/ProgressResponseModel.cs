namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class ProgressResponseModel
    {
        public decimal AvailableForWithdrawal { get; set; }
        public decimal AwaitingAccrual { get; set; }
        public decimal AllTimeIncome { get; set; }
        public LevelInfoModel BaseLevelInfo { get; set; }
        public NextBasicLevelRequirementsModel NextBasicLevelRequirements { get; set; }
        public int PartnersCurrentLevelCount { get; set; }
        public LevelInfoModel MounthlyLevelInfo { get; set; }
        public decimal? NextMounthlyLevelRequirement { get; set; }
    }
}
