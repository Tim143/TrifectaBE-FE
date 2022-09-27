using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserProfileInfoEntity : Entity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
