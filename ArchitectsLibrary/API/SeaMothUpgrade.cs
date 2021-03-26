using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Patches;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// an abstract class inheriting from <see cref="Equipable"/> that simplifies the process of making a Custom Seamoth Upgrade.
    /// </summary>
    public abstract class SeaMothUpgrade : Equipable
    {
        /// <summary>
        /// Initializes a new <see cref="SeaMothUpgrade"/>
        /// </summary>
        /// <param name="classId">The main internal identifier for this item. Your item's <see cref="TechType"/> will be created using this name.</param>
        /// <param name="friendlyName">The name displayed in-game for this item whether in the open world or in the inventory.</param>
        /// <param name="description">The description for this item, Typically seen in the PDA, Inventory, or crafting screens.</param>
        public SeaMothUpgrade(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            OnFinishedPatching += () =>
            {
                if (MaxCharge.HasValue)
                    VehicleHandler.MaxCharge(this.TechType, MaxCharge.Value);
                
                if (EnergyCost.HasValue)
                    VehicleHandler.EnergyCost(this.TechType, EnergyCost.Value);

                if (this is ISeaMothOnUse seaMothOnUse)
                {
                    SeaMothPatches.seaMothOnUses[this.TechType] = seaMothOnUse;
                }

                if (this is ISeaMothOnEquip seaMothOnEquip)
                {
                    SeaMothPatches.SeaMothOnEquips[this.TechType] = seaMothOnEquip;
                }
            };
        }
        public sealed override EquipmentType EquipmentType => EquipmentType.SeamothModule;

        public virtual TechType ModelTemplate => TechType.SeamothSolarCharge;
        public virtual float? EnergyCost { get; }
        public virtual float? MaxCharge { get; }

        public sealed override GameObject GetGameObject()
        {
            return base.GetGameObject();
        }

        public sealed override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (ModelTemplate != TechType.None)
            {
                CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(ModelTemplate);
                yield return task;
                
                var prefab = task.GetResult();
                var obj = GameObject.Instantiate(prefab);
                
                prefab.SetActive(false);
                obj.SetActive(true);
                
                gameObject.Set(obj);
            }
        }
    }
}