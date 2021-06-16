using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class WarperEgg : EggPrefab
    {
        public WarperEgg()
            : base("WarperEgg", "Warper Egg", "Warpers are made with these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("WarperEgg.prefab");
        public override TechType HatchingCreature => TechType.Warper;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("WarperEgg");
    }
}