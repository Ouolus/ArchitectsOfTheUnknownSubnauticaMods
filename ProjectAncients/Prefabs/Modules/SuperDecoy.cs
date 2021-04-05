using ProjectAncients.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Assets;
using UnityEngine;
using System.Collections;

namespace ProjectAncients.Prefabs.Modules
{
    public class SuperDecoy : Equipable
    {
        public SuperDecoy() : base("CyclopsDecoyMk2", "Creature Decoy MK2", "Attracts creatures to its location from afar using an ionic energy field. Can be deployed by hand or by a submarine. Cannot be reclaimed once deployed.")
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.DecoySlot;

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override TechType RequiredForUnlock => Mod.architectElectricityMasterTech;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override string[] StepsToFabricatorTab { get; } = { "CyclopsMenu" };

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 3,
                Ingredients =
                {
                    new Ingredient(TechType.CyclopsDecoy, 3),
                    new Ingredient(TechType.PrecursorIonCrystal, 2),
                    new Ingredient(TechType.UraniniteCrystal, 2),
                    new Ingredient(TechType.Polyaniline, 1)
                }
            };
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.CyclopsDecoy);
            yield return task;

            var prefab = task.GetResult();
            var obj = GameObject.Instantiate(prefab);
            obj.AddComponent<EcoTarget>().type = Mod.superDecoyTargetType;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Shark;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.MediumFish;

            prefab.SetActive(false);
            obj.SetActive(true);

            gameObject.Set(obj);
        }

        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.CyclopsDecoy);
            var obj = GameObject.Instantiate(prefab);
            obj.AddComponent<EcoTarget>().type = Mod.superDecoyTargetType;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Shark;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.MediumFish;

            prefab.SetActive(false);
            obj.SetActive(true);

            return prefab;
        }

    }
}
