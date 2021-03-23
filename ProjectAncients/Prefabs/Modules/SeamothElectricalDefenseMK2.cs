using ArchitectsLibrary.API;
using ArchitectsLibrary.Interfaces;
using ProjectAncients.Mono.Modules;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace ProjectAncients.Prefabs.Modules
{
    public class SeamothElectricalDefenseMK2 : SeaMothUpgrade, ISeaMothOnUse
    {
        public SeamothElectricalDefenseMK2()
            : base("SeamothElectricalDefenseMK2", "Seamoth Perimeter Defense MK2",
                "Implements a nanotechnology system that generates an extremely powerful plasma-like substance. Defensive capabilities against leviathan-class predators. Use with caution.")
        {}
        public override QuickSlotType QuickSlotType => QuickSlotType.SelectableChargeable;
        public override TechType ModelTemplate => TechType.SeamothElectricalDefense;
        public override float? MaxCharge => 30f;
        public override float? EnergyCost => 5f;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;
        public override string[] StepsToFabricatorTab => new string[] { "SeamothMenu" };
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        #region Interface Implementation

        public float Cooldown => 5f;
        public void OnUpgradeUse(int slotID, SeaMoth seaMoth)
        {
            var obj = Object.Instantiate(seaMoth.seamothElectricalDefensePrefab);
            obj.name = "ElectricalDefenseMK2";

            var ed = obj.GetComponent<ElectricalDefense>() ?? obj.GetComponentInParent<ElectricalDefense>();
            if (ed is not null)
            {
                Object.Destroy(ed);
            }

            var edMk2 = obj.EnsureComponent<ElectricalDefenseMK2>();
            if (edMk2 is not null)
            {
                edMk2.fxElectSpheres = seaMoth.seamothElectricalDefensePrefab.GetComponent<ElectricalDefense>().fxElecSpheres;
                edMk2.defenseSound = seaMoth.seamothElectricalDefensePrefab.GetComponent<ElectricalDefense>().defenseSound;
            }

            float charge = seaMoth.quickSlotCharge[slotID];
            float slotCharge = seaMoth.GetSlotCharge(slotID);

            var electricalDefense = Utils
                .SpawnZeroedAt(obj, seaMoth.transform)
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
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.Titanium, 2)
                }
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.SeamothElectricalDefense);
        }
    }
}