using System.Collections;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrecursorIonPowerCell : VanillaPrefab
    {
        public PrecursorIonPowerCell()
            : base("f54adc84-8087-49a7-b99c-2954e264f279", "PrecursorIonPowerCellAL", TechType.PrecursorIonPowerCell)
        {}

#if SN1
        public override GameObject GetGameObject()
        {
            var prefab = Object.Instantiate(Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonPowerCell"));

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.5f;
            vfxFabricating.localMaxY = 0.2f;

            return prefab;
        }
#endif

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Tools/PrecursorIonPowerCell");

            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);
                
            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.5f;
            vfxFabricating.localMaxY = 0.2f;
            
            gameObject.Set(prefab);
        }
    }
}