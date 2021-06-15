namespace ArchitectsLibrary.Items
{
    using UnityEngine;
    using API;
    using Handlers;
    using System.Collections.Generic;
    using UWE;
    
    class DrillableCobalt : ReskinSpawnable
    {
        protected override string ReferenceClassId => "601ee500-1744-4697-8279-59ef35160edb";

        public DrillableCobalt() : base("DrillableCobalt", "Cobalt", "Co. Applications in magnetic, high-strength alloy fabrication.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.CobaltTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.CobaltTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 2;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltCopper_Diffuse"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltCopper_Spec"));
                renderer.material.SetFloat("_SpecInt", 5f);
                renderer.material.SetFloat("_Fresnel", 0.53f);
                renderer.material.SetFloat("_Shininess", 7f);
                renderer.material.SetColor("_SpecColor", new Color(5, 1f, 1f));
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
                biome = BiomeType.Mountains_Rock,
                count = 1,
                probability = 0.04f
            },
            new LootDistributionData.BiomeData
            {
                biome = BiomeType.Mountains_Sand,
                count = 1,
                probability = 0.05f
            },
            new()
            {
                biome = BiomeType.SparseReef_Wall,
                count = 1,
                probability = 0.05f
            },
            new()
            {
                biome = BiomeType.SparseReef_Sand,
                count = 1,
                probability = 0.05f
            },
            new()
            {
                biome = BiomeType.SparseReef_Spike,
                count = 1,
                probability = 0.04f
            }
        };
    }
}
