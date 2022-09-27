using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    public static class MonthlyLevelsEnitialData
    {
        internal static IDictionary<Level, Guid> MonthlyLevelIdsByType = new Dictionary<Level, Guid>
        {
            { Level.FirstLevel, new Guid("3d0c7240-1b5a-493e-8288-6a347d06903a")},
            { Level.SecondLevel, new Guid("02142b58-bba1-4ba2-91ce-bcd1414489f0")},
            { Level.ThirdLevel, new Guid("db5b982e-2abc-46bc-bfc1-8c326957c346")},
            { Level.FourthLevel, new Guid("5c426acc-9fd0-4949-8981-1d728891723a")},
            { Level.FifthLevel, new Guid("e0c6f6e8-10f2-4f2a-9561-078421696d99")},
            { Level.SixthLevel, new Guid("d8e52ae8-cf15-48c4-ac42-1a9cd420ec21")},
            { Level.SeventhLevel, new Guid("2cfafa29-2513-4b4d-81b3-bd83be3de6ca")},
            { Level.EighthLevel, new Guid("2056618d-ad11-4ef9-8805-bd4d18074856")},
            { Level.NinthLevel, new Guid("62697dc5-154d-4fe4-906a-3989b2fe5d7f")},
            { Level.TenthLevel, new Guid("d3d4cae1-dc63-4ccf-8921-a93e33c106c3")}
        };

        public static MonthlyLevelEntity[] MonthlyLevelSeeds = new MonthlyLevelEntity[]
        {
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.FirstLevel], Name="1 уровень", Level = Level.FirstLevel, Turnover = null },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.SecondLevel], Name="2 уровень", Level = Level.SecondLevel, Turnover = null },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.ThirdLevel], Name="3 уровень", Level = Level.ThirdLevel, Turnover = 4000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.FourthLevel], Name="4 уровень", Level = Level.FourthLevel, Turnover = 8100 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.FifthLevel], Name="5 уровень", Level = Level.FifthLevel, Turnover =  20000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.SixthLevel], Name="6 уровень", Level = Level.SixthLevel, Turnover = 41000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.SeventhLevel], Name="7 уровень", Level = Level.SeventhLevel, Turnover = 81000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.EighthLevel], Name="8 уровень", Level = Level.EighthLevel, Turnover = 165000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.NinthLevel], Name="9 уровень", Level = Level.NinthLevel, Turnover = 330000 },
             new MonthlyLevelEntity {Id = MonthlyLevelIdsByType[Level.TenthLevel], Name="10 уровень", Level = Level.TenthLevel, Turnover = 670000 }
        };
    }
}

