using System.Collections;
using ArchitectsLibrary.MonoBehaviours;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Items
{
    class PrecursorIonCrystal : ModPrefab
    {
        public PrecursorIonCrystal()
            : base("38ebd2e5-9dcc-4d7a-ada4-86a22e01191a", "PrecursorIonCrystalAL", TechType.PrecursorIonCrystal)
        {}

#if SN1
        public override GameObject GetGameObject()
        {
            var prefab = Object.Instantiate(Resources.Load<GameObject>("WorldEntities/Natural/PrecursorIonCrystal"));

            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
            
            return prefab;
        }
#endif
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = PrefabDatabase.GetPrefabForFilenameAsyncImpl("WorldEntities/Natural/PrecursorIonCrystal");
            yield return task;

            task.TryGetPrefab(out var obj);

            var prefab = Object.Instantiate(obj);
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
            
            gameObject.Set(prefab);
        }
    }
}