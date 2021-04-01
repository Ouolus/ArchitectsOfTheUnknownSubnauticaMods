using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class GhostRayRedEgg : EggPrefab
    {
        public GhostRayRedEgg()
            : base("GhostRayRedEgg", "Crimson Ray Egg", "Crimson Rays hatch from these")
        {
            LateEnhancements += InitializeObject;
        }
        public override GameObject Model => LoadGameObject("CrimsonRayEgg.prefab");
        public override TechType HatchingCreature => TechType.GhostRayRed;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("RobotEgg");

        public void InitializeObject(GameObject prefab) => prefab.AddComponent<SpawnLocations>();

    }
}