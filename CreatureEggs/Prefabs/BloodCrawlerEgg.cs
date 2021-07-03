using System.Collections.Generic;
using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace CreatureEggs.Prefabs
{
    public class BloodCrawlerEgg : EggPrefab
    {
        public BloodCrawlerEgg()
            : base("BloodCrawlerEgg", "Blood Crawler Egg", "Blood Crawlers hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("BloodCrawlerEgg.prefab");
        
        public override TechType HatchingCreature => TechType.Shuttlebug;

        public override List<LootDistributionData.BiomeData> BiomesToSpawnIn { get; } = new()
        {
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.BloodKelp_CaveFloor,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.BloodKelp_ShockerEggs,
                probability = 0.01f
            },
            new LootDistributionData.BiomeData
            {
                count = 1,
                biome = BiomeType.BloodKelp_TrenchFloor,
                probability = 0.01f
            }
        };

        public override float HatchingTime => 2f;
        
        public override Sprite ItemSprite => LoadSprite("BloodCrawlerEgg");

        public override Vector2int SizeInInventory => new(2, 2);
    }
}