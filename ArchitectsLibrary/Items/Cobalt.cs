using System.Collections.Generic;
using SMLHelper.V2.Utility;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items
{
    class Cobalt : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "7815b1b7-2830-418b-9b5d-19949b0ae9ec";

        public Cobalt() : base("Cobalt", "Cobalt", "Cobalt that makes Metious go yes.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltOre_Diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltOre_Spec"));
            renderer.material.SetFloat("_SpecInt", 1.5f);
            renderer.material.SetFloat("_Fresnel", 0.26f);
            renderer.material.SetFloat("_Shininess", 6f);
            renderer.material.SetColor("_SpecColor", new Color(3f, 2f, 1f));
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("CobaltOre_Icon"));
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
            
        };
    }
}
