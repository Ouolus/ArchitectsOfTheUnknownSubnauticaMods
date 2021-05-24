using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    class DrillableMorganite : ReskinSpawnable
    {
        protected override string ReferenceClassId => "b3db72b6-f0cf-4234-be74-d98bd4c49797";

        public DrillableMorganite() : base("DrillableMorganite", "Morganite", "Be₃Al₂SiO₆. Rare mineral with applications in advanced alien fabrication.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<Light>().color = new Color(1f, 0f, 1f);
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.MorganiteTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.MorganiteTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 3;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Morganite_diffuse"));
                renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Morganite_illum"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("Morganite_spec"));
                renderer.material.SetColor("_GlowColor", new Color(3f, 0f, 3f));
                renderer.material.SetColor("_SpecColor", new Color(2f, 1.5f, 2f));
                renderer.material.SetFloat("_SpecInt", 2f);
                renderer.material.SetFloat("_Shininess", 7f);
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
