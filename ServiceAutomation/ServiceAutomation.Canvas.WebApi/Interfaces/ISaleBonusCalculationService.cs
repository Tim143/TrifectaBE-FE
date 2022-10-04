using ServiceAutomation.Canvas.WebApi.Models;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ISaleBonusCalculationService
    {
        Task<CalulatedRewardInfoModel> CalculateStartBonusRewardAsync(decimal sellingPrice, UserPackageModel userPackage, int userSalesCount);
        Task<CalulatedRewardInfoModel> CalculateDynamicBonusRewardAsync(decimal sellingPrice, UserPackageModel userPackage, int saleNumber);
    }
}
