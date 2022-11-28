using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class PurchaseService: IPurchaseService
    {
        private readonly AppDbContext dbContext;
        private readonly IPackagesService packagesService;
        private readonly IBonusesCalculationService bonusesCalculationService;
        private readonly IRewardAccrualService rewardAccrualService;

        public PurchaseService(AppDbContext dbContext,
                              IPackagesService packagesService,
                              IBonusesCalculationService bonusesCalculationService,
                              IRewardAccrualService rewardAccrualService)
        {
            this.dbContext = dbContext;
            this.packagesService = packagesService;
            this.bonusesCalculationService = bonusesCalculationService;
            this.rewardAccrualService = rewardAccrualService;
        }

        public async Task BuyPackageAsync(PackageModel package, Guid userId)
        {
            var currentUserPackage = await packagesService.GetUserPackageByIdAsync(userId);
            if (currentUserPackage != null && currentUserPackage.Price >= package.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            await AddNewPurchaseAsync(package, userId);
        }

        public async Task BuyPackageByCashAsync(PackageModel package, Guid userId)
        {
            var currentUserPackage = await packagesService.GetUserPackageByIdAsync(userId);
            if (currentUserPackage != null && currentUserPackage.Price >= package.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            var purchaseRequest = new CashPurchaseEntity()
            {
                UserId = userId,
                PackageId = package.Id,
                IsClosed = false,
            };

            await dbContext.CashPurchases.AddAsync(purchaseRequest);
            await dbContext.SaveChangesAsync();
        }

        public async Task BuyPackageByPackageTypeAsync(PackageType packageType, Guid userId)
        {
            var purchasedPackage = await packagesService.GetPackageByTypeAsync(packageType);
            var currentUserPackage = await packagesService.GetUserPackageByIdAsync(userId);

            if (currentUserPackage != null && currentUserPackage.Price >= purchasedPackage.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            await AddNewPurchaseAsync(purchasedPackage, userId);
        }    
       

        private async Task AddNewPurchaseAsync(PackageModel package, Guid userId)
        {
            var purchasePrice = package.Price;

            var purchase = new PurchaseEntity
            {
                UserId = userId,
                PackageId = package.Id,
                Price = purchasePrice,
                PurchaseDate = DateTime.Now
            };

            await dbContext.UsersPurchases.AddAsync(purchase);
            await dbContext.SaveChangesAsync();

            await bonusesCalculationService.CalculateBonusesForRefferalsAsync(userId, purchasePrice);
        }
    }
}
