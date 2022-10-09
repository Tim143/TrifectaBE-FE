using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelCalculationService : ILevelCalculationService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILevelsService _levelsService;
        private readonly ITurnoverService _turnoverService;
        private readonly ILevelStatisticService _levelStatisticService;
        private readonly IRewardAccrualService _rewardAccrualForLevelService;
        private readonly ITenantGroupService _tenantGroupService;

        public LevelCalculationService(AppDbContext dbContext,
                                       ITurnoverService turnoverService,
                                       ILevelsService levelsService,
                                       ILevelStatisticService levelStatisticService,
                                       IRewardAccrualService rewardAccrualForLevelService,
                                       ITenantGroupService tenantGroupService)
        {
            _dbContext = dbContext;
            _turnoverService = turnoverService;
            _levelsService = levelsService;
            _levelStatisticService = levelStatisticService;
            _rewardAccrualForLevelService = rewardAccrualForLevelService;
            _tenantGroupService = tenantGroupService;
        }

        public async Task СalculateUserLevelsAsync(Guid userId)
        {
            var user = await _dbContext.Users.AsNoTracking().Include(u => u.BasicLevel).FirstAsync(u => u.Id == userId);
            var basicLevels = await _dbContext.BasicLevels.Include(x => x.PartnersLevel).ToArrayAsync();

            await СalculateUserBasicLevelsAsync(user, basicLevels);
            await CalculateUserMonthlyLevelAsync(user);
        }

        public async Task СalculateParentPartnersLevelsAsync(Guid userId)
        {
            var parentUsers = await GetParentUsersAsync(userId);

            if (parentUsers == null)
            {
                return;
            }

            var basicLevels = await _dbContext.BasicLevels.Include(x => x.PartnersLevel).ToArrayAsync();

            foreach (var user in parentUsers)
            {
                await СalculateUserBasicLevelsAsync(user, basicLevels);
                await CalculateUserMonthlyLevelAsync(user);
            }
        }

        private async Task CalculateUserMonthlyLevelAsync(UserEntity user)
        {
            var monthlyTurnover = await _turnoverService.GetMonthlyTurnoverByUserIdAsync(user.Id);
            var earnedMonthlyLevel = await _levelsService.GetCurrentMonthlyLevelByTurnoverAsync(monthlyTurnover, user.BasicLevel.Level);

            var currentLevel = await _levelStatisticService.GetMonthlyLevelByUserIdAsync(user.Id);

            if (earnedMonthlyLevel.Id != currentLevel.Id)
            {
                await _levelStatisticService.AddMonthlyLevelInfoAsync(user.Id, earnedMonthlyLevel.Id, monthlyTurnover);
            }
            else
            {
                await _levelStatisticService.UpdateMonthlyLevelInfoAsync(user.Id, currentLevel.Id, monthlyTurnover);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task СalculateUserBasicLevelsAsync(UserEntity user, BasicLevelEntity[] basicLevels)
        {
            var turnover = await _turnoverService.GetTurnoverByUserIdAsync(user.Id);

            var levelsInfo = await _tenantGroupService.GetLevelsInfoInReferralStructureByUserIdAsync(user.Id);
            var appropriateLevels = basicLevels.Where(l => l.Turnover == null || l.Turnover < turnover).OrderByDescending(l => (int)l.Level);

            BasicLevelEntity newLevel = null;

            foreach (var level in appropriateLevels)
            {
                if (level.PartnersLevel != null)
                {
                    var appropriateChildLevelsCount = levelsInfo.Where(x => x.Key >= level.PartnersLevel.Level).Sum(x => x.Value);
                    if (appropriateChildLevelsCount >= level.PartnersCount)
                    {
                        newLevel = level;
                    }
                }
                else
                {
                    newLevel = level;
                }

                if (newLevel != null)
                    break;
            }

            if (newLevel != null)
            {
                if (user.BasicLevel.Id != newLevel.Id)
                {
                    user.BasicLevel = newLevel;

                    await _levelStatisticService.AddBasicLevelInfoAsync(user.Id, newLevel.Id, turnover);
                    await _dbContext.SaveChangesAsync();
                    await _rewardAccrualForLevelService.AccrueRewardForBasicLevelAsync(user.Id);
                }
                else
                {
                    await _levelStatisticService.UpdateBasicLevelInfoAsync(user.Id, user.BasicLevelId.Value, turnover);
                }
            }
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
