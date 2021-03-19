using ProjectAncients.Mono.Modules;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace ProjectAncients.Prefabs.Modules
{
    public class SeamothElectricalDefenseMK2 : Equipable
    {
        public SeamothElectricalDefenseMK2()
            : base("SeamothElectricalDefenseMK2", "Seamoth Perimeter Defense MK2",
                "Perimeter Defense that makes me go yes")
        {
            OnFinishedPatching += () =>
            {
                CraftData.maxCharges[this.TechType] = 30f;
                CraftData.energyCost[this.TechType] = 5f;
            };
        }
		
		public override EquipmentType EquipmentType => EquipmentType.SeamothModule;
        public override QuickSlotType QuickSlotType => QuickSlotType.SelectableChargeable;

        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.SeamothElectricalDefense);
            prefab.SetActive(false);
            
            
            var obj = Object.Instantiate(prefab);
            obj.SetActive(true);
            
            return obj;
        }
        
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