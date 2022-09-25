using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IVideoTemplateService
    {
        Task<IEnumerable<VideoLessonResponseModel>> GetVideos();
        Task<VideoLessonResponseModel> GetVideo(Guid id);
    }
}
