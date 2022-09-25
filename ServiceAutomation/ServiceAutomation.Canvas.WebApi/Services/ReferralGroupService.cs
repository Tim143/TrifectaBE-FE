using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class ReferralGroupService : IReferralGroupService
    {
        private readonly AppDbContext dbContext;
        public ReferralGroupService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TenantGroupEntity> GetReferralTree(Guid userId)
        {
            var referralGroup = await dbContext.TenantGroups.Where(x => x.OwnerUserId == userId).Include(x => x.ChildGroups).FirstOrDefaultAsync();

            return referralGroup;
        }
    }
}
