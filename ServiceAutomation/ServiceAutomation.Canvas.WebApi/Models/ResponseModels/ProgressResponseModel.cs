namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class ProgressResponseModel
    {
        public decimal AvailableForWithdrawal { get; set; }
        public decimal AwaitingAccrual { get; set; }
        public decimal AllTimeIncome { get; set; }

        public BaseLevelProgressInfoModel BaseLevelProgress { get; set; }

        public StructuralLevelProgressInfoModel StructuralLevelProgress { get; set; }

        public AutoBonusProgressInfoModel AutoBonusProgress { get; set; }

        public TravelBonusInfoModel TravelBonusInfo { get; set; }
    }
}
