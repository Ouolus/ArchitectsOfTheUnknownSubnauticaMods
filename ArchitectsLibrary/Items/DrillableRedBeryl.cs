using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    class DrillableRedBeryl : ReskinSpawnable
    {
        protected override string ReferenceClassId => "fb5de2b6-1fe8-44fc-a555-dc0a09dc292a";

        public DrillableRedBeryl() : base("DrillableRedBeryl", "Red Beryl", "Be₃Al₂Si₆O₁₈. Very rare mineral with applications in advanced alien fabrication.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<Light>().color = new Color(1f, 0f, 0f);
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.RedBerylTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.RedBerylTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 2;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_diffuse"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_spec"));
                renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("RedBeryl_illum"));
                renderer.material.SetFloat("_SpecInt", 10f);
                renderer.material.SetFloat("_GlowStrength", 2f);
                renderer.material.SetFloat("_GlowStrengthNight", 2f);
            }
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
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
                probability = 0.04f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_MagmaTree,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_Ceiling,
                count = 1,
                probability = 0.4f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Chamber_MagmaBubble,
                count = 1,
                probability = 1f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Wall,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.InactiveLavaZone_Corridor_Floor,
                count = 1,
                probability = 0.1f
            },
        };
    }
}
