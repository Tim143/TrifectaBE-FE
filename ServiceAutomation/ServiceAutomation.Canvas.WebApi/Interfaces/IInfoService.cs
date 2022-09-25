using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IInfoService
    {
        Task<IEnumerable<ThumbnailResponseModel>> GetThumbnails();
        Task<ThumbnailResponseModel> GetThumbnail(Guid id);
    }
}
