using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class SalesService: ISalesService
    {
        private readonly AppDbContext _dbContext;

        public SalesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetUserSalesCountAsync(Guid userId)
        {
            var referralUsersIds = await _dbContext.Users.AsNoTracking()
                                                         .Where(u => u.Id == userId)
                                                         .Select(u => u.Group)
                                                         .SelectMany(g => g.ChildGroups)
                                                         .Select(g => g.OwnerUserId)
                                                         .ToArrayAsync();

            var countSales = await _dbContext.UsersPurchases.AsNoTracking()
                                                            .Where(p => referralUsersIds.Contains(p.UserId))
                                                            .CountAsync();
            return countSales;
        }

        public async Task<int> GerSalesCountInMonthAsync(Guid userId)
        {
            var referralUsersIds = await _dbContext.Users.AsNoTracking()
                                             .Where(u => u.Id == userId)
                                             .Select(u => u.Group)
                                             .SelectMany(g => g.ChildGroups)
                                             .Select(g => g.OwnerUserId)
                                             .ToArrayAsync();

            var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var countSales = await _dbContext.UsersPurchases.AsNoTracking()
                                                            .Where(p => referralUsersIds.Contains(p.UserId))
                                                            .Where(p => p.PurchaseDate >= startOfMonth)
                                                            .CountAsync();
            return countSales;
        }
    }
}
