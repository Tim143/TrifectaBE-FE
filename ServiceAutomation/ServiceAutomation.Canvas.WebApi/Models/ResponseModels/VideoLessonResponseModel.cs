using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class VideoLessonResponseModel
    {
        public Guid Id { get; set; }
        public string VideoName { get; set; }
        public string VideoFullPath { get; set; }
    }
}
