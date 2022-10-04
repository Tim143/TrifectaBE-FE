using ServiceAutomation.Canvas.WebApi.Models;
using ServiceAutomation.Common.Models;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ITenantGroupService
    {
        Task<ReferralGroupModel> GetReferralTree(Guid userId);

        Task CreateTenantGroupForUserAsync(UserModel userModel);

        Task<ReferralGroupModel> GetReferralGroupByUserIdAsync(Guid userId);

        Task<ReferralGroupModel> GetReferralGroupAsync(Guid groupId);

        Task<ReferralGroupModel[]> GetPartnersReferralGroupsAsync(Guid groupId);

        Task<IDictionary<Level, int>> GetLevelsInfoInReferralStructureByUserIdAsync(Guid userId);
    }
}
