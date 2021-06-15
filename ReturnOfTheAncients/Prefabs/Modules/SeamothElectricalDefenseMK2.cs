using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using RotA.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;

namespace RotA.Prefabs.Modules
{
    public class SeamothElectricalDefenseMK2 : VehicleUpgrade, ISeaMothOnUse
    {
        public SeamothElectricalDefenseMK2()
            : base("SeamothElectricalDefenseMK2", "Seamoth Ion Perimeter Defense System",
                "Generates a powerful ionic energy field designed to ward off large aggressive fauna. Doesn't stack.")
        {
            OnFinishedPatching += () =>
            {
                KnownTechHandler.SetAnalysisTechEntry(TechType, new TechType[0],
                    UnlockSprite: Mod.assetBundle.LoadAsset<Sprite>("SeamothElectricalDefenseMk2"));
            };
        }

        public override ModuleEquipmentType EquipmentType => ModuleEquipmentType.SeamothModule;
        
        public override QuickSlotType QuickSlotType => QuickSlotType.SelectableChargeable;
        
        public override TechType ModelTemplate => TechType.SeamothElectricalDefense;
        
        public override float? MaxCharge => 30f;
        
        public override float? EnergyCost => 10f;

        public override float CraftingTime => 7f;

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override bool UnlockedAtStart { get; } = false;

        #region Interface Implementation

        public float UseCooldown => 5f;

        public void OnUpgradeUse(int slotID, SeaMoth seaMoth)
        {
            var obj = Object.Instantiate(seaMoth.seamothElectricalDefensePrefab);
            obj.name = "ElectricalDefenseMK2";

            var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
            Object.DestroyImmediate(ed);

            var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
            if (edMk2 is not null)
            {
                edMk2.fxElectSpheres = seaMoth.seamothElectricalDefensePrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
            }

            float charge = seaMoth.quickSlotCharge[slotID];
            float slotCharge = seaMoth.GetSlotCharge(slotID);

            var electricalDefense = Utils
                .SpawnZeroedAt(obj, seaMoth.transform)
                .GetComponent<ElectricalDefenseMK2>();

            MainCameraControl.main.ShakeCamera(6f * slotCharge, 2f * slotCharge, MainCameraControl.ShakeMode.Quadratic);

            if (electricalDefense is not null)
            {
                electricalDefense.charge = charge;
                electricalDefense.chargeScalar = slotCharge;
                electricalDefense.attackType = ElectricalDefenseMK2.AttackType.Both;
            }

            obj.SetActive(true);
        }

        #endregion
        
        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.SeamothElectricalDefense, 1),
                    new Ingredient(TechType.AdvancedWiringKit, 1),
                    new Ingredient(TechType.PrecursorIonCrystal, 2),
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("SeamothElectricalDefenseMk2"));
        }

        protected override void CustomizePrefab(GameObject prefab)
        {
            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>(true);
            FixVFXFabricating(vfxFabricating);
            vfxFabricating.posOffset = new Vector3(0f, 0.05f, 0f);
            Mod.ApplyAlienUpgradeMaterials(prefab.GetComponentInChildren<Renderer>());
        }
    }
}
