using System;
namespace ServiceAutomation.Canvas.WebApi.Models
{
    public class PasswordHashModel
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
