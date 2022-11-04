using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static ServiceAutomation.Canvas.WebApi.Constants.Requests;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class TenantGroupService : ITenantGroupService
    {
        private readonly AppDbContext dbContext;
        private readonly ILevelsService levelStatisticService;
        public TenantGroupService(AppDbContext dbContext, ILevelsService levelStatisticService)
        {
            this.dbContext = dbContext;
            this.levelStatisticService = levelStatisticService;
        }

        public async Task<ReferralGroupModel> GetReferralTree(Guid userId)
        {
           var referralGroup = await dbContext.Users.AsNoTracking()
                                                    .Where(u => u.Id == userId)
                                                    .Select(u => u.Group)
                                                    .Select(GetReferralGroupWithPartners)
                                                    .FirstOrDefaultAsync();

            var monthlyLevelInfo = await levelStatisticService.GetCurrentMonthlyLevelAsync(userId);
            var basicLevelInfo = await levelStatisticService.GetUserBasicLevelAsync(userId);

            referralGroup.GroupOwner.PersonalTurnover = (double)basicLevelInfo.CurrentTurnover;
            referralGroup.GroupOwner.GroupTurnover = (double)monthlyLevelInfo.CurrentTurnover;
            referralGroup.GroupOwner.MounthlyTurnover = (double)monthlyLevelInfo.CurrentTurnover;
            referralGroup.GroupOwner.BaseLevel = basicLevelInfo.CurrentLevel.Level;

            await FillReferralTreeAsync(referralGroup.PartnersGroups);
            return referralGroup;
        }

        private async Task FillReferralTreeAsync(ReferralGroupModel[] childPartners)
        {
            foreach(var partner in childPartners)
            {
                var childParetnerGroups = await GetPartnersReferralGroupsAsync(partner.Id);
                partner.PartnersGroups = childParetnerGroups;

                var monthlylevelinfo = await levelStatisticService.GetCurrentMonthlyLevelAsync(partner.GroupOwner.UserId);
                var basicLevelInfo = await levelStatisticService.GetUserBasicLevelAsync(partner.GroupOwner.UserId);

                partner.GroupOwner.PersonalTurnover = (double)basicLevelInfo.CurrentTurnover;
                partner.GroupOwner.GroupTurnover = (double)monthlylevelinfo.CurrentTurnover;
                partner.GroupOwner.MounthlyTurnover = (double)monthlylevelinfo.CurrentTurnover;
                partner.GroupOwner.BaseLevel = basicLevelInfo.CurrentLevel.Level;
                await FillReferralTreeAsync(childParetnerGroups);
            }
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
            HasPartners = g.ChildGroups.Any(),
            GroupOwner = new PartnerInfoModel
            {
                UserId = g.OwnerUserId,
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
                HasPartners = cg.ChildGroups.Any(),
                GroupOwner = new PartnerInfoModel
                {
                    UserId = cg.OwnerUserId,
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
                UserId = g.OwnerUserId,
                FirstName = g.OwnerUser.FirstName,
                LastName = g.OwnerUser.LastName,
                Email = g.OwnerUser.Email,
                PackageType = g.OwnerUser.UserPurchases.OrderByDescending(p => p.PurchaseDate)
                                                       .Select(p => p.Package.Name)
                                                       .FirstOrDefault()
            },
            HasPartners = g.ChildGroups.Any()
        };

        public async Task<IDictionary<Level, int>> GetLevelsInfoInReferralStructureByUserIdAsync(Guid userId)
        {
            var groupId = await dbContext.Users.AsNoTracking()
                                               .Where(u => u.Id == userId)
                                               .Select(u => u.Group.Id)
                                               .FirstAsync();

            var getLevelsInBranchInfosString = GetLevelsInfoSqlQueryString(groupId);
            var levelsInfo = await dbContext.UserLevelsInfos
                                              .FromSqlRaw(getLevelsInBranchInfosString)
                                              .Include(x => x.BasicLevel)
                                              .ToDictionaryAsync(x => x.BasicLevel.Level, x => x.BranchCount);
            return levelsInfo;
        }

        private string GetLevelsInfoSqlQueryString(Guid groupId)
        {
            var getLevelsInBranchInfos = "with recursive resultGroup as (\n"
                                         + "SELECT firstLine.\"Id\",\n"
                                         + "firstLine.\"OwnerUserId\",\n"
                                         + "firstLine.\"ParentId\",\n"
                                         + "users.\"BasicLevelId\",\n"
                                         + "users.\"Id\" as \"OwnerBranchId\",\n"
                                         + "ROW_NUMBER() OVER(ORDER BY firstLine.\"Id\") as \"BranchNumber\"\n"
                                         + "FROM public.\"TenantGroups\" as firstLine\n"
                                         + "inner join public.\"Users\" as users on firstLine.\"OwnerUserId\" = users.\"Id\"\n"
                                         + $"where firstLine.\"ParentId\" = '{groupId}'\n"
                                         + "union all\n"
                                         + "select childTenantGroup.\"Id\",\n"
                                         + "childTenantGroup.\"OwnerUserId\",\n"
                                         + "childTenantGroup.\"ParentId\",\n"
                                         + "childUser.\"BasicLevelId\",\n"
                                         + "res.\"OwnerBranchId\",\n"
                                         + "res.\"BranchNumber\"\n"
                                         + "FROM public.\"TenantGroups\" as childTenantGroup\n"
                                         + "inner join public.\"Users\" as childUser on childTenantGroup.\"OwnerUserId\" = childUser.\"Id\"\n"
                                         + "inner join resultGroup as res\n"
                                         + "on childTenantGroup.\"ParentId\" = res.\"Id\"\n"
                                         + ")\n"

                                         + "select levelsInfos.\"BasicLevelId\", count(levelsInfos.\"BranchNumber\") as \"BranchCount\"\n"
                                         + "from("
                                         + "select \"BranchNumber\", \"OwnerBranchId\", \"BasicLevelId\", count(\"BasicLevelId\") as \"CountLevelInBranch\" from resultGroup\n"
                                         + "group by \"BranchNumber\", \"BasicLevelId\", \"OwnerBranchId\"\n"
                                         + ") as levelsInfos \n"
                                         + "group by \"BasicLevelId\"";


            return getLevelsInBranchInfos;
        }
    }
}
