using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Canvas.WebApi.Models.RequestsModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class PackagesController : ApiBaseController
    {
        private readonly IPackagesService packagesService;

        private readonly IPurchaseService purchaseService;
        public PackagesController(IPackagesService packagesService, IPurchaseService purchaseService)
        {
            this.packagesService = packagesService;
            this.purchaseService = purchaseService;
        }

        [HttpGet(Constants.Requests.Package.GetPackages)]
        public async Task<IEnumerable<PackageModel>> GetPackagesAsync()
        {
            return await packagesService.GetPackagesAsync();
        }

        [HttpPost(Constants.Requests.Package.BuyPackage)]
        public async Task<IActionResult> BuyPackageAsync([FromBody] BuyPackageRequestModel buyPackageRequest)
        {
            var userId = GetCurrentUserId();

            var package = await packagesService.GetPackageByIdAsync(buyPackageRequest.PackageId);

            if (package == null)
            {
                return BadRequest();
            }

            await purchaseService.BuyPackageAsync(package, userId);

            return Ok();
        }

        [HttpGet(Constants.Requests.Package.GetUserPackage)]
        public async Task<PackageModel> GetUserPackageAsync(Guid userId)
        {
            return await packagesService.GetUserPackageByIdAsync(userId);
        }

    }
}
