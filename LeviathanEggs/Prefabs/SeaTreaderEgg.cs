using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class SeaTreaderEgg : EggPrefab
    {
        public SeaTreaderEgg()
            : base("SeaTreaderEgg", "Sea Treader Egg", "Sea Treaders hatch from these.")
        {
            LateEnhancements += InitializeObject;
        }

        public override GameObject Model => LoadGameObject("SeaTreaderEgg.prefab");
        public override TechType HatchingCreature => TechType.SeaTreader;
        public override float HatchingTime => 5f;
        public override Sprite ItemSprite => LoadSprite("SeaTreaderEgg");
        public override Vector2int SizeInInventory { get; } = new(3, 3);

        public void InitializeObject(GameObject prefab) => prefab.AddComponent<SpawnLocations>();
    }
}