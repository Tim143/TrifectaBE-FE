using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageTemplateController : ApiBaseController
    {
        public PackageTemplateController()
        {

        }

        [HttpGet(Constants.Requests.PackageTemplate.GetPackTemplates)]
        public async Task<int> GetPackTemplates()
        {
            throw new NotImplementedException();
        }

        [HttpPost(Constants.Requests.PackageTemplate.BuyPackTemplate)]
        public async Task<int> BuyPackTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
