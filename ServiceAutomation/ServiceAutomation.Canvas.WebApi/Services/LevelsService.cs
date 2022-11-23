using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ServiceAutomation.Canvas.WebApi.Constants.Requests;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelsService : ILevelsService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITurnoverService turnoverService;
        private readonly ITMPService tenantGroupService;

        public LevelsService(AppDbContext dbContext, IMapper mapper, ITurnoverService turnoverService, ITMPService tenantGroupService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.turnoverService = turnoverService;
            this.tenantGroupService = tenantGroupService;
        }

        public async Task<LevelInfoModel> GetUserMonthlyLevelInfoAsync(Guid userId, LevelModel basicLevelModel)
        {
            var userMonthlyTurnover = await turnoverService.GetMonthlyTurnoverByUserIdAsync(userId);

            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => l.Level == _dbContext.MonthlyLevels
                                                             .Where(x => (!x.Turnover.HasValue || x.Turnover.Value < userMonthlyTurnover)
                                                                          && x.Level <= (Level)basicLevelModel.Level)
                                                             .Max(x => x.Level))
                                                             .SingleOrDefaultAsync();
            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(monthlyLevel),
                CurrentTurnover = userMonthlyTurnover,
            };

            return lefelInfoModel;
        }

        public async Task<LevelModel> GetNextMonthlyLevelAsync(int level)
        {
            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => ((int)l.Level) == level + 1)
                                                             .SingleOrDefaultAsync();

            return _mapper.Map<LevelModel>(monthlyLevel);
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

        public async Task<LevelInfoModel> GetUserBasicLevelAsync(Guid userId)
        {
            var turnover = await turnoverService.GetTurnoverByUserIdAsync(userId);
            return await GetUserBasicLevelAsync(userId, turnover);
        }

        public async Task<LevelInfoModel> CalculateBasicLevelByTurnoverAsync(Guid userId, decimal turnover)
        {
            return await GetUserBasicLevelAsync(userId, turnover);
        }

        private async Task<LevelInfoModel> GetUserBasicLevelAsync(Guid userId, decimal turnover)
        {
            BasicLevelEntity[] basicLevels = await GetBasicLevelsAsync();
            var user = await _dbContext.Users.Include(x => x.BasicLevel).FirstOrDefaultAsync(x => x.Id == userId);
            var levelsInfo = await tenantGroupService.GetLevelsInfoInReferralStructureByUserIdAsync(userId);
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

            //var currentBasicLevel = newLevel;

            if(user.BasicLevel == null || user.BasicLevel.Level < newLevel.Level)
            {
                user.BasicLevelId = newLevel.Id;
                await _dbContext.SaveChangesAsync();
            }

            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(newLevel),
                CurrentTurnover = turnover,
            };

            return lefelInfoModel;
        }

        private async Task<BasicLevelEntity[]> GetBasicLevelsAsync()
        {
            if (_basicLevels == null)
            {
                _basicLevels = await _dbContext.BasicLevels.ToArrayAsync();
            }

            return _basicLevels;
        }

        public async Task<LevelInfoModel> GetUserMonthlyLevelInfoWithouLastPurchaseAsync(Guid userId, LevelModel basicLevelModel, decimal price)
        {
            var userMonthlyTurnover = await turnoverService.GetMonthlyTurnoverByUserIdAsync(userId) - price;

            var levelsUser = await GetLevelsInfoInReferralStructureByUserIdAsync(userId);


            var monthlyLevel = new MonthlyLevelEntity();

            monthlyLevel = await _dbContext.MonthlyLevels.AsNoTracking().Where(l => l.Level == _dbContext.MonthlyLevels
                                                             .Where(x => (x.Turnover == null || x.Turnover.Value <= userMonthlyTurnover)
                                                                          && x.Level <= (Level)basicLevelModel.Level)
                                                             .Max(x => x.Level))
                                                             .SingleOrDefaultAsync();

            //if (monthlyLevel.Id == Guid.Parse("02142b58-bba1-4ba2-91ce-bcd1414489f0"))
            //{
            //    monthlyLevel = await _dbContext.MonthlyLevels.AsNoTracking().FirstOrDefaultAsync(l => l.Id == Guid.Parse("3d0c7240-1b5a-493e-8288-6a347d06903a"));
            //}
            //else
            //{
            //    monthlyLevel = await _dbContext.MonthlyLevels.AsNoTracking().Where(l => l.Level == _dbContext.MonthlyLevels
            //                                                 .Where(x => (!x.Turnover.HasValue || x.Turnover.Value <= userMonthlyTurnover)
            //                                                              && x.Level <= (Level)basicLevelModel.Level)
            //                                                 .Max(x => x.Level))
            //                                                 .SingleOrDefaultAsync();
            //}


            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(monthlyLevel),
                CurrentTurnover = userMonthlyTurnover,
            };

            return lefelInfoModel;
        }

        private BasicLevelEntity[] _basicLevels;

        private async Task<IDictionary<Level, int>> GetLevelsInfoInReferralStructureByUserIdAsync(Guid userId)
        {
            var groupId = await _dbContext.Users.AsNoTracking()
                                               .Where(u => u.Id == userId)
                                               .Select(u => u.Group.Id)
                                               .FirstAsync();

            var getLevelsInBranchInfosString = GetLevelsInfoSqlQueryString(groupId);
            var levelsInfo = await _dbContext.UserLevelsInfos
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

        public async Task<LevelInfoModel> CalculateBasicLevelByTurnoverWithPreviousPurchaseAsync(Guid userId, decimal turnover)
        {
            BasicLevelEntity[] basicLevels = await GetBasicLevelsAsync();
            var user = await _dbContext.Users.Include(x => x.BasicLevel).FirstOrDefaultAsync(x => x.Id == userId);
            var levelsInfo = await tenantGroupService.GetLevelsInfoInReferralStructureByUserIdAsync(userId);
            levelsInfo.Remove(levelsInfo.Last().Key);

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

            //var currentBasicLevel = newLevel;

            if (user.BasicLevel == null || user.BasicLevel.Level < newLevel.Level)
            {
                user.BasicLevelId = newLevel.Id;
                await _dbContext.SaveChangesAsync();
            }

            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(newLevel),
                CurrentTurnover = turnover,
            };

            return lefelInfoModel;
        }
    }
}

