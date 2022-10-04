using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IPackagesService
    {
        Task<IEnumerable<PackageModel>> GetPackagesAsync();

        Task<PackageModel> GetPackageByIdAsync(Guid packageId);

        Task<UserPackageModel> GetUserPackageByIdAsync(Guid userId);

        Task<PackageModel> GetPackageByTypeAsync(PackageType packageType);
    }
}
