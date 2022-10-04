namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class StructuralLevelProgressInfoModel
    {
        public LevelModel CurrentLevel { get; set; }

        public decimal CurrentMonthlyTurnover { get; set; }

        public decimal RequiredMonthlyTurnoverToNextLevel { get; set; }
    }
}
