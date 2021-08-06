using System.Collections;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrecursorIonBattery : VanillaPrefab
    {
        public PrecursorIonBattery()
            : base("811c128d-a85f-4b0a-b9c4-4071db4fb7aa", "PrecursorIonBatteryAL", TechType.PrecursorIonBattery)
        {}

#if SN1
        public override GameObject GetGameObject()
        {
            var prefab = Object.Instantiate(Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonBattery"));

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.4f;
            vfxFabricating.localMaxY = 0.4f;

            return prefab;
        }
#endif

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Tools/PrecursorIonBattery");

            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.4f;
            vfxFabricating.localMaxY = 0.4f;
            
            gameObject.Set(prefab);
        }
    }
}