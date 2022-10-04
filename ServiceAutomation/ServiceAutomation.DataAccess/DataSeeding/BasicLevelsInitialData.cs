using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ServiceAutomation.DataAccess.Migrations")]

namespace ServiceAutomation.DataAccess.DataSeeding
{
    public static class BasicLevelsInitialData
    {
        internal static IDictionary<Level, Guid> BasicLevelIdsByType = new Dictionary<Level, Guid>
        {
            { Level.FirstLevel , new Guid( "6af8926e-e8fe-4555-946e-87529bed9702")},
            { Level.SecondLevel , new Guid("6fd6510f-47a9-425a-abb3-a74ad2b19765")},
            { Level.ThirdLevel , new Guid("fc7cd396-3583-47b5-b1e1-67940060d5f7")},
            { Level.FourthLevel , new Guid( "0e307b2d-9861-4e64-aceb-d9503963a964")},
            { Level.FifthLevel , new Guid("6b6579c0-390e-482c-84b3-b0c952113ff2")},
            { Level.SixthLevel , new Guid("4249d477-5ea6-4d59-8346-71b05052b512")},
            { Level.SeventhLevel, new Guid("ca855801-94e7-400f-a35a-a058d3208642")},
            { Level.EighthLevel, new Guid("f0affe24-1c8c-4ea2-a112-9794c82c2767")},
            { Level.NinthLevel, new Guid("62b0371b-cac7-45f6-87b0-9e6dfd0fe7f9")},
            { Level.TenthLevel, new Guid("c302e9be-fd45-42f9-8c82-97571bdd7f15")}
        };

        public static BasicLevelEntity[] BasicLevelSeeds = new BasicLevelEntity[]
        {
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.FirstLevel], Name="1 уровень", Level = Level.FirstLevel, Turnover = null, PartnersCount = null, PartnersLevelId = null },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.SecondLevel], Name="2 уровень", Level = Level.SecondLevel, Turnover = 2100, PartnersCount = null, PartnersLevel = null },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.ThirdLevel], Name="3 уровень", Level = Level.ThirdLevel, Turnover = 9000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.SecondLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.FourthLevel], Name="4 уровень", Level = Level.FourthLevel, Turnover = 30000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.ThirdLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.FifthLevel], Name="5 уровень", Level = Level.FifthLevel, Turnover =  61000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.FourthLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.SixthLevel], Name="6 уровень", Level = Level.SixthLevel, Turnover = 125000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.FifthLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.SeventhLevel], Name="7 уровень", Level = Level.SeventhLevel, Turnover = 300000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.SixthLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.EighthLevel], Name="8 уровень", Level = Level.EighthLevel, Turnover = 700000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.SeventhLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.NinthLevel], Name="9 уровень", Level = Level.NinthLevel, Turnover = 1500000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.EighthLevel] },
             new BasicLevelEntity {Id = BasicLevelIdsByType[Level.TenthLevel], Name="10 уровень", Level = Level.TenthLevel, Turnover = 3000000, PartnersCount = 3, PartnersLevelId = BasicLevelIdsByType[Level.NinthLevel] }
        };
    }
}
