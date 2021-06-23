namespace ArchitectsLibrary.Items
{
    using UnityEngine;
    using API;
    using Handlers;
    using System.Collections.Generic;
    using UWE;
    
    class DrillableCobalt : ReskinSpawnable
    {
        protected override string ReferenceClassId => "a05fe1c9-ae0d-43db-a12c-865992808cb2";

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
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltGold_Diffuse"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltGold_Spec"));
                renderer.material.SetFloat("_SpecInt", 1.5f);
                renderer.material.SetFloat("_Fresnel", 0.26f);
                renderer.material.SetFloat("_Shininess", 6f);
                renderer.material.SetColor("_SpecColor", new Color(3f, 2f, 1f));
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
