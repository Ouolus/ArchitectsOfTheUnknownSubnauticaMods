using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    /// <summary>
    ///An item based of off a base-game item, that can spawn in the world if you want.
    /// </summary>
    public abstract class ReskinSpawnable : Spawnable
    {
        GameObject cachedPrefab;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="classId">Class ID.</param>
        /// <param name="friendlyName">Name.</param>
        /// <param name="description">Tooltip.</param>
        protected ReskinSpawnable(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        /// <summary>
        /// The original class id of this crafable.
        /// </summary>
        protected abstract string ReferenceClassId { get; }

        /// <summary>
        /// Allows you to customize this object.
        /// </summary>
        /// <param name="prefab"></param>
        protected virtual void ApplyChangesToPrefab(GameObject prefab)
        {

        }

#if SN1
        public sealed override GameObject GetGameObject()
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
        public sealed override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
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
