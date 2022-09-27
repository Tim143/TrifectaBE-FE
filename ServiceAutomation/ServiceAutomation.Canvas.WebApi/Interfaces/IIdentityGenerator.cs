using System;

namespace ServiceAutomaion.Services.Interfaces
{
    public interface IIdentityGenerator
    {
        Guid Generate();
    }
}
