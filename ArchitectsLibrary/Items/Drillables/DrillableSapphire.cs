namespace ArchitectsLibrary.Items.Drillables
{
    using UnityEngine;
    using API;
    using Handlers;
    using System.Collections.Generic;
    using UWE;
    
    class DrillableSapphire : ReskinSpawnable
    {
        protected override string ReferenceClassId => "109bbd29-c445-4ad8-a4bf-be7bc6d421d6";

        public DrillableSapphire() : base("DrillableSapphire", LanguageSystem.Get("DrillableSapphire"), LanguageSystem.GetTooltip("DrillableSapphire"))
        {
            OnFinishedPatching += () =>
            {
                AUHandler.DrillableSapphireTechType = TechType;
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<Light>().color = Color.blue; 
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.SapphireTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.SapphireTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 4;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_diffuse"));
                renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_illum"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Sapphire_spec"));
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
            new()
            {
                biome = BiomeType.GrandReef_Ground,
                count = 1,
                probability = 0.08f
            },
            new()
            {
                biome = BiomeType.GrandReef_CaveCeiling,
                count = 1,
                probability = 0.08f
            },
            new()
            {
                biome = BiomeType.GrandReef_Grass,
                count = 1,
                probability = 0.08f
            },
            new()
            {
                biome = BiomeType.DeepGrandReef_Ground,
                count = 1,
                probability = 0.08f
            },
            new()
            {
                biome = BiomeType.KooshZone_Sand,
                count = 1,
                probability = 0.04f
            },
            new()
            {
                biome = BiomeType.KooshZone_CaveFloor,
                count = 1,
                probability = 0.25f
            },
        };
    }
}
