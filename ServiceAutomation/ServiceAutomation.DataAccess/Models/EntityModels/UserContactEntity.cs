using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserContactEntity : Entity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string IdentityCode { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
