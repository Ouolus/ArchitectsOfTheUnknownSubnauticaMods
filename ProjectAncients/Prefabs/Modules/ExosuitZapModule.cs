using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using ProjectAncients.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections;
using UWE;

namespace ProjectAncients.Prefabs.Modules
{
    public class ExosuitZapModule : VehicleUpgrade, IVehicleOnToggleRepeating
    {
        public ExosuitZapModule()
    : base("ExosuitZapModule", "Prawn Suit Ion Perimeter Defense System",
        "When enabled, periodically generates a powerful ionic energy field designed to ward off large aggressive fauna. Doesn't stack.")
        {
            CoroutineHost.StartCoroutine(LoadElectricalDefensePrefab());
        }

        public override ModuleEquipmentType EquipmentType => ModuleEquipmentType.ExosuitModule;

        public override QuickSlotType QuickSlotType => QuickSlotType.Selectable;

        public override TechType ModelTemplate => TechType.ExoHullModule1;

        public override float? EnergyCost => 4f;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override string[] StepsToFabricatorTab { get; } = { "ExosuitMenu" };

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override TechType RequiredForUnlock => Mod.architectElectricityMasterTech;

        static GameObject electricalDefensePrefab;

        #region Interface Implementation

        public float RepeatingActionCooldown => 1f;

        public void DoRepeatingAction(int slotID, Vehicle vehicle)
        {
            var obj = Object.Instantiate(electricalDefensePrefab);
            obj.name = "ExosuitZap";

            var fxElectSpheres = electricalDefensePrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
            var defenseSound = electricalDefensePrefab.GetComponent<ElectricalDefense>().defenseSound;

            var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
            if (ed is not null)
            {
                Object.Destroy(ed);
            }

            var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
            if (edMk2 is not null)
            {
                edMk2.fxElectSpheres = fxElectSpheres;
                edMk2.defenseSound = defenseSound;
            }

            float charge = vehicle.quickSlotCharge[slotID];
            float slotCharge = vehicle.GetSlotCharge(slotID);

            var electricalDefense = Utils
                .SpawnZeroedAt(obj, vehicle.transform)
                .GetComponent<ElectricalDefenseMK2>();

            if (electricalDefense is not null)
            {
                electricalDefense.charge = charge;
                electricalDefense.chargeScalar = slotCharge;
            }
        }

        #endregion

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.TitaniumIngot, 1),
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.PrecursorIonCrystal, 2),
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromTexture(Mod.assetBundle.LoadAsset<Texture2D>("PrawnSuitAutoZapper"));
        }

        IEnumerator LoadElectricalDefensePrefab()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return task;
            GameObject seamoth = task.GetResult();
            electricalDefensePrefab = seamoth.GetComponent<SeaMoth>().seamothElectricalDefensePrefab;
        }
    }
}
