using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.Enums;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class WithdrawService : IWithdrawService
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public WithdrawService(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WithdrawResponseModel>> GetWithdrawHistory(Guid userId, TransactionStatus transactionStatus = default, PeriodType period = default)
        {
            var user = await dbContext.Users.Where(x => x.Id == userId)
                .Include(x => x.Credential)
                .ThenInclude(x => x.WithdrawTransactions)
                .FirstOrDefaultAsync();

            var today = DateTime.Now;
            if (period != 0 && transactionStatus != 0)
            {
                switch (period)
                {
                    case PeriodType.OneWeek:
                        var oneWeekPeriod = new DateTime(today.Year, today.Month, today.Day - 7);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneWeekPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.OneMonth:
                        var oneMonthPeriod = new DateTime(today.Year, today.Month - 1, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneMonthPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.ThreeMonth:
                        var threeMonthPeriod = new DateTime(today.Year, today.Month - 3, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= threeMonthPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.OneYear:
                        var oneYearPeriod = new DateTime(today.Year - 1, today.Month, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneYearPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                }
            }
            
            if(transactionStatus != 0)
            {
                return user?.Credential?.WithdrawTransactions?.Where(x => x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            }

            if(period != 0)
            {
                switch (period)
                {
                    case PeriodType.OneWeek:
                        var oneWeekPeriod = new DateTime(today.Year, today.Month, today.Day - 7);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneWeekPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.OneMonth:
                        var oneMonthPeriod = new DateTime(today.Year, today.Month - 1, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneMonthPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.ThreeMonth:
                        var threeMonthPeriod = new DateTime(today.Year, today.Month - 3, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= threeMonthPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                    case PeriodType.OneYear:
                        var oneYearPeriod = new DateTime(today.Year - 1, today.Month, today.Day);
                        return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneYearPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
                }
            }

            return user?.Credential?.WithdrawTransactions?.Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
        }

        public async Task<IEnumerable<AccuralResponseModel>> GetAccuralHistory(Guid userId)
        {
            var user = await dbContext.Accruals.Where(x => x.UserId == userId).ToListAsync();
            return user.Select(x => mapper.Map<AccuralResponseModel>(x));
        }
    }
}
