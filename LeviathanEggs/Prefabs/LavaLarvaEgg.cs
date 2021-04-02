using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class LavaLarvaEgg : EggPrefab
    {
        public LavaLarvaEgg()
            : base("LavaLarvaEgg", "LavaLarva Egg", "LavaLarva hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }
        public override GameObject Model => LoadGameObject("LavaLarvaEgg.prefab");
        public override TechType HatchingCreature => TechType.LavaLarva;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("LavaLarvaEgg");
        public override Vector2int SizeInInventory { get; } = new(1, 1);

        public void InitializeObject(GameObject prefab) => prefab.AddComponent<SpawnLocations>();
    }
}