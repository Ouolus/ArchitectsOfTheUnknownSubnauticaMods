using System.Collections;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrecursorIonBattery : VanillaPrefab
    {
        GameObject _processedPrefab;
        
        public PrecursorIonBattery()
            : base("811c128d-a85f-4b0a-b9c4-4071db4fb7aa", "PrecursorIonBatteryAL", TechType.PrecursorIonBattery)
        {}

#if SN1
        public override GameObject GetGameObject()
        {
            if (_processedPrefab != null)
            {
                var go = Object.Instantiate(_processedPrefab);
                go.SetActive(true);
                return go;
            }
            
            var obj = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonBattery");
            var prefab = Object.Instantiate(obj);

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.4f;
            vfxFabricating.localMaxY = 0.4f;

            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonBattery";

            return prefab;
        }
#endif

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (_processedPrefab != null)
            {
                var go = Object.Instantiate(_processedPrefab);
                go.SetActive(true);
                gameObject.Set(go);
                yield break;
            }
            
            var task = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Tools/PrecursorIonBattery");

            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.4f;
            vfxFabricating.localMaxY = 0.4f;
            
            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonBattery";

            gameObject.Set(prefab);
        }
    }
}