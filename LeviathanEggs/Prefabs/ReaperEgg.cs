using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;


namespace LeviathanEggs.Prefabs
{
    public class ReaperEgg : EggPrefab
    {
        public ReaperEgg()
            : base("ReaperEgg", "Reaper Leviathan Egg", "Reapers hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }

        public override GameObject Model => LoadGameObject("ReaperEgg");
        public override TechType HatchingCreature => TechType.ReaperLeviathan;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("RobotEgg");

        public void InitializeObject(GameObject prefab) => prefab.AddComponent<SpawnLocations>();
    }
}