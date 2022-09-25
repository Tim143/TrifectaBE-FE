using ServiceAutomation.Common.Models;
using System;
using System.Collections.Generic;


namespace ServiceAutomation.Common.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ReferralCode { get; set; }
        //public Country country { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
