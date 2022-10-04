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
        private readonly AppDbContext _dbContext;
        private readonly IPackagesService _packagesService;
        private readonly ITurnoverService _turnoverService;

        public TravelBonusService(AppDbContext dbContext, IPackagesService packagesService, ITurnoverService turnoverService)
        {
            _dbContext = dbContext;
            _packagesService = packagesService;
            _turnoverService = turnoverService;
        }

        public async Task<TravelBonusInfoModel> GetTravelBonusInfoByUserIdAsync(Guid userId)
        {
            var userPackage = await _packagesService.GetUserPackageByIdAsync(userId);
            var requiredTurnover = await _dbContext.TravelBonusRequirements.AsNoTracking()
                                                                           .FirstOrDefaultAsync(x => x.PackageId == userPackage.Id);

            var personalTurnover = await _turnoverService.GetUserPersonalTurnoverByUserIdAsync(userId);

            return new TravelBonusInfoModel
            {
                UserPersonalMonthlyTurnover = personalTurnover,
                TravelBonusTurnover = requiredTurnover.Turnover
            };
        }
    }
}
