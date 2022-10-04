
namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class CalulatedRewardInfoModel
    {
        public decimal InitialReward { get; init; } = 0;

        public decimal Reward { get; init; } = 0;

        public double? Percent { get; init; }
    }
}
