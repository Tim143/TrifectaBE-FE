using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class PackagesService : IPackagesService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public PackagesService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<PackageModel> GetPackageByIdAsync(Guid packageId)
        {
            var package = await dbContext.Packages.FirstAsync(x => x.Id == packageId);
            return mapper.Map<PackageModel>(package);
        }

        public async Task<IEnumerable<PackageModel>> GetPackagesAsync()
        {
            var packagesEntities = await dbContext.Packages
                .Include(x => x.PackageBonuses)
                .ThenInclude(x => x.Bonus)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            return packagesEntities.Select(x => mapper.Map<PackageModel>(x));
        }

        public async Task<PackageModel> GetUserPackageAsync(Guid userId)
        {
            var package = await dbContext.UsersPurchases
                                .AsNoTracking()
                                .Where(x => x.UserId == userId)
                                .OrderByDescending(x => x.PurchaseDate)
                                .Include(x=> x.Package)
                                .ThenInclude(x => x.PackageBonuses)
                                .ThenInclude(x => x.Bonus)
                                .Select(x => x.Package)                                
                                .FirstOrDefaultAsync();

            return mapper.Map<PackageModel>(package);
        }

    }
}
