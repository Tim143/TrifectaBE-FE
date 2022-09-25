using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class ResultModel
    {
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
