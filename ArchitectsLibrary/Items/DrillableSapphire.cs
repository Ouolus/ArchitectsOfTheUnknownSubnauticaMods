using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    class DrillableSapphire : ReskinSpawnable
    {
        protected override string ReferenceClassId => "109bbd29-c445-4ad8-a4bf-be7bc6d421d6";

        public DrillableSapphire() : base("DrillableSapphire", "Sapphire", "Al₂O₃. Valuable insulative properties and applications in glass reinforcement.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<Light>().color = Color.blue; 
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.SapphireTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.SapphireTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 3;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
        }

        public override WorldEntityInfo EntityInfo => new()
        {
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Medium,
            techType = TechType
        };

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GrandReef_Ground,
                count = 1,
                probability = 0.07f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.DeepGrandReef_Ground,
                count = 1,
                probability = 0.08f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 0.04f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.KooshZone_CaveFloor,
                count = 1,
                probability = 0.25f
            },
        };
    }
}
