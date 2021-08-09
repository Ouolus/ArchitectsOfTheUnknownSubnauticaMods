using ArchitectsLibrary.API;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace RotA.Prefabs.Placeable
{
    class GargantuanAdultToy : ALPlaceable
    {
        public GargantuanAdultToy()
            : base("GargantuanAdultToy", "Gargantuan Adult Toy", "Gargantuan Adult Toy that makes me go yes.")
        {}


        public override PlacementFlags GetPlacementMode => PlacementFlags.Inside | PlacementFlags.AllowedOnRigidbody | PlacementFlags.Ground;

        protected override TechData GetBlueprintRecipe() => new()
        {
            craftAmount = 1,
            Ingredients =
            {
                new(TechType.Quartz, 2),
                new(TechType.Titanium, 1),
                new(TechType.CreepvineSeedCluster, 1)
            }
        };

        protected override Atlas.Sprite GetItemSprite() => SpriteManager.defaultSprite;

        public override GameObject GetModel() => Mod.gargAssetBundle.LoadAsset<GameObject>("GargantuanAdultToy");

    }
}