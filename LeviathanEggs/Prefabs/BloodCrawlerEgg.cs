using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
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
        
        public override float HatchingTime => 2f;
        
        public override Sprite ItemSprite => LoadSprite("BloodCrawlerEgg");

        public override Vector2int SizeInInventory => new(2, 2);
    }
}