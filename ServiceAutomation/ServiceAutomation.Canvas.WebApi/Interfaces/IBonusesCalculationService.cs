using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IBonusesCalculationService
    {
        Task CalculateBonusesForRefferalsAsync(Guid userId, decimal purchasePrice);
    }
}
