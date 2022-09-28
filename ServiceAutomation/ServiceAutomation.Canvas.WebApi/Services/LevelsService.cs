using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelsService : ILevelsService
    {
        private readonly AppDbContext _dbContext;
        private readonly ITurnoverService _turnoverService;
        private readonly IMapper _mapper;

        public LevelsService(AppDbContext dbContext, ITurnoverService turnoverService, IMapper mapper)
        {
            _dbContext = dbContext;
            _turnoverService = turnoverService;
            _mapper = mapper;
        }

        public async Task<LevelModel> GetMonthlyLevelByUserIdAsync(Guid userId)
        {
            var monthlyTurnover = await _turnoverService.GetMonthlyTurnoverByUserIdAsync(userId);
            return await GetCurrentMonthlyLevelByTurnoverAsync(monthlyTurnover);
        }

        public async Task<LevelInfoModel> GetMonthlyLevelInfoByUserIdAsync(Guid userId)
        {
            var monthlyTurnover = await _turnoverService.GetMonthlyTurnoverByUserIdAsync(userId);
            var monthlyLevel = await GetCurrentMonthlyLevelByTurnoverAsync(monthlyTurnover);

            var monthlyLevelInfo = new LevelInfoModel
            {
                CurrentLevel = monthlyLevel,
                CurrentTurnover = monthlyTurnover
            };

            return monthlyLevelInfo;
        }

        private async Task<LevelModel> GetCurrentMonthlyLevelByTurnoverAsync(decimal monthlyTurnover)
        {
            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => l.Level == _dbContext.MonthlyLevels
                                                             .Where(x => !x.Turnover.HasValue || x.Turnover.Value < monthlyTurnover)
                                                             .Max(x => x.Level))
                                                             .SingleOrDefaultAsync();

            return _mapper.Map<LevelModel>(monthlyLevel);
        }

        public async Task<LevelModel> GetNextMonthlyLevelAsync(int level)
        {
            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => ((int)l.Level) == level + 1)
                                                             .SingleOrDefaultAsync();

            return _mapper.Map<LevelModel>(monthlyLevel);
        }

        public async Task<LevelInfoModel> GetBasicLevelInfoByUserIdAsync(Guid userId)
        {
            var basicLevel = await _dbContext.Users.AsNoTracking()
                                                   .Where(u => u.Id == userId)
                                                   .Select(x => x.BasicLevel)
                                                   .SingleOrDefaultAsync();

            var turnover = await _turnoverService.GetTurnoverByUserIdAsync(userId);

            var basicLevelInfo = new LevelInfoModel
            {
                CurrentLevel = _mapper.Map<LevelModel>(basicLevel),
                CurrentTurnover = turnover
            };

            return basicLevelInfo;
        }

        public async Task<NextBasicLevelRequirementsModel> GetNextBasicLevelRequirementsAsync(Level currentUserBasicLevel)
        {
            var nextBasicLevel = currentUserBasicLevel + 1;

            var nextBasicLevelEntity = await _dbContext.BasicLevels.AsNoTracking()
                                                                   .Include(b => b.PartnersLevel)
                                                                   .FirstAsync(l => l.Level == nextBasicLevel);

            return new NextBasicLevelRequirementsModel
            {
                GroupTurnover = nextBasicLevelEntity.Turnover,
                PartnersRequirementCount = nextBasicLevelEntity.PartnersCount,
                PartnersRequirementLevel = (int?)nextBasicLevelEntity.PartnersLevel?.Level ?? null
            };            
        }

        public async Task СalculatePartnersBasicLevelsAsync(Guid userId)
        {
            var parentUsers = await GetParentUsersAsync(userId);

            if (parentUsers == null)
            {
                return;
            }

            var basicLevels = await _dbContext.BasicLevels.Include(x => x.PartnersLevel).ToArrayAsync();

            foreach (var user in parentUsers)
            {
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
                            levelsInfo[user.BasicLevel.Level]--;
                            user.BasicLevel = newLevel;
                            levelsInfo[user.BasicLevel.Level]++;
                        }
                        break;
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
                                    + "u.\"UserAccountOrganizationId\",\n u.\"BasicLevelId\""
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

