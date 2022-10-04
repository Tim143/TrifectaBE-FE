using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.DbContexts;
using ServiceAutomation.DataAccess.Models.EntityModels;
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
            var withdraws = await dbContext.WithdrawTransactions.Where(x => x.UserId == userId).ToListAsync();

            var today = DateTime.Now;
            //if (period != 0 && transactionStatus != 0)
            //{
            //    switch (period)
            //    {
            //        case PeriodType.OneWeek:
            //            var oneWeekPeriod = new DateTime(today.Year, today.Month, today.Day - 7);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneWeekPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.OneMonth:
            //            var oneMonthPeriod = new DateTime(today.Year, today.Month - 1, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneMonthPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.ThreeMonth:
            //            var threeMonthPeriod = new DateTime(today.Year, today.Month - 3, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= threeMonthPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.OneYear:
            //            var oneYearPeriod = new DateTime(today.Year - 1, today.Month, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneYearPeriod && x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //    }
            //}
            
            //if(transactionStatus != 0)
            //{
            //    return user?.Credential?.WithdrawTransactions?.Where(x => x.TransactionStatus == transactionStatus).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //}

            //if(period != 0)
            //{
            //    switch (period)
            //    {
            //        case PeriodType.OneWeek:
            //            var oneWeekPeriod = new DateTime(today.Year, today.Month, today.Day - 7);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneWeekPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.OneMonth:
            //            var oneMonthPeriod = new DateTime(today.Year, today.Month - 1, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneMonthPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.ThreeMonth:
            //            var threeMonthPeriod = new DateTime(today.Year, today.Month - 3, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= threeMonthPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //        case PeriodType.OneYear:
            //            var oneYearPeriod = new DateTime(today.Year - 1, today.Month, today.Day);
            //            return user?.Credential?.WithdrawTransactions?.Where(x => x.Date >= oneYearPeriod).Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
            //    }
            //}

            return withdraws.Select(x => mapper.Map<WithdrawResponseModel>(x)).OrderByDescending(x => x.DateTime);
        }

        public async Task<IEnumerable<AccuralResponseModel>> GetAccuralHistory(Guid userId, TransactionStatus transactionStatus = default, BonusType bonus = default)
        {
            var accruals = await dbContext.Accruals.AsNoTracking()
                                                   .Where(x => x.UserId == userId)
                                                   .Include(x => x.ForWhom)
                                                   .Include(x => x.Bonus)
                                                   .ToListAsync();
            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));

            //if (bonus != 0 && transactionStatus != 0)
            //{
            //    switch (bonus)
            //    {
            //        case BonusType.LevelBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.AutoBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.BunBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.DynamicBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.TravelBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.BonusOverall:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //        case BonusType.TeamBonus:
            //            return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //    }
            //}

            //if (transactionStatus != 0)
            //{
            //    return accruals.Select(x => mapper.Map<AccuralResponseModel>(x));
            //}
        }

        public async Task MakeWithdraw(Guid userId, ICollection<Guid> accuralsId)
        {
            var accuralList = new List<AccrualsEntity>();
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            foreach(var accural in accuralsId)
            {
                var result = await dbContext.Accruals.FirstOrDefaultAsync(x => x.Id == accural);
                result.TransactionStatus = TransactionStatus.Pending;

                await dbContext.SaveChangesAsync();

                accuralList.Add(result);
            }

            var accuralVerificationRequest = new UserAccuralsVerificationEntity()
            {
                UserId = userId,
                Accurals = accuralList,
                User = user,
                IsVerified = false
            };

            await dbContext.UserAccuralsVerifications.AddAsync(accuralVerificationRequest);
            await dbContext.SaveChangesAsync();
        }
    }
}
