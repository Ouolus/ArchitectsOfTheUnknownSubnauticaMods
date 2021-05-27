using SMLHelper.V2.Assets;
using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class AtmosphereVolumePrefab : Spawnable
    {
        GameObject cachedPrefab;
        string baseClassId;

        public AtmosphereVolumePrefab(string newClassId, string classId = "d645d7c7-76a2-4818-86b0-5c3e37a51e31") : base(newClassId, ".", ".") //the default class id here is that of the Antechamber atmosphere volume
        {
            baseClassId = classId;
        }

        void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.GetComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.VeryFar;
        }
#if SN1
        public override GameObject GetGameObject()
        {
            if(cachedPrefab == null)
            {
                PrefabDatabase.TryGetPrefab(baseClassId, out GameObject prefab);
                cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                ApplyChangesToPrefab(cachedPrefab);
            }
            return cachedPrefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if(cachedPrefab == null)
            {
                IPrefabRequest request = PrefabDatabase.GetPrefabAsync(baseClassId);
                yield return request;
                request.TryGetPrefab(out GameObject prefab);
                GameObject cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                ApplyChangesToPrefab(cachedPrefab);
            }
            gameObject.Set(cachedPrefab);
        }
#endif
    }
}
