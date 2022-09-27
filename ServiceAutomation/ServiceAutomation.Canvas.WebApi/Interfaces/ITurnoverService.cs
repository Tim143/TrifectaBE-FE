using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ITurnoverService
    {
        /// <summary>
        /// Returns the user's monthly turnover in the current month
        /// </summary>
        Task<decimal> GetMonthlyTurnoverByUserIdAsync(Guid userId);

        Task<decimal> GetTurnoverByUserIdAsync(Guid userId);
    }
}
