using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    internal static class TeamBonusRewardsInitialData
    {
        public static TeamBonusRewardEntity[] TeamBonusRewardSeeds = new TeamBonusRewardEntity[]
        {
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.FirstLevel], CommonTurnover = 0, Percent = 10},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.SecondLevel], CommonTurnover = 2100, Percent = 12.5},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.ThirdLevel], CommonTurnover = 9000, Percent = 15},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.FourthLevel], CommonTurnover = 30000, Percent = 17},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.FifthLevel], CommonTurnover = 61000, Percent = 19},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.SixthLevel], CommonTurnover = 125000, Percent = 20.5},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.SeventhLevel], CommonTurnover = 300000, Percent = 22},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.EighthLevel], CommonTurnover = 700000, Percent = 23},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.NinthLevel], CommonTurnover = 1500000, Percent = 24},
            new TeamBonusRewardEntity {MonthlyLevelId = MonthlyLevelsEnitialData.MonthlyLevelIdsByType[Level.TenthLevel], CommonTurnover = 3000000, Percent = 25}
        };
    }
}
