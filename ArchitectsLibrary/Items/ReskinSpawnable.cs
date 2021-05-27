using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    abstract class ReskinSpawnable : Spawnable
    {
        GameObject cachedPrefab;

        protected ReskinSpawnable(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        protected abstract string ReferenceClassId { get; }

        protected virtual void ApplyChangesToPrefab(GameObject prefab)
        {

        }

#if SN1
        public override GameObject GetGameObject()
        {
            if (cachedPrefab == null)
            {
                PrefabDatabase.TryGetPrefab(ReferenceClassId, out GameObject prefab);
                cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                cachedPrefab.EnsureComponent<TechTag>().type = TechType;
                cachedPrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                ApplyChangesToPrefab(cachedPrefab);
            }
            return cachedPrefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (cachedPrefab == null)
            {
                IPrefabRequest request = PrefabDatabase.GetPrefabAsync(ReferenceClassId);
                yield return request;
                request.TryGetPrefab(out GameObject prefab);
                GameObject cachedPrefab = GameObject.Instantiate(prefab);
                cachedPrefab.SetActive(false);
                cachedPrefab.EnsureComponent<TechTag>().type = TechType;
                cachedPrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                ApplyChangesToPrefab(cachedPrefab);
            }
            gameObject.Set(cachedPrefab);
        }
#endif
    }
}
