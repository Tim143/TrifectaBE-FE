using Microsoft.EntityFrameworkCore;
using ServiceAutomation.DataAccess.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ServiceAutomation.DataAccess.DbContexts;
using System.Linq;

namespace ServiceAutomation.Canvas.WebApi.Interfaces
{
    public interface ITMPService
    {
        Task<IDictionary<Level, int>> GetLevelsInfoInReferralStructureByUserIdAsync(Guid userId);
    }

    public class TMPService : ITMPService
    {
        private readonly AppDbContext dbContext;
        public TMPService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IDictionary<Level, int>> GetLevelsInfoInReferralStructureByUserIdAsync(Guid userId)
        {
            var groupId = await dbContext.Users.AsNoTracking()
                                               .Where(u => u.Id == userId)
                                               .Select(u => u.Group.Id)
                                               .FirstAsync();

            var getLevelsInBranchInfosString = GetLevelsInfoSqlQueryString(groupId);
            var levelsInfo = await dbContext.UserLevelsInfos
                                              .FromSqlRaw(getLevelsInBranchInfosString)
                                              .Include(x => x.BasicLevel)
                                              .ToDictionaryAsync(x => x.BasicLevel.Level, x => x.BranchCount);
            return levelsInfo;
        }

        private string GetLevelsInfoSqlQueryString(Guid groupId)
        {
            var getLevelsInBranchInfos = "with recursive resultGroup as (\n"
                                         + "SELECT firstLine.\"Id\",\n"
                                         + "firstLine.\"OwnerUserId\",\n"
                                         + "firstLine.\"ParentId\",\n"
                                         + "users.\"BasicLevelId\",\n"
                                         + "users.\"Id\" as \"OwnerBranchId\",\n"
                                         + "ROW_NUMBER() OVER(ORDER BY firstLine.\"Id\") as \"BranchNumber\"\n"
                                         + "FROM public.\"TenantGroups\" as firstLine\n"
                                         + "inner join public.\"Users\" as users on firstLine.\"OwnerUserId\" = users.\"Id\"\n"
                                         + $"where firstLine.\"ParentId\" = '{groupId}'\n"
                                         + "union all\n"
                                         + "select childTenantGroup.\"Id\",\n"
                                         + "childTenantGroup.\"OwnerUserId\",\n"
                                         + "childTenantGroup.\"ParentId\",\n"
                                         + "childUser.\"BasicLevelId\",\n"
                                         + "res.\"OwnerBranchId\",\n"
                                         + "res.\"BranchNumber\"\n"
                                         + "FROM public.\"TenantGroups\" as childTenantGroup\n"
                                         + "inner join public.\"Users\" as childUser on childTenantGroup.\"OwnerUserId\" = childUser.\"Id\"\n"
                                         + "inner join resultGroup as res\n"
                                         + "on childTenantGroup.\"ParentId\" = res.\"Id\"\n"
                                         + ")\n"

                                         + "select levelsInfos.\"BasicLevelId\", count(levelsInfos.\"BranchNumber\") as \"BranchCount\"\n"
                                         + "from("
                                         + "select \"BranchNumber\", \"OwnerBranchId\", \"BasicLevelId\", count(\"BasicLevelId\") as \"CountLevelInBranch\" from resultGroup\n"
                                         + "group by \"BranchNumber\", \"BasicLevelId\", \"OwnerBranchId\"\n"
                                         + ") as levelsInfos \n"
                                         + "group by \"BasicLevelId\"";


            return getLevelsInBranchInfos;
        }
    }
}
