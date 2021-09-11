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
    /// an abstract class inheriting from <see cref="Craftable"/> that simplifies the process of making a custom cyclops upgrade.
    /// </summary>
    public abstract class CyclopsUpgrade : Craftable
    {        
        /// <summary>
        /// Initializes a new <see cref="VehicleUpgrade"/>
        /// </summary>
        /// <param name="classId">The main internal identifier for this item. Your item's <see cref="TechType"/> will be created using this name.</param>
        /// <param name="friendlyName">The name displayed in-game for this item whether in the open world or in the inventory.</param>
        /// <param name="description">The description for this item, Typically seen in the PDA, Inventory, or crafting screens.</param>
        public CyclopsUpgrade(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {
            OnFinishedPatching += () =>
            {
                CraftDataHandler.SetEquipmentType(TechType, EquipmentType.CyclopsModule);
            };
        }
        
        /// <summary>
        /// TechType that this module will instantiate its prefab and copy it as its own.
        /// </summary>
        public virtual TechType ModelTemplate => TechType.CyclopsHullModule1;

#if SN1
        public sealed override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(ModelTemplate);
            var obj = GameObject.Instantiate(prefab);

            prefab.SetActive(false);
            obj.SetActive(true);

            CustomizePrefab(obj);

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

                CustomizePrefab(obj);

                gameObject.Set(obj);
            }
        }

        /// <summary>
        /// Allows you to customize the prefab for this item (which by default is a clone of <see cref="ModelTemplate"/>).
        /// </summary>
        /// <param name="prefab"></param>
        protected virtual void CustomizePrefab(GameObject prefab)
        {

        }

        /// <summary>
        /// A method that fixes the crafting model that seems to break on some modules for some reason.
        /// </summary>
        /// <param name="vfx"></param>
        protected void FixVFXFabricating(VFXFabricating vfx)
        {
            vfx.localMinY = -0.19f;
            vfx.localMaxY = 0.13f;
            vfx.posOffset = new Vector3(0f, 0.1f, 0f);
            vfx.eulerOffset = new Vector3(0f, 90f, 90f);
        }
    }
}