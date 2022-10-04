
namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class AutoBonusProgressInfoModel
    {
        public LevelModel BaseLevel { get; set; }

        public decimal CurrentMonthlyTurnover { get; set; }

        public decimal RequiredMonthlyTurnoverToNextLevel { get; set; }
    }
}
