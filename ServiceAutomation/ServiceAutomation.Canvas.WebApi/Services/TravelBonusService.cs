using ServiceAutomation.Canvas.WebApi.Interfaces;
using System.Threading.Tasks;
using System;
using ServiceAutomation.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Models;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class TravelBonusService : ITravelBonusService
    {
        private readonly AppDbContext dbContext;
        private readonly IPackagesService packagesService;
        private readonly ITurnoverService turnoverService;

        public TravelBonusService(AppDbContext dbContext, IPackagesService packagesService, ITurnoverService turnoverService)
        {
            this.dbContext = dbContext;
            this.packagesService = packagesService;
            this.turnoverService = turnoverService;
        }

        public async Task<TravelBonusInfoModel> GetTravelBonusInfoByUserIdAsync(Guid userId)
        {
            var userPackage = await packagesService.GetUserPackageByIdAsync(userId);
            var requiredTurnover = await dbContext.TravelBonusRequirements.AsNoTracking()
                                                                           .FirstOrDefaultAsync(x => x.PackageId == userPackage.Id);

            var personalTurnover = await turnoverService.GetUserPersonalTurnoverByUserIdAsync(userId);

            return new TravelBonusInfoModel
            {
                UserPersonalMonthlyTurnover = personalTurnover,
                TravelBonusTurnover = requiredTurnover.Turnover
            };
        }
    }
}
