using System;

namespace ServiceAutomation.DataAccess.Models.EntityModels
{
    public class UserLevelsInfoEntity
    {
        public int BranchCount { get; set; }

        public Guid BasicLevelId { get; set; }

        public BasicLevelEntity BasicLevel { get; set; }

    }
}
