using ServiceAutomation.Canvas.WebApi.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class BonusCalculatorService : IBonusCalculatorService
    {
        private readonly IPackagesService packagesService;
        public BonusCalculatorService(IPackagesService packagesService)
        {
            this.packagesService = packagesService;
        }

        public async Task<int> CalculateBonusesAsync(Guid userId)
        {
            var levelBonus = await GetLevelBonusAsync(userId);

            return 0;
        }

        private async Task<decimal> GetLevelBonusAsync(Guid userId)
        {
            var userPurchases = await packagesService.GetUserPackageAsync(userId);
            

            if(userPurchases == null)
            {
                return 0;
            }

            if(userPurchases.Name == "Classic")
            {

            }
            else if(userPurchases.Name == "Premium")
            {

            }

            return 0;
        }
    }
}
