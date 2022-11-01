﻿using AutoMapper;
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
        private readonly ITurnoverService turnoverService;

        public LevelsService(AppDbContext dbContext, IMapper mapper, ITurnoverService turnoverService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this.turnoverService = turnoverService;
        }

        public async Task<LevelModel> GetCurrentMonthlyLevelByTurnoverAsync(decimal monthlyTurnover, Level basicLevel)
        {
            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => l.Level == _dbContext.MonthlyLevels
                                                             .Where(x => (!x.Turnover.HasValue || x.Turnover.Value < monthlyTurnover)
                                                                          && x.Level <= basicLevel)
                                                             .Max(x => x.Level))
                                                             .SingleOrDefaultAsync();

            return _mapper.Map<LevelModel>(monthlyLevel);
        }

        public async Task<LevelInfoModel> GetCurrentMonthlyLevelAsync(Guid userId)
        {
            var basicLevel = await _dbContext.Users.Where(x => x.Id == userId).Select(x => x.BasicLevel.Level).FirstAsync();
            var turnover = await turnoverService.GetMonthlyTurnoverByUserIdAsync(userId);

            var monthlyLevel = await _dbContext.MonthlyLevels.Where(l => l.Level == _dbContext.MonthlyLevels
                                                             .Where(x => (!x.Turnover.HasValue || x.Turnover.Value < turnover)
                                                                          && x.Level <= basicLevel)
                                                             .Max(x => x.Level))
                                                             .SingleOrDefaultAsync();
            var lefelInfoModel = new LevelInfoModel()
            {
                CurrentLevel = _mapper.Map<LevelModel>(monthlyLevel),
                CurrentTurnover = turnover,
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
    }
}

