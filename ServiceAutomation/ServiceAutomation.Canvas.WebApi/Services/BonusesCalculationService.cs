using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class BonusesCalculationService : IBonusesCalculationService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILevelsService _levelsService;
        private readonly ITurnoverService _turnoverService;
        private readonly IRewardAccrualService _rewardAccrualService;

        public BonusesCalculationService(AppDbContext dbContext,
                                       ITurnoverService turnoverService,
                                       ILevelsService levelsService,
                                       IRewardAccrualService rewardAccrualService)
        {
            _dbContext = dbContext;
            _turnoverService = turnoverService;
            _levelsService = levelsService;
            _rewardAccrualService = rewardAccrualService;
        }

        public async Task CalculateBonusesForRefferalsAsync(Guid userId, decimal purchasePrice)
        {
            await CalculateBonesesForSale(userId, purchasePrice);
            await CalculateBonusesForBasicLavelAsync(userId, purchasePrice);
        }

        private async Task CalculateBonusesForBasicLavelAsync(Guid userId, decimal purchasePrice)
        {
            var parentUsers = await GetParentUsersAsync(userId);

            if (parentUsers == null)
            {
                return;
            }

            foreach (var user in parentUsers)
            {
                var currentTurnover = await _turnoverService.GetTurnoverByUserIdAsync(user.Id);
                var turnoverWithoutPurchase = currentTurnover - purchasePrice;

                var currentBasicLevel = await _levelsService.CalculateBasicLevelByTurnoverAsync(userId, currentTurnover);
                var previousBasicLevel = await _levelsService.CalculateBasicLevelByTurnoverAsync(userId, turnoverWithoutPurchase);

                if (previousBasicLevel.CurrentLevel.Level < currentBasicLevel.CurrentLevel.Level)
                {
                    await _rewardAccrualService.AccrueRewardForBasicLevelAsync(user.Id, currentBasicLevel);
                }
            }
        }

        private async Task CalculateBonesesForSale(Guid userId, decimal purchasePrice)
        {
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

        private async Task<UserEntity[]> GetParentUsersAsync(Guid userId)
        {
            var tenantGroup = await _dbContext.Users
                                              .Where(u => u.Id == userId)
                                              .Select(x => x.Group)
                                              .FirstOrDefaultAsync();
            if (tenantGroup.ParentId == null)
            {
                return null;
            }

            var getParentUsersQuery = GetParenUsersSqlQueryString(tenantGroup);

            var parentUsers = await _dbContext.Users
                                              .FromSqlRaw(getParentUsersQuery)
                                              .Include(x => x.Group)
                                              .ToArrayAsync();
            return parentUsers;
        }

        private string GetParenUsersSqlQueryString(TenantGroupEntity tenantGroup)
        {
            var parentGroupId = tenantGroup.ParentId;

            var getUserParentsQuery = "with recursive resultGroup as ("
                                    + "SELECT parentGroup.\"Id\",\n"
                                    + "parentGroup.\"OwnerUserId\",\n"
                                    + "parentGroup.\"ParentId\",\n"
                                    + "1 as \"Level\""
                                    + "FROM public.\"TenantGroups\" as parentGroup\n"
                                    + $"where parentGroup.\"Id\" ='{parentGroupId}'\n"
                                    + "union all\n"
                                    + "select parentGroup2.\"Id\",\n"
                                    + "parentGroup2.\"OwnerUserId\",\n"
                                    + "parentGroup2.\"ParentId\",\n"
                                    + "parent.\"Level\" + 1 as \"Level\""
                                    + "FROM public.\"TenantGroups\" as parentGroup2\n"
                                    + "inner join resultGroup parent on parent.\"ParentId\" = parentGroup2.\"Id\")\n"

                                    + "SELECT u.\"Id\",\n u.\"FirstName\",\n u.\"LastName\",\n u.\"Email\",\n u.\"Country\",\n u.\"PersonalReferral\",\n"
                                    + "u.\"InviteReferral\",\n u.\"PasswordHash\",\n u.\"PasswordSalt\", u.\"DateOfBirth\", \n"
                                    + "u.\"IsVerifiedUser\",\n u.\"BasicLevelId\",\n u.\"Patronymic\",\n u.\"PhoneNumber\",\n u.\"Role\" \n"
                                    + "from resultGroup\n"
                                    + "inner join public.\"Users\" as u on u.\"Id\" = resultGroup.\"OwnerUserId\"\n"
                                    + "order by \"Level\"";

            return getUserParentsQuery;
        }       
    }
}
