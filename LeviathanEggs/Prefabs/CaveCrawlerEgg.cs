using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class CaveCrawlerEgg : EggPrefab
    {
        public CaveCrawlerEgg()
            : base("CaveCrawlerEgg", "Cave Crawler Egg", "Cave Crawlers hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("CaveCrawlerEgg.prefab");
        public override TechType HatchingCreature => TechType.CaveCrawler;
        public override float HatchingTime => 2f;
        public override Sprite ItemSprite => LoadSprite("CaveCrawlerEgg");
    }
}