﻿namespace ArchitectsLibrary.Items.Minerals
{
    using Handlers;
    using System.Collections.Generic;
    using UnityEngine;
    using UWE;
    using API;
    
    class Sapphire : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "87293f19-cca3-46e6-bb3d-6e8dc579e27b";

        public Sapphire() : base("Sapphire", LanguageSystem.Get("Sapphire"), LanguageSystem.GetTooltip("Sapphire"))
        {
            OnFinishedPatching += () =>
            {
                AUHandler.SapphireTechType = TechType;
                CraftData.pickupSoundList.Add(TechType, Main.ionCubePickupSound);
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_spec"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_illum"));
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("Sapphire_icon"));
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Near,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Small,
            techType = TechType
        };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new()
        {
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_Ground,
                count = 1,
                probability = 0.65f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.GrandReef_WhiteCoral,
                count = 1,
                probability = 0.3f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.DeepGrandReef_Ceiling,
                count = 1,
                probability = 0.25f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.DeepGrandReef_Ground,
                count = 1,
                probability = 0.6f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_Sand,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.MushroomForest_RockWall,
                count = 1,
                probability = 0.3f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandSides,
                count = 1,
                probability = 0.5f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandTop,
                count = 1,
                probability = 0.5f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 0.13f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.KooshZone_RockWall,
                count = 1,
                probability = 0.3f
            },
        };
    }
}
