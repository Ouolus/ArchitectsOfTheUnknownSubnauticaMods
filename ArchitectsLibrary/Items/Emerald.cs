using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
using UWE;

namespace ArchitectsLibrary.Items
{
    class Emerald : Spawnable
    {
        GameObject cachedPrefab;
        Atlas.Sprite sprite;
        const string kyaniteClassId = "6e7f3d62-7e76-4415-af64-5dcd88fc3fe4";

        public Emerald() : base("Emerald", "Emerald", "Be₃Al₂SiO₆. Rare mineral with applications in advanced alien fabrication.")
        {
        }

#if SN1
        public override GameObject GetGameObject()
        {
            if (cachedPrefab == null)
            {
                PrefabDatabase.TryGetPrefab(kyaniteClassId, out GameObject prefab);
                cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                ApplyChangesToPrefab(cachedPrefab);
            }
            return cachedPrefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (cachedPrefab == null)
            {
                IPrefabRequest request = PrefabDatabase.GetPrefabAsync(kyaniteClassId);
                yield return request;
                request.TryGetPrefab(out GameObject prefab);
                GameObject cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                ApplyChangesToPrefab(cachedPrefab);
            }
            gameObject.Set(cachedPrefab);
        }
#endif

        void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<TechTag>().type = TechType;
            prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.EnsureComponent<ResourceTracker>().overrideTechType = TechType;
            var renderer = prefab.GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", Main.assetBundle.LoadAsset<Texture2D>("Emerald_Diffuse"));
            renderer.material.SetTexture("_Illum", Main.assetBundle.LoadAsset<Texture2D>("Emerald_Illum"));
            renderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
            renderer.material.SetColor("_SpecColor", new Color(1f, 2f, 1.2f));
            renderer.material.SetFloat("_Fresnel", 0.6f);
            renderer.material.SetFloat("_SpecInt", 20f);
            renderer.material.SetFloat("_GlowStrength", 1f);
            renderer.material.SetFloat("_GlowStrengthNight", 1f);
            renderer.transform.localScale = Vector3.one * 0.66f;
            ApplyTranslucency(renderer);
        }

        private void ApplyTranslucency(Renderer renderer)
        {
            renderer.material.EnableKeyword("_ZWRITE_ON");
            renderer.material.EnableKeyword("WBOIT");
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.SetInt("_Cutoff", 0);
            renderer.material.SetFloat("_SrcBlend", 1f);
            renderer.material.SetFloat("_DstBlend", 1f);
            renderer.material.SetFloat("_SrcBlend2", 0f);
            renderer.material.SetFloat("_DstBlend2", 10f);
            renderer.material.SetFloat("_AddSrcBlend", 1f);
            renderer.material.SetFloat("_AddDstBlend", 1f);
            renderer.material.SetFloat("_AddSrcBlend2", 0f);
            renderer.material.SetFloat("_AddDstBlend2", 10f);
            renderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack | MaterialGlobalIlluminationFlags.RealtimeEmissive;
            renderer.material.renderQueue = 3101;
            renderer.material.enableInstancing = true;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            if (sprite == null)
            {
                sprite = ImageUtils.LoadSpriteFromTexture(Main.assetBundle.LoadAsset<Texture2D>("Emerald_Icon"));
            }
            return sprite;
        }

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn => new List<LootDistributionData.BiomeData>()
        {
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GhostTree_Ground,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.GhostTree_Wall,
                count = 1,
                probability = 0.1f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_Ground,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Corridor_Ground,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Corridor_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.BonesField_Ground,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverJunction_Ground,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverJunction_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_Ground,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.LostRiverCorridor_Wall,
                count = 1,
                probability = 0.075f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandTop,
                count = 1,
                probability = 0.15f
            },
            new LootDistributionData.BiomeData()
            {
                biome = BiomeType.UnderwaterIslands_IslandSides,
                count = 1,
                probability = 0.15f
            }
        };

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            cellLevel = LargeWorldEntity.CellLevel.Near,
            classId = ClassID,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Small,
            techType = TechType
        };
    }
}
