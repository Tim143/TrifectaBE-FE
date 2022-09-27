using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class TenantGroupService : ITenantGroupService
    {
        private readonly AppDbContext dbContext;
        public TenantGroupService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TenantGroupEntity> GetReferralTree(Guid userId)
        {
            var referralGroup = await dbContext.TenantGroups.Where(x => x.OwnerUserId == userId).Include(x => x.ChildGroups).FirstOrDefaultAsync();

            return referralGroup;
        }

        public async Task CreateTenantGroupForUserAsync(UserModel userModel)
        {
            var tenantGroup = new TenantGroupEntity
            {
                OwnerUserId = userModel.Id
            };

            if (!string.IsNullOrEmpty(userModel.InviteCode))
            {
                var parentTenantGroupId = await dbContext.Users.Where(x => x.PersonalReferral == userModel.InviteCode)
                                                               .Select(x => x.Group.Id)
                                                               .FirstOrDefaultAsync();
                
                tenantGroup.ParentId = parentTenantGroupId == Guid.Empty ? null : parentTenantGroupId;
            }

            await dbContext.TenantGroups.AddAsync(tenantGroup);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get user referral group with first line of partners.
        /// </summary>
        public async Task<ReferralGroupModel> GetReferralGroupByUserIdAsync(Guid userId)
        {
            var referralGroup = await dbContext.Users.AsNoTracking()
                                                     .Where(u => u.Id == userId)
                                                     .Select(u => u.Group)
                                                     .Select(GetReferralGroupWithPartners)
                                                     .FirstOrDefaultAsync();
            return referralGroup;

        }

        public async Task<ReferralGroupModel> GetReferralGroupAsync(Guid groupId)
        {
            var referralGroup = await dbContext.TenantGroups.AsNoTracking()
                                                            .Where(g => g.Id == groupId)
                                                            .Select(GetReferralGroupWithPartners)
                                                            .FirstOrDefaultAsync();
            return referralGroup;
        }

        public async Task<ReferralGroupModel[]> GetPartnersReferralGroupsAsync(Guid groupId)
        {
            var partnersReferralGroups = await dbContext.TenantGroups.AsNoTracking()
                                                                    .Where(g => g.Id == groupId)
                                                                    .SelectMany(g => g.ChildGroups)
                                                                    .Select(GetReferralGroup)
                                                                    .ToArrayAsync();
            return partnersReferralGroups;
        }

        private readonly Expression<Func<TenantGroupEntity, ReferralGroupModel>> GetReferralGroupWithPartners = g => new ReferralGroupModel
        {
            Id = g.Id,
            HasPartners = g.ChildGroups.Count != 0,
            GroupOwner = new PartnerInfoModel
            {
                //UserId = g.Id,
                FirstName = g.OwnerUser.FirstName,
                LastName = g.OwnerUser.LastName,
                Email = g.OwnerUser.Email,
                PackageType = g.OwnerUser.UserPurchases.OrderByDescending(p => p.PurchaseDate)
                                                       .Select(p => p.Package.Name)
                                                       .FirstOrDefault()
            },
            PartnersGroups = g.ChildGroups.Select(cg => new ReferralGroupModel
            {
                Id = cg.Id,
                HasPartners = g.ChildGroups.Count != 0,
                GroupOwner = new PartnerInfoModel
                {
                    FirstName = cg.OwnerUser.FirstName,
                    LastName = cg.OwnerUser.LastName,
                    Email = cg.OwnerUser.Email,
                    PackageType = cg.OwnerUser.UserPurchases.OrderByDescending(p => p.PurchaseDate)
                                                            .Select(p => p.Package.Name)
                                                            .FirstOrDefault()
                }
            }).ToArray()
        };

        private readonly Expression<Func<TenantGroupEntity, ReferralGroupModel>> GetReferralGroup = g => new ReferralGroupModel
        {
            Id = g.Id,
            GroupOwner = new PartnerInfoModel
            {
                FirstName = g.OwnerUser.FirstName,
                LastName = g.OwnerUser.LastName,
                Email = g.OwnerUser.Email,
                PackageType = g.OwnerUser.UserPurchases.OrderByDescending(p => p.PurchaseDate)
                                                       .Select(p => p.Package.Name)
                                                       .FirstOrDefault()
            },
            HasPartners = g.ChildGroups.Count != 0
        };
    }
}
