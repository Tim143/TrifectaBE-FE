using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Constants;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class InfoController : ApiBaseController
    {
        private readonly IInfoService infoService;

        public InfoController(IInfoService infoService)
        {
            this.infoService = infoService;
        }

        [HttpGet(Requests.Info.GetThumbnails)]
        public async Task<IEnumerable<ThumbnailResponseModel>> GetThumbnails()
        {
            return await infoService.GetThumbnails();
        }

        [HttpGet(Requests.Info.GetThumbnailById)]
        public async Task<ThumbnailResponseModel> GetThumbnail(Guid id)
        {
            return await infoService.GetThumbnail(id);
        }
    }
}
