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
            var currentBasicLevel = newLevel;
            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(currentBasicLevel),
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

        private BasicLevelEntity[] _basicLevels;
    }
}

