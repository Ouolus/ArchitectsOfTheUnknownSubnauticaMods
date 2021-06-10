using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    class DrillableCobalt : ReskinSpawnable
    {
        protected override string ReferenceClassId => "9c8f56e6-3380-42e4-a758-e8d733b5ddec";

        public DrillableCobalt() : base("DrillableCobalt", "Cobalt", "Cobalt drillable that makes Metious go yes.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<Light>().color = new Color(1f, 0f, 1f);
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = AUHandler.RedBerylTechType;
            var drillable = prefab.GetComponent<Drillable>();
            drillable.resources[0] = new Drillable.ResourceType() { chance = 1f, techType = AUHandler.CobaltTechType };
            drillable.kChanceToSpawnResources = 1f;
            drillable.maxResourcesToSpawn = 2;
            Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltOre_Diffuse"));
                renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltOre_Spec"));
                renderer.material.SetFloat("_SpecInt", 3f);
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
            
        };
    }
}
