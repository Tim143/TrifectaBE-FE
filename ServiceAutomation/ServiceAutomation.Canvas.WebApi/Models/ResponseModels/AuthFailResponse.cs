using System;
using System.Collections.Generic;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class AuthFailResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
