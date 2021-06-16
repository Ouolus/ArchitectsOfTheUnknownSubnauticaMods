using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class CrimsonRayEgg : EggPrefab
    {
        public CrimsonRayEgg()
            : base("CrimsonRayEgg", "Crimson Ray Egg", "Crimson Rays hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        public override GameObject Model => LoadGameObject("CrimsonRayEgg.prefab");
        public override TechType HatchingCreature => TechType.GhostRayRed;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("CrimsonRayEgg");
        public override Vector2int SizeInInventory { get; } = new(2, 2);

    }
}