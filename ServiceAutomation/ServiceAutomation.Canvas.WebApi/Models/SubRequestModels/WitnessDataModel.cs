using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.SubRequestModels
{
    public class WitnessDataModel 
    {
        public string CertificateNumber { get; set; }
        public string RegistrationAuthority { get; set; }
        public DateTime CertificateDateIssue { get; set; }
        public IFormFile FileData { get; set; }
    }
}
