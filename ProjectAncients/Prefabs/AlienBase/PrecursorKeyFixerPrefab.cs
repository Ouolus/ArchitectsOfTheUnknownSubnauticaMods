using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class PrecursorKeyFixer : Spawnable
    {
        private string originalClassId;
        public PrecursorKeyFixer(string classId, string originalClassId) : base(classId, null, null)
        {
            this.originalClassId = originalClassId;
        }

        public override GameObject GetGameObject()
        {
            GameObject obj = null;
            if (PrefabDatabase.TryGetPrefab(originalClassId, out GameObject prefab))
            {
                obj = GameObject.Instantiate(prefab);
                obj.SetActive(false);
                obj.GetComponent<BoxCollider>().isTrigger = false;
            }
            return obj;
        }
    }
}
