﻿namespace ArchitectsLibrary.Items.Minerals
{
    using System.Collections.Generic;
    using API;
    using UnityEngine;
    using UWE;
    using Handlers;
    class Cobalt : ReskinSpawnable
    {
        Atlas.Sprite sprite;
        protected override string ReferenceClassId => "3c5bd4db-953d-4d23-92be-f5a3b76b2e25";

        public Cobalt() : base("Cobalt", LanguageSystem.Get("Cobalt"), LanguageSystem.GetTooltip("Cobalt"))
        {
            OnFinishedPatching += () =>
            {
                AUHandler.CobaltTechType = TechType;
                CraftData.pickupSoundList.Add(TechType, Main.ionCubePickupSound);
            };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltGold_Diffuse"));
            renderer.material.SetTexture("_SpecTex", Main.assetBundle.LoadAsset<Texture2D>("CobaltGold_Spec"));
            renderer.material.SetFloat("_SpecInt", 3f);
            renderer.material.SetFloat("_Fresnel", 0.26f);
            renderer.material.SetFloat("_Shininess", 6f);
            renderer.material.SetColor("_SpecColor", new Color(2f, 1f, 1f));
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
            new()
            {
                biome = BiomeType.CragField_Ground,
                count = 1,
                probability = 0.12f
            },
            new()
            {
                biome = BiomeType.CragField_Rock,
                count = 1,
                probability = 0.14f
            },
            new()
            {
                biome = BiomeType.CragField_Sand,
                count = 1,
                probability = 0.05f
            },
            new()
            {
                biome = BiomeType.SparseReef_Wall,
                count = 1,
                probability = 0.02f
            },
            new()
            {
                biome = BiomeType.SparseReef_Spike,
                count = 1,
                probability = 0.2f
            }
        };
    }
}
