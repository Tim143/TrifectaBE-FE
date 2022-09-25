using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public abstract partial class Entity
    {
        public virtual Guid Id { get; protected internal set; }
    }
}
