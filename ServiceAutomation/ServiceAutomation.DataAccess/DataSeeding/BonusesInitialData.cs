using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    public static class BonusesInitialData
    {
        internal static IDictionary<BonusType, Guid> BonusesIdsByType = new Dictionary<BonusType, Guid>
        {
            { BonusType.TeamBonus , new Guid("fe990ab5-4279-7f82-d24e-582177512933")},
            { BonusType.DynamicBonus , new Guid("415fee4f-accd-87f8-7bdf-d05a2c5c145d")},
            { BonusType.TravelBonus , new Guid("613df55a-fba7-1d3e-f74f-1b02ed4b2bab")},
            { BonusType.LevelBonus , new Guid("511485d7-7a50-482f-ddb0-dd2b34aeacef")},
            { BonusType.AutoBonus , new Guid("00f35f8c-603d-13fa-3f23-49149d37f9be")},
            { BonusType.BunBonus , new Guid("556ecfc1-f1ae-feba-82d7-d91088cd8de2")},
            { BonusType.BonusOverall, new Guid("306968a4-46f4-c5a8-ee39-39a77fd71002")},
            { BonusType.StartBonus, new Guid("f7d833f0-3fd2-96a7-b11e-a1d849f5e3af")}
        };

        public static BonusEntity[] BonusSeeds = new BonusEntity[]
        {
            new BonusEntity {Id = BonusesIdsByType[BonusType.TeamBonus], Name="Team Bonus", Type = BonusType.TeamBonus, DisplayOrder = 1 },            
            new BonusEntity {Id = BonusesIdsByType[BonusType.LevelBonus], Name="Level Bonus", Type = BonusType.LevelBonus, DisplayOrder = 2 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.StartBonus], Name="Start Bonus", Type = BonusType.StartBonus, DisplayOrder = 3 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.DynamicBonus], Name="Dynamic Bonus", Type = BonusType.DynamicBonus, DisplayOrder = 4 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.AutoBonus], Name="Auto Bonus", Type = BonusType.AutoBonus, DisplayOrder = 5 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.TravelBonus], Name="Travel Bonus", Type = BonusType.TravelBonus, DisplayOrder = 6 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.BunBonus], Name="Bun Bonus", Type = BonusType.BunBonus, DisplayOrder = 7 },
            new BonusEntity {Id = BonusesIdsByType[BonusType.BonusOverall], Name="Bonus Overall", Type = BonusType.BonusOverall, DisplayOrder = 8 },
            
        };
    }
}
