using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;
using System.Collections;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class VoidInteriorForcefield : Spawnable
    {
        static GameObject prefab;
        public VoidInteriorForcefield() : base("VoidInteriorForcefield", "Forcefield", ".")
        {
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if(prefab == null)
            {
                IPrefabRequest request = PrefabDatabase.GetPrefabAsync("4ea69565-60e4-4554-bbdb-671eaba6dffb");
                yield return request;
                if(request.TryGetPrefab(out GameObject requestObj))
                {
                    prefab = GameObject.Instantiate(requestObj);
                    prefab.SetActive(false);
                }
            }

        }
    }
}
