using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    internal class DrillableBismuth : ReskinSpawnable
    {
        protected override string ReferenceClassId => "1efa1a20-3a39-4f56-ace0-154211d6af12";

        public DrillableBismuth() : base("DrillableBismuth", "Bismuth", "Drillable bismuth that makes Metious go yes.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.GetComponentInChildren<Light>().color = Color.green; 
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.BismuthTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.BismuthTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 2;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                
            }
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Medium,
            techType = TechType
        };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new()
        {
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Mountains_Rock,
                count = 1,
                probability = 0.03f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Mountains_Sand,
                count = 1,
                probability = 0.01f
            }
        };
    }
}
