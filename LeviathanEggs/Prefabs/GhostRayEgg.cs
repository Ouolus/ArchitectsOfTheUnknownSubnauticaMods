using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class GhostRayEgg : EggPrefab
    {
        public GhostRayEgg()
            : base("GhostRayEgg", "Ghost Ray Egg", "Ghost Rays hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("GhostRayEgg.prefab");
        public override TechType HatchingCreature => TechType.GhostRayBlue;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("GhostRayEgg");
        public override Vector2int SizeInInventory { get; } = new(2, 2);
    }
}