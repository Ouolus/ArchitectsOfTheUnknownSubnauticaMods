using System.Collections.Generic;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace CreatureEggs.Prefabs
{
    public class RiverProwlerEgg : EggPrefab
    {
        public RiverProwlerEgg()
            : base("RiverProwlerEgg", "River Prowler Egg", "River Prowlers hatch from these.")
        {
            LateEnhancements += LateEnhance;
        }
        
        public override GameObject Model => LoadGameObject("RiverProwlerEgg.prefab");
        
        public override TechType HatchingCreature => TechType.SpineEel;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverJunction_Ground,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverCorridor_Ground,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.LostRiverCorridor_LakeFloor,
                probability = 0.01f
            }
        };

        public override float HatchingTime => 2f;
        
        public override Sprite ItemSprite => LoadSprite("ProwlerEgg");
        
        void LateEnhance(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                var material = renderer.material;
                if (material.name.Contains("Transparent"))
                {
                    material.SetFloat("_Shininess", 2f);
                    material.SetFloat("_SpecInt", 8f);
                }
                else if (renderer.name.Contains("C_low"))
                {
                    material.SetFloat("_EmissionLMNight", .15f);
                }
            }
            prefab.AddComponent<SpawnLocations>();
        }

        public override Vector2int SizeInInventory => new(2, 2);
    }
}
