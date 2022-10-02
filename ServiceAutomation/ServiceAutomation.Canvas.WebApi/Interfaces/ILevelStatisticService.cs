using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelStatisticService
    {
        Task AddNewUserMonthlyLevelRecordAsync(Guid userId, Guid newLevelId, decimal currentMonthlyTurnover);

        Task UpdateUserMonthlyLevelTurnoverAsync(Guid userId, Guid ForLevelId, decimal newMonthTurnover);

        Task AddNewUserBasicLevelRecordAsync(Guid userId, Guid newLevelId, decimal currentTurnover);

        Task UpdateUserBasicLevelTurnoverAsync(Guid userId, Guid ForLevelId, decimal newTurnover);
    }
}
