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

        public LevelCalculationService(AppDbContext dbContext,
                                       ITurnoverService turnoverService,
                                       ILevelsService levelsService,
                                       ILevelStatisticService levelStatisticService)
        {
            _dbContext = dbContext;
            _turnoverService = turnoverService;
            _levelsService = levelsService;
            _levelStatisticService = levelStatisticService;
        }

        public async Task СalculateParentPartnersLevelsAsync(Guid userId)
        {
            var parentUsers = await GetParentUsersAsync(userId);

            if (parentUsers == null)
            {
                return;
            }

            foreach (var user in parentUsers)
            {
                await CalculateUserMonthlyLevelAsync(user);
                await СalculatePartnersBasicLevelsAsync(user);
            }
        }

        private async Task CalculateUserMonthlyLevelAsync(UserEntity user)
        {
            var monthlyTurnover = await _turnoverService.GetMonthlyTurnoverByUserIdAsync(user.Id);
            var earnedMonthlyLevel = await _levelsService.GetCurrentMonthlyLevelByTurnoverAsync(monthlyTurnover);

            var currentLevel = await _levelsService.GetMonthlyLevelByUserIdAsync(user.Id);

            if (earnedMonthlyLevel.Level < currentLevel.Level)
            {
                throw new Exception("current level cannot be less than earned");
            }

            if (earnedMonthlyLevel.Id != currentLevel.Id && earnedMonthlyLevel.Level > currentLevel.Level)
            {
                await _levelStatisticService.AddNewUserMonthlyLevelRecordAsync(user.Id, earnedMonthlyLevel.Id, monthlyTurnover);
            }
            else
            {
                await _levelStatisticService.UpdateUserMonthlyLevelTurnoverAsync(user.Id, currentLevel.Id, monthlyTurnover);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task СalculatePartnersBasicLevelsAsync(UserEntity user)
        {
            var basicLevels = await _dbContext.BasicLevels.Include(x => x.PartnersLevel).ToArrayAsync();

            var turnover = await _turnoverService.GetTurnoverByUserIdAsync(user.Id);

            var getLevelsInBranchInfosString = GetLevelsInfoSqlQueryString(user);
            var levelsInfo = await _dbContext.UserLevelsInfos
                                              .FromSqlRaw(getLevelsInBranchInfosString)
                                              .Include(x => x.BasicLevel)
                                              .ToDictionaryAsync(x => x.BasicLevel.Level, x => x.BranchCount);
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
                {
                    if (user.BasicLevel != newLevel)
                    {
                        var currlevel = user.BasicLevel.Level;
                        if (levelsInfo.ContainsKey(currlevel))
                        {
                            levelsInfo[currlevel]--;
                        }

                        user.BasicLevel = newLevel;

                        if (levelsInfo.ContainsKey(currlevel))
                        {
                            levelsInfo[currlevel]++;
                        }
                        else
                        {
                            levelsInfo[currlevel] = 1;
                        }
                        await _levelStatisticService.AddNewUserBasicLevelRecordAsync(user.Id, user.BasicLevelId.Value, turnover);
                    }
                    else
                    {
                        await _levelStatisticService.UpdateUserBasicLevelTurnoverAsync(user.Id, user.BasicLevelId.Value, turnover);
                    }
                }
            }

            await _dbContext.SaveChangesAsync();
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
                                    + "u.\"InviteReferral\",\n u.\"PasswordHash\",\n u.\"PasswordSalt\",\n u.\"UserPhoneNumberId\",\n"
                                    + "u.\"IsVerifiedUser\",\n u.\"BasicLevelId\""
                                    + "from resultGroup\n"
                                    + "inner join public.\"Users\" as u on u.\"Id\" = resultGroup.\"OwnerUserId\"\n"
                                    + "order by \"Level\"";

            return getUserParentsQuery;
        }

        private string GetLevelsInfoSqlQueryString(UserEntity user)
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
                                         + $"where firstLine.\"ParentId\" = '{user.Group.Id}'\n"
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
