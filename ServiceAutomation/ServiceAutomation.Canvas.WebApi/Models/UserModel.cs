using ServiceAutomation.DataAccess.Schemas.Enums;
using System;

namespace ServiceAutomation.Common.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string InviteCode { get; set; }
        public Guid GroupId { get; set; }
        public Country Country { get; set; }
        public bool IsVeriveid { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
