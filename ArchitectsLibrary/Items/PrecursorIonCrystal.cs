using System.Collections;
using ArchitectsLibrary.MonoBehaviours;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace ArchitectsLibrary.Items
{
    class PrecursorIonCrystal : ModPrefab
    {
        public PrecursorIonCrystal()
            : base("38ebd2e5-9dcc-4d7a-ada4-86a22e01191a", "WorldEntities/Natural/PrecursorIonCrystal", TechType.PrecursorIonCrystal)
        {}

#if SN1
        public override GameObject GetGameObject()
        {
            var prefab = CraftData.InstantiateFromPrefab(TechType.PrecursorIonCrystal);
            
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
            
            return prefab;
        }
#endif
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;

            var prefab = Object.Instantiate(task.GetResult());
            prefab.EnsureComponent<PrecursorIonStorage>()._capacity = 1000000;
            Main.IonCubeCraftModelFix(prefab);
            
            gameObject.Set(prefab);
        }
    }
}