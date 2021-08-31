using System.Collections;
using ArchitectsLibrary.MonoBehaviours;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrecursorIonCrystal : VanillaPrefab
    {
        GameObject _processedPrefab;
        
        public PrecursorIonCrystal()
            : base("38ebd2e5-9dcc-4d7a-ada4-86a22e01191a", "PrecursorIonCrystalAL", TechType.PrecursorIonCrystal)
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

            var obj = Resources.Load<GameObject>("WorldEntities/Natural/PrecursorIonCrystal");
            var prefab = Object.Instantiate(obj);

            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 300000;
            Main.IonCubeCraftModelFix(prefab);
            
            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonCrystal";
            
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
            
            var task = PrefabDatabase.GetPrefabForFilenameAsync("WorldEntities/Natural/PrecursorIonCrystal");
            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 300000;
            Main.IonCubeCraftModelFix(prefab);
            
            _processedPrefab = Object.Instantiate(prefab);
            _processedPrefab.SetActive(false);
            _processedPrefab.name = "PrecursorIonCrystal";

            gameObject.Set(prefab);
        }
    }
}