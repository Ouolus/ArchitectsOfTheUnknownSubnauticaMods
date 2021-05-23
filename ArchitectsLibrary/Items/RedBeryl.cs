using System.Collections.Generic;
using SMLHelper.V2.Utility;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items
{
    class RedBeryl : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "3b52098a-4b58-467c-a29a-1d1b6d92ec3e";

        public RedBeryl() : base("RedBeryl", "Red Beryl", "Be₃Al₂Si₆O₁₈. Very rare mineral with applications in advanced alien fabrication.")
        {

        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_spec"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_illum"));
            renderer.material.SetFloat("_SpecInt", 10f);
            renderer.material.SetFloat("_GlowStrength", 2f);
            renderer.material.SetFloat("_GlowStrengthNight", 2f);
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("RedBeryl_icon"));
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
                biome = BiomeType.InactiveLavaZone_Chamber_Floor,
                count = 1,
                probability = 0.09f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_MagmaTree,
                count = 1,
                probability = 0.9f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Ceiling,
                count = 1,
                probability = 0.9f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_MagmaBubble,
                count = 1,
                probability = 1.65f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Wall,
                count = 1,
                probability = 0.19f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Floor,
                count = 1,
                probability = 0.19f
            }
        };
    }
}
