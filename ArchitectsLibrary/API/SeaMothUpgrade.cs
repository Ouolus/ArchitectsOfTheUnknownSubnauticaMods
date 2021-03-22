using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Patches;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace ArchitectsLibrary.API
{
    public abstract class SeaMothUpgrade : Equipable
    {
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
            };
        }
        public sealed override EquipmentType EquipmentType => EquipmentType.SeamothModule;

        public virtual TechType ModelTemplate => TechType.SeamothSolarCharge;
        public virtual float? EnergyCost { get; }
        public virtual float? MaxCharge { get; }

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