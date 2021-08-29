using System.Collections;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrecursorIonPowerCell : VanillaPrefab
    {
        GameObject _processedPrefab;
        
        public PrecursorIonPowerCell()
            : base("f54adc84-8087-49a7-b99c-2954e264f279", "PrecursorIonPowerCellAL", TechType.PrecursorIonPowerCell)
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
            
            var obj = Resources.Load<GameObject>("WorldEntities/Tools/PrecursorIonPowerCell");
            var prefab = Object.Instantiate(obj);

            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.5f;
            vfxFabricating.localMaxY = 0.2f;
            
            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonPowerCell";

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
            
            var task = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Tools/PrecursorIonPowerCell");

            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);
                
            var vfxFabricating = prefab.GetComponentInChildren<VFXFabricating>();
            vfxFabricating.localMinY = -0.5f;
            vfxFabricating.localMaxY = 0.2f;
            
            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonPowerCell";
            
            gameObject.Set(prefab);
        }
    }
}