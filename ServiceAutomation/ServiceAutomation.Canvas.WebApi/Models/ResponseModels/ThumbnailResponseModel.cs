using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class ThumbnailResponseModel
    {
        public Guid Id { get; set; }
        public string ThumbnailName { get; set; }
        public string ThumbnailFullPath { get; set; }
    }
}
