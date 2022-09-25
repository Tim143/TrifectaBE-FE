using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAutomation.Canvas.WebApi.Interfaces;
using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoTemplateController : ApiBaseController
    {
        private readonly IVideoTemplateService videoTemplateService;
        public VideoTemplateController(IVideoTemplateService videoTemplateService)
        {
            this.videoTemplateService = videoTemplateService;
        }

        [Authorize]
        [HttpGet(Constants.Requests.VideoTemplate.GetVideos)]
        public async Task<IEnumerable<VideoLessonResponseModel>> GetAllVideoTemplates()
        {
            return await videoTemplateService.GetVideos();
        }

        [Authorize]
        [HttpGet(Constants.Requests.VideoTemplate.GetVideo)]
        public async Task<VideoLessonResponseModel> GetVideoTemplate(Guid id)
        {
            return await videoTemplateService.GetVideo(id);
        }
    }
}
