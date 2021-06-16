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
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }

        public override GameObject Model => LoadGameObject("ReaperEgg.prefab");
        public override TechType HatchingCreature => TechType.ReaperLeviathan;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("ReaperEgg");
        public override Vector2int SizeInInventory { get; } = new(3, 3);
    }
}