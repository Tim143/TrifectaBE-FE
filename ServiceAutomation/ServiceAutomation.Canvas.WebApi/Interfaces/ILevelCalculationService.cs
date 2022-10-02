using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ILevelCalculationService
    {
        Task СalculateParentPartnersLevelsAsync(Guid userId);
    }
}
