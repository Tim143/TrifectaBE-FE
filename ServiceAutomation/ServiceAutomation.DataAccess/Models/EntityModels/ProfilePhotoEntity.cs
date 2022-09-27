using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class ProfilePhotoEntity : Entity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
