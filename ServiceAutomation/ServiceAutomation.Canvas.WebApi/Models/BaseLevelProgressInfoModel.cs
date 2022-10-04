namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class BaseLevelProgressInfoModel
    {
        public LevelModel BaseLevel { get; set; }

        public decimal CurrentCommonTurnover { get; set; }

        public int CountOfRefferralRequiredFoNextLevel { get; set; }

        public NextBasicLevelRequirementsModel NextBasicLevelRequirements { get; set; }
    }
}
