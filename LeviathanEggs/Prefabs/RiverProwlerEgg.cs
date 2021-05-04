using LeviathanEggs.MonoBehaviours;
using ArchitectsLibrary.API;
using static LeviathanEggs.Helpers.AssetsBundleHelper;
using UnityEngine;

namespace LeviathanEggs.Prefabs
{
    public class RiverProwlerEgg : EggPrefab
    {
        public RiverProwlerEgg()
            : base("RiverProwlerEgg", "River Prowler Egg", "River Prowlers hatch from these.")
        {}
        public override GameObject Model => LoadGameObject("RiverProwlerEgg.prefab");
        public override TechType HatchingCreature => TechType.SpineEel;
        public override float HatchingTime => 2f;
        public override Sprite ItemSprite => LoadSprite("RiverProwlerEgg");
    }
}