using CreatureEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static CreatureEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace CreatureEggs.Prefabs
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
        
        public override Vector2int SizeInInventory { get; } = new(2, 2);
    }
}