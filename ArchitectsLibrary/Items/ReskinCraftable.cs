using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    abstract class ReskinCraftable : Craftable
    {
        Atlas.Sprite sprite;
        private GameObject cachedPrefab;

        protected ReskinCraftable(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        protected abstract string ReferenceClassId { get; }

        public override bool UnlockedAtStart => false;

        protected virtual void ApplyChangesToPrefab(GameObject prefab)
        {

        }

        protected override Atlas.Sprite GetItemSprite()
        {
            if (sprite == null)
            {
                string textureName = SpriteName();
                if (string.IsNullOrEmpty(textureName))
                {
                    return null;
                }
                sprite = new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>(textureName));
            }
            return sprite;
        }

        protected abstract string SpriteName();

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
