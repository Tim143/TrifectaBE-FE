using ServiceAutomation.DataAccess.Schemas.Enums;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserEntity : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Country Country { get; set; }
        public string PersonalReferral { get; set; }
        public string InviteReferral { get; set; }

        //public Role Role { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<CredentialEntity> Credentionals { get; set; }
        public virtual TenantGroupEntity Group { get; set; }
        public virtual UserContactEntity UserContact { get; set; }
        public virtual ProfilePhotoEntity ProfilePhoto { get; set; }
    }
}
