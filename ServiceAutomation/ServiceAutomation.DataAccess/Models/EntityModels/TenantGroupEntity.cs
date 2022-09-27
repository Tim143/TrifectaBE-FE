using System;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class TenantGroupEntity : Entity
    {
        public Guid OwnerUserId { get; set; }
        public Guid? ParentId { get; set; }
        public TenantGroupEntity Parent { get; set; }

        public ICollection<TenantGroupEntity> ChildGroups { get; set; }
        public virtual UserEntity OwnerUser { get; set; }
    }
}
