using ArchitectsLibrary.API;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace RotA.Prefabs.Placeable
{
    class GargantuanJuvenileToy : ALPlaceable
    {
        public GargantuanJuvenileToy()
            : base("GargantuanJuvenileToy", "Gargantuan Juvenile Toy", "Gargantuan Juvenile Toy that makes me go yes.")
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

        public override GameObject GetModel() => Mod.gargAssetBundle.LoadAsset<GameObject>("GargantuanJuvenileToy");
    }
}
