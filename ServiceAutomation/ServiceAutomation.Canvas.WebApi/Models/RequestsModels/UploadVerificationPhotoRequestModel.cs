using Microsoft.AspNetCore.Http;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class UploadVerificationPhotoRequestModel
    {
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public IFormFile Photo3 { get; set; }
        public IFormFile Photo4 { get; set; }
        public Guid UserId { get; set; }
    }
}
