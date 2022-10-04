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
    public class LevelStatisticService : ILevelStatisticService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public LevelStatisticService(AppDbContext dbContext, IMapper mapper)
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
            var monthlyLevelStatistic = await _dbContext.MonthlyLevelStatistics.AsNoTracking()
                                                                               .Where(m => m.UserId == userId)
                                                                               .Include(m => m.Level)
                                                                               .OrderByDescending(s => s.ReachingLevelDate)
                                                                               .FirstAsync();
            LevelEntity currentMonthlyLevel;
            decimal currentTurnover;

            var currentDate = DateTime.Today;
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            if (monthlyLevelStatistic.ReachingLevelDate < firstDayOfMonth)
            {
                var secondLevel = await _dbContext.MonthlyLevels.FirstAsync(l => l.Level == Level.SecondLevel);
                var statistic = new MonthlyLevelStatisticEntity
                {
                    UserId = userId,
                    LevelId = secondLevel.Id,
                    ReachingLevelDate = firstDayOfMonth
                };

                var monthlyLevelStatisticEntity = await _dbContext.MonthlyLevelStatistics.AddAsync(statistic);
                await _dbContext.SaveChangesAsync();

                currentMonthlyLevel = secondLevel;
                currentTurnover = 0;
            }
            else
            {
                currentMonthlyLevel = monthlyLevelStatistic.Level;
                currentTurnover = monthlyLevelStatistic.Turnover ?? 0;
            }

            var monthlyLevelInfo = new LevelInfoModel
            {
                CurrentLevel = _mapper.Map<LevelModel>(currentMonthlyLevel),
                CurrentTurnover = currentTurnover
            };

            return monthlyLevelInfo;
        }

        public async Task<LevelInfoModel> GetBasicLevelInfoByUserIdAsync(Guid userId)
        {
            var basicLevelStatistic = await _dbContext.BasicLevelStatistics
                                                      .AsNoTracking()
                                                      .Where(u => u.UserId == userId)
                                                      .Include(b => b.Level)
                                                      .OrderByDescending(b => b.ReachingLevelDate)
                                                      .FirstOrDefaultAsync();

            var basicLevelInfo = new LevelInfoModel
            {
                CurrentLevel = _mapper.Map<LevelModel>(basicLevelStatistic.Level),
                CurrentTurnover = basicLevelStatistic.Turnover ?? 0
            };

            return basicLevelInfo;
        }

        public async Task AddMonthlyLevelInfoAsync(Guid userId, Guid newLevelId, decimal currentMonthlyTurnover)
        {
            var monthlyStatisticRecord = new MonthlyLevelStatisticEntity
            {
                UserId = userId,
                LevelId = newLevelId,
                ReachingLevelDate = DateTime.Now,
                Turnover = currentMonthlyTurnover
            };

            await _dbContext.MonthlyLevelStatistics.AddAsync(monthlyStatisticRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMonthlyLevelInfoAsync(Guid userId, Guid ForLevelId, decimal newMonthTurnover)
        {
            var basicLevelStatistic = await _dbContext.MonthlyLevelStatistics.FirstAsync(s => s.UserId == userId && s.LevelId == ForLevelId);
            basicLevelStatistic.Turnover = newMonthTurnover;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddBasicLevelInfoAsync(Guid userId, Guid newLevelId, decimal currentTurnover)
        {
            var basicLevelStatistic = new BasicLevelStatisticEntity
            {
                UserId = userId,
                LevelId = newLevelId,
                ReachingLevelDate = DateTime.Now,
                Turnover = currentTurnover
            };

            await _dbContext.BasicLevelStatistics.AddAsync(basicLevelStatistic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateBasicLevelInfoAsync(Guid userId, Guid ForLevelId, decimal newTurnover)
        {
            var basicLevelStatistic = await _dbContext.BasicLevelStatistics.FirstAsync(s => s.UserId == userId && s.LevelId == ForLevelId);
            basicLevelStatistic.Turnover = newTurnover;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddLevelsInfoForNewUserAsync(Guid userId)
        {
            var secondMonthlyLevel = await _dbContext.MonthlyLevels.FirstAsync(l => l.Level == Level.FirstLevel);
            await AddMonthlyLevelInfoAsync(userId, secondMonthlyLevel.Id, 0);

            var firstBasicLevel = await _dbContext.BasicLevels.FirstAsync(b => b.Level == Level.FirstLevel);
            await AddBasicLevelInfoAsync(userId, firstBasicLevel.Id, 0);
        }
    }
}
