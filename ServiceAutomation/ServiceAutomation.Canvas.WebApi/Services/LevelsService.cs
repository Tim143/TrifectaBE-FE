using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelsService : ILevelsService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public LevelsService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LevelModel> GetMonthlyLevelByUserIdAsync(Guid userId)
        {
            var monthlyLevel = await _dbContext.MonthlyLevelStatistics.AsNoTracking()
                                                                  .Where(m => m.UserId == userId)
                                                                  .OrderByDescending(s => s.ReachingLevelDate)
                                                                  .Select(m => m.Level)                                                                  
                                                                  .FirstAsync();
            return _mapper.Map<LevelModel>(monthlyLevel);
        }

        public async Task<LevelInfoModel> GetMonthlyLevelInfoByUserIdAsync(Guid userId)
        {
            var mothlyLevelStatistic = await _dbContext.MonthlyLevelStatistics.AsNoTracking()
                                                                  .Where(m => m.UserId == userId)
                                                                  .Include( m => m.Level)
                                                                  .OrderByDescending(s => s.ReachingLevelDate)
                                                                  .FirstAsync();

            var monthlyLevel = _mapper.Map<LevelModel>(mothlyLevelStatistic.Level);
            var monthlyLevelInfo = new LevelInfoModel
            {
                CurrentLevel = monthlyLevel,
                CurrentTurnover = mothlyLevelStatistic.Turnover ?? 0
            };

            return monthlyLevelInfo;
        }

        public async Task<LevelModel> GetCurrentMonthlyLevelByTurnoverAsync(decimal monthlyTurnover)
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
            var basicLevelStatistic = await _dbContext.BasicLevelStatistics
                                                      .AsNoTracking()
                                                      .Where(u => u.UserId == userId)
                                                      .Include(b => b.Level)
                                                      .OrderByDescending( b => b.ReachingLevelDate)
                                                      .FirstOrDefaultAsync();

            var basicLevelInfo = new LevelInfoModel
            {
                CurrentLevel = _mapper.Map<LevelModel>(basicLevelStatistic.Level),
                CurrentTurnover = basicLevelStatistic.Turnover ?? 0
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
    }
}

