using System;

namespace ServiceAutomation.Canvas.WebApi.Models.ResponseModels
{
    public class PackageVerificationResponseModel
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public Guid PackageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PackageName { get; set; } 
    }
}
