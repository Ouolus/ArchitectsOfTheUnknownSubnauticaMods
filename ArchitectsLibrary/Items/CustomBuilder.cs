using ArchitectsLibrary.MonoBehaviours;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace ArchitectsLibrary.Items
{
    // in-complete custom builder class, merely serves as a test for the custom builder API
    internal class CustomBuilder : Equipable
    {
        public CustomBuilder()
            : base("custombuilder", "Custom Builder", "Custom Builder that makes me go yes")
        {}

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.Builder);
            var builderTool = prefab.GetComponent<BuilderTool>();

            var obj = Object.Instantiate(prefab);
            Object.DestroyImmediate(obj.GetComponent<BuilderTool>());
            var builder = obj.EnsureComponent<CustomBuilderTool>();
            builder.animator = obj.GetComponent<Animator>() ?? obj.GetComponentInChildren<Animator>();
            builder.bar = builderTool.bar;
            builder.barMaterial = builderTool.barMaterial;
            builder.barMaterialID = builderTool.barMaterialID;
            builder.beamLeft = builderTool.beamLeft;
            builder.beamRight = builderTool.beamRight;
            builder.buildSound = builderTool.buildSound;
            builder.completeSound = builderTool.completeSound;
            builder.constructText = builderTool.constructText;
            builder.drawSound = builderTool.drawSound;
            builder.mainCollider = obj.GetComponent<Collider>();
            builder.pickupable = obj.GetComponent<Pickupable>();
            
            return obj;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Titanium, 1)
                }
            };
        }
    }
}