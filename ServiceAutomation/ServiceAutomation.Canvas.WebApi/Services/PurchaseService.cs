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
        private readonly AppDbContext _dbContext;
        private readonly IPackagesService _packagesService;
        private readonly ILevelCalculationService _levelCalculationService;
        private readonly IRewardAccrualService _rewardAccrualService;

        public PurchaseService(AppDbContext dbContext,
                              IPackagesService packagesService,
                              ILevelCalculationService levelCalculationService,
                              IRewardAccrualService rewardAccrualService)
        {
            _dbContext = dbContext;
            _packagesService = packagesService;
            _levelCalculationService = levelCalculationService;
            _rewardAccrualService = rewardAccrualService;
        }

        public async Task BuyPackageAsync(PackageModel package, Guid userId)
        {
            var currentUserPackage = await _packagesService.GetUserPackageByIdAsync(userId);
            if (currentUserPackage != null && currentUserPackage.Price >= package.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            await AddNewPurchaseAsync(package, userId);
        }

        public async Task BuyPackageByCashAsync(PackageModel package, Guid userId)
        {
            var currentUserPackage = await _packagesService.GetUserPackageByIdAsync(userId);
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

            await _dbContext.CashPurchases.AddAsync(purchaseRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BuyPackageByPackageTypeAsync(PackageType packageType, Guid userId)
        {
            var purchasedPackage = await _packagesService.GetPackageByTypeAsync(packageType);
            var currentUserPackage = await _packagesService.GetUserPackageByIdAsync(userId);

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

            await _dbContext.UsersPurchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            await _levelCalculationService.СalculateUserLevelsAsync(userId);
            await _levelCalculationService.СalculateParentPartnersLevelsAsync(userId);

            var inviteReferral = await _dbContext.Users.AsNoTracking()
                                           .Where(u => u.Id == userId)
                                           .Select(u => u.InviteReferral)
                                           .FirstOrDefaultAsync();
            if (inviteReferral == null)
                return;

            var referralUserId = await _dbContext.Users.AsNoTracking()
                                                       .Where(u => u.PersonalReferral == inviteReferral)
                                                       .Select(u => u.Id)
                                                       .FirstOrDefaultAsync();

            await _rewardAccrualService.AccrueRewardForSaleAsync(referralUserId, userId, purchasePrice);
        }
    }
}
