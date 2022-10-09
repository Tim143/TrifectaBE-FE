using ServiceAutomation.DataAccess.Schemas.Enums;
using System.Collections.Generic;
using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserEntity : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }
        public Country Country { get; set; }
        public string PersonalReferral { get; set; }
        public string InviteReferral { get; set; }

        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsVerifiedUser { get; set; }

        public Guid? BasicLevelId { get; set; }

        public BasicLevelEntity BasicLevel { get; set; }

        public virtual CredentialEntity Credential { get; set; }
        public virtual TenantGroupEntity Group { get; set; }
        public virtual UserProfileInfoEntity UserContact { get; set; }
        public virtual ProfilePhotoEntity ProfilePhoto { get; set; }
        public virtual ICollection<PurchaseEntity> UserPurchases { get; set; }
        public virtual ICollection<AccrualsEntity> UserAccruals { get; set; }
        public virtual UserAccountOrganizationEntity UserAccountOrganization { get; set; }
    }
}
