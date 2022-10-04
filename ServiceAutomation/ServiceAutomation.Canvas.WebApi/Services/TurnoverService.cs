using Microsoft.EntityFrameworkCore;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.DataAccess.DbContexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Services
{
    public class TurnoverService : ITurnoverService
    {
        private readonly AppDbContext _dbContext;

        public TurnoverService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// for current month
        /// </summary>
        public async Task<decimal> GetMonthlyTurnoverByUserIdAsync(Guid userId)
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);

            var getPartnersPurchases = GetPartnersPurchasesSqlQueryString(userId, startDate);

            var turnover = await _dbContext.PartnerPurchase.FromSqlRaw(getPartnersPurchases)
                                                            .SumAsync(x => x.PurchasePrice);

            return turnover ?? 0;
        }

        public async Task<decimal> GetUserPersonalTurnoverByUserIdAsync(Guid userId)
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);

            var userIds = await _dbContext.Users.AsNoTracking()
                                                         .Where(u => u.Id == userId)
                                                         .SelectMany(u => u.Group.ChildGroups)
                                                         .Select(g => g.OwnerUser)
                                                         .Where(u => u.UserPurchases.Any(p => p.PurchaseDate >= startDate))
                                                         .Select(u => u.Id)
                                                         .ToArrayAsync();

            var personalTurnover = await _dbContext.UsersPurchases.AsNoTracking()
                                                                  .Where(p => userIds.Contains(p.UserId))
                                                                  .Where(p => p.PurchaseDate >= startDate)
                                                                  .GroupBy(p => p.UserId)
                                                                  .Select(x => x.Max(y => y.Price))
                                                                  .SumAsync();
            return personalTurnover;
        }

        public async Task<decimal> GetTurnoverByUserIdAsync(Guid userId)
        {
            var getPartnersPurchases = GetPartnersPurchasesSqlQueryString(userId);

            var turnover = await _dbContext.PartnerPurchase.FromSqlRaw(getPartnersPurchases)
                                                            .SumAsync(x => x.PurchasePrice);

            return turnover ?? 0;
        }

        private string GetPartnersPurchasesSqlQueryString(Guid userId, DateTime? startDate = null)
        {
            var getPartnersPurchasesQuery = "with recursive resultGroup as (\n"
                                            + "SELECT tenantGroup.\"Id\",\n"
                                            + "tenantGroup.\"OwnerUserId\",\n"
                                            + "tenantGroup.\"ParentId\"\n"
                                            + "FROM public.\"TenantGroups\" as tenantGroup\n"
                                            + "inner join public.\"Users\" as users on tenantGroup.\"OwnerUserId\" = users.\"Id\"\n"
                                            + $"where users.\"Id\" ='{userId}'\n"
                                            + "union all\n"
                                            + "select tenantGroup2.\"Id\",\n"
                                            + "tenantGroup2.\"OwnerUserId\",\n"
                                            + "tenantGroup2.\"ParentId\"\n"
                                            + "FROM public.\"TenantGroups\" as tenantGroup2\n"
                                            + "inner join resultGroup t2 on tenantGroup2.\"ParentId\" = t2.\"Id\")\n"
                                            + "SELECT purchases.\"UserId\" as UserId,\n"
                                            + "Max(\"Price\") as PurchasePrice\n"
                                            + "FROM public.\"Purchases\" purchases\n"
                                            + "inner join resultGroup on resultGroup.\"OwnerUserId\" = purchases.\"UserId\"\n"
                                            +  (startDate.HasValue ? $"where purchases.\"PurchaseDate\" >= '{ startDate }' and " : "where")
                                            + $" purchases.\"UserId\" != '{userId}' "
                                            + "group by \"UserId\"";

            return getPartnersPurchasesQuery;
        }
    }
}
