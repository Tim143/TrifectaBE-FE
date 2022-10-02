using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class PurchaseService: IPurchaseService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPackagesService _packagesService;
        private readonly ILevelCalculationService _levelCalculationService;

        public PurchaseService(AppDbContext dbContext, IPackagesService packagesService, ILevelCalculationService levelCalculationService)
        {
            _dbContext = dbContext;
            _packagesService = packagesService;
            _levelCalculationService = levelCalculationService;
        }

        public async Task BuyPackageAsync(PackageModel package, Guid userId)
        {
            var currentUserPackage = await _packagesService.GetUserPackageAsync(userId);

            if (currentUserPackage != null && currentUserPackage.Price > package.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            var purchase = new PurchaseEntity
            {
                UserId = userId,
                PackageId = package.Id,
                Price = package.Price,
                PurchaseDate = DateTime.Now
            };

            await _dbContext.UsersPurchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            await _levelCalculationService.СalculateParentPartnersLevelsAsync(userId);
        }

        public async Task BuyPackageByPackageTypeAsync(PackageType packageType, Guid userId)
        {
            var purchasedPackage = await _dbContext.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.Type == packageType);

            var currentUserPackage = await _packagesService.GetUserPackageAsync(userId);

            if (currentUserPackage != null && currentUserPackage.Price > purchasedPackage.Price)
            {
                throw new Exception("The purchased package must be larger than the current one!");
            }

            var purchase = new PurchaseEntity
            {
                UserId = userId,
                PackageId = purchasedPackage.Id,
                Price = purchasedPackage.Price,
                PurchaseDate = DateTime.Now
            };

            await _dbContext.UsersPurchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            await _levelCalculationService.СalculateParentPartnersLevelsAsync(userId);
        }
    }
}
