using Microsoft.AspNetCore.Http;
using System;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class UploadUserProfileRequestModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
