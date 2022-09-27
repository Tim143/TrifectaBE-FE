using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected internal set; }
    }
}
