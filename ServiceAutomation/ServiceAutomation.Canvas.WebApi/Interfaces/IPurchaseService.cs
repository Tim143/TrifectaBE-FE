using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IPurchaseService
    {
        Task BuyPackageAsync(PackageModel package, Guid userId);
        Task BuyPackageByCashAsync(PackageModel package, Guid userId);
        Task BuyPackageByPackageTypeAsync(PackageType packageType, Guid userId);

    }
}
