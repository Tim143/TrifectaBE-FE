using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceAutomation.Canvas.WebApi.Models.RequestsModels
{
    public class RegisterRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ReferralCode { get; set; }
        public string DateOfBirth { get; set; }
        public Country country { get; set; }
    }
}
