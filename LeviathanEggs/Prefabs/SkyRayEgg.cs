using ArchitectsLibrary.API;
using LeviathanEggs.MonoBehaviours;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class SkyRayEgg : EggPrefab
    {
        public SkyRayEgg()
            : base("SkyRayEgg", "Skyray Egg", "Skyrays Hatch from these.")
        {
            LateEnhancements += prefab => prefab.AddComponent<SpawnLocations>();
        }
        
        public override GameObject Model => LoadGameObject("SkyRayEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaTreader;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("SkyRayEgg");
        public override Vector2int SizeInInventory { get; } = new(1, 1);
        public override bool MakeCreatureLayEggs { get; } = true;
    }
}