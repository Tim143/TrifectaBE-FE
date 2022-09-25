using ServiceAutomaion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAutomaion.Services.Services
{
    public class IdentityGenerator : IIdentityGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
