using System;
using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Patches;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// an abstract class inheriting from <see cref="Craftable"/> that simplifies the process of making a Custom Seamoth Upgrade.
    /// </summary>
    public abstract class VehicleUpgrade : Craftable
    {
        internal Func<ModuleEquipmentType, EquipmentType> ParseAsEquipmentType =
            (x) => (EquipmentType)Enum.Parse(typeof(EquipmentType), x.ToString());
        
        /// <summary>
        /// Initializes a new <see cref="VehicleUpgrade"/>
        /// </summary>
        /// <param name="classId">The main internal identifier for this item. Your item's <see cref="TechType"/> will be created using this name.</param>
        /// <param name="friendlyName">The name displayed in-game for this item whether in the open world or in the inventory.</param>
        /// <param name="description">The description for this item, Typically seen in the PDA, Inventory, or crafting screens.</param>
        public VehicleUpgrade(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetEquipmentType(this.TechType, ParseAsEquipmentType(EquipmentType));
                CraftDataHandler.SetQuickSlotType(this.TechType, QuickSlotType);
                
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

                if (this is IExosuitOnEquip exosuitOnEquip)
                {
                    ExosuitPatches.ExosuitOnEquips[this.TechType] = exosuitOnEquip;
                }
            };
        }

        public abstract ModuleEquipmentType EquipmentType { get; }

        public virtual QuickSlotType QuickSlotType => QuickSlotType.None;
        public virtual TechType ModelTemplate => TechType.SeamothSolarCharge;
        public virtual float? EnergyCost { get; }
        public virtual float? MaxCharge { get; }

#if SN1
        public sealed override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(ModelTemplate);
            var obj = GameObject.Instantiate(prefab);

            prefab.SetActive(false);
            obj.SetActive(true);

            return obj;
        }
#endif
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

        public enum ModuleEquipmentType
        {
            SeamothModule,
            ExosuitModule,
            VehicleModule
        }
    }
}