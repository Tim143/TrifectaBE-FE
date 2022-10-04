using ServiceAutomation.DataAccess.Models.EntityModels;
using ServiceAutomation.DataAccess.Schemas.Enums;
using System;
using System.Collections.Generic;

namespace ServiceAutomation.DataAccess.DataSeeding
{
    public static class PackagesInitialData
    {
        internal static IDictionary<PackageType, Guid> PackagesIdsByType = new Dictionary<PackageType, Guid> {
            { PackageType.Start, new Guid("00f1967f-a1f4-706d-7e57-453d605c4747") },
            { PackageType.Classic,  new Guid("3decbb76-bbc7-e035-e53c-ea10a3d54b16") },
            { PackageType.Premium, new Guid("0ff93d94-077f-ea49-34f0-3214704f5dbf") }
        };

        public static PackageEntity[] PackageSeeds = new PackageEntity[]
        {
            new PackageEntity {Id = PackagesIdsByType[PackageType.Start], Type = PackageType.Start, Name = "Start", Price = 199, DisplayOrder = 1 },
            new PackageEntity {Id = PackagesIdsByType[PackageType.Classic], Type = PackageType.Classic, Name = "Classic", Price = 999, DisplayOrder = 2 },
            new PackageEntity {Id = PackagesIdsByType[PackageType.Premium], Type = PackageType.Premium, Name = "Premium", Price = 1999, DisplayOrder = 3 }
        };

        public static PackageBonusAssociationEntity[] PackageBonusAssociationSeeds = new PackageBonusAssociationEntity[]
        {
            new PackageBonusAssociationEntity { Id= new Guid("6971a3aa-a58d-81bf-8173-c7784e1bfc54"), PackageId = PackagesIdsByType[PackageType.Start], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TeamBonus], FromLevel = 1},
            new PackageBonusAssociationEntity { Id= new Guid("b7de76fc-aeab-00c1-bfe0-25812ec21a9f"), PackageId = PackagesIdsByType[PackageType.Start], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.DynamicBonus], FromLevel = 1},
            new PackageBonusAssociationEntity { Id= new Guid("b2dadece-dd97-755a-0a13-d27791627f3b"), PackageId = PackagesIdsByType[PackageType.Start], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TravelBonus], FromLevel = 1},
            new PackageBonusAssociationEntity { Id= new Guid("0a887ad2-8f03-ba8e-618a-42e44366c0d8"), PackageId = PackagesIdsByType[PackageType.Start], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.StartBonus], FromLevel = 1},

            new PackageBonusAssociationEntity { Id= new Guid("ae024a8b-b76a-4165-84b0-d85ccae29393"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TeamBonus], FromLevel = 1},
            new PackageBonusAssociationEntity { Id= new Guid("caa03805-f0a6-425d-695e-1aaa6ad4676d"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.LevelBonus], FromLevel = 1, PayablePercent = 50},
            new PackageBonusAssociationEntity { Id= new Guid("95032936-3175-725c-ea27-dadd33c4764b"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.DynamicBonus], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("fce1f8db-bff1-afd7-5e4a-eba2262be705"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.AutoBonus], FromLevel = 7 },
            new PackageBonusAssociationEntity { Id= new Guid("7dcaab0e-8097-29c4-45b5-7cf6d0a68f8a"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TravelBonus], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("d0c1790f-a7a8-6e70-a816-3c2b4ba160dd"), PackageId = PackagesIdsByType[PackageType.Classic], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.StartBonus], FromLevel = 1 },

            new PackageBonusAssociationEntity { Id= new Guid("96a15515-b17d-6396-4175-298cc57d5d6b"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TeamBonus], FromLevel = 1},
            new PackageBonusAssociationEntity { Id= new Guid("0ba69e78-4e3c-036f-2163-2175fd53ff05"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.LevelBonus], FromLevel = 1, PayablePercent = 100},
            new PackageBonusAssociationEntity { Id= new Guid("f35c1cb9-4227-218f-1f2e-0f6bba4b9dca"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.DynamicBonus], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("93dad289-299a-2db0-839e-89d48656ea9b"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.AutoBonus], FromLevel = 5 },
            new PackageBonusAssociationEntity { Id= new Guid("35d8317e-b382-cafa-205a-0cdc2c14df3f"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.TravelBonus], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("e13915f2-d294-d303-d977-1026c31acd04"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.BunBonus], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("d253614d-1367-cf2b-1b4b-55695c0a3c46"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.BonusOverall], FromLevel = 1 },
            new PackageBonusAssociationEntity { Id= new Guid("110d8697-1997-4f15-f944-7a00ee8cca4d"), PackageId = PackagesIdsByType[PackageType.Premium], BonusId= BonusesInitialData.BonusesIdsByType[BonusType.StartBonus], FromLevel = 1 }
        };

    }
}
