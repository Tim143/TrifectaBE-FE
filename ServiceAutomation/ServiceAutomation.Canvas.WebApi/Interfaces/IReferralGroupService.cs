using ServiceAutomation.Canvas.WebApi.Models.ResponseModels;
using ServiceAutomation.DataAccess.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface IReferralGroupService
    {
        Task<TenantGroupEntity> GetReferralTree(Guid userId);
    }
}
