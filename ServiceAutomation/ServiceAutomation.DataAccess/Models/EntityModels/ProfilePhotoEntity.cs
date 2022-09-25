using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class ProfilePhotoEntity : Entity
    {
        public Guid UserId { get; set; }
        public Byte[] Data { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
