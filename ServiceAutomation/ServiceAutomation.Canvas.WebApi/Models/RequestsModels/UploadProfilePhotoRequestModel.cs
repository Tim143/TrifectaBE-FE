using Microsoft.AspNetCore.Http;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class UploadProfilePhotoRequestModel
    {
        public IFormFile ProfilePhoto { get; set; }
        public Guid UserId { get; set; }
    }
}
