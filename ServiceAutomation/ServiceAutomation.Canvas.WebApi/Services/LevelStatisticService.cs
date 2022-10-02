using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class LevelStatisticService : ILevelStatisticService
    {
        private readonly AppDbContext _dbContext;
        public LevelStatisticService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewUserMonthlyLevelRecordAsync(Guid userId, Guid newLevelId, decimal currentMonthlyTurnover)
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

        public async Task UpdateUserMonthlyLevelTurnoverAsync(Guid userId, Guid ForLevelId, decimal newMonthTurnover)
        {
            var basicLevelStatistic = await _dbContext.MonthlyLevelStatistics.FirstAsync(s => s.UserId == userId && s.LevelId == ForLevelId);
            basicLevelStatistic.Turnover = newMonthTurnover;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewUserBasicLevelRecordAsync(Guid userId, Guid newLevelId, decimal currentTurnover)
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

        public async Task UpdateUserBasicLevelTurnoverAsync(Guid userId, Guid ForLevelId, decimal newTurnover)
        {
            var basicLevelStatistic = await _dbContext.BasicLevelStatistics.FirstAsync( s => s.UserId == userId && s.LevelId == ForLevelId);
            basicLevelStatistic.Turnover = newTurnover;
            await _dbContext.SaveChangesAsync();
        }
    }
}
